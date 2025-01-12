#nullable disable

using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Enums;
using maERP.SalesChannels.Models.Shopware5;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using maERP.SalesChannels.Models;
using maERP.SalesChannels.Contracts;
using Microsoft.IdentityModel.Tokens;

namespace maERP.SalesChannels.Tasks;

public class Shopware5OrderImportTask : IHostedService
{
    private readonly IServiceScopeFactory _service;
    private readonly ILogger<Shopware5OrderImportTask> _logger;

    public Shopware5OrderImportTask(IServiceScopeFactory serviceScopeFactory, ILogger<Shopware5OrderImportTask> logger)
    {
        _service = serviceScopeFactory;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(async () =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation("Shopware5OrderImportTask MainLoop start");

                await MainLoop();

                await Task.Delay(new TimeSpan(0, 0, 60)); // 60 second delay

                _logger.LogInformation("Shopware5OrderImportTask MainLoop finished");
            }
        }, cancellationToken);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task MainLoop()
    {
        var scope = _service.CreateScope();

        var salesChannelRepository = scope.ServiceProvider.GetService<ISalesChannelRepository>();
        var orderImportRepository = scope.ServiceProvider.GetService<IOrderImportRepository>();

        var salesChannels = await salesChannelRepository.GetAllAsync();

        foreach (var salesChannel in salesChannels)
        {
            if (salesChannel.Type != SalesChannelType.Shopware5 || salesChannel.ImportOrders != true)
            {
                continue;
            }

            if (salesChannel.ImportProducts && !salesChannel.InitialProductImportCompleted)
            {
                _logger.LogInformation($"Initial Product Import not completed for {salesChannel.Name} (ID: {salesChannel.Id})");
                continue;
            }

            _logger.LogInformation($"Start OrderDownload for {salesChannel.Name} (ID: {salesChannel.Id})");

            int requestStart = 0;
            int requestLimit = 100;
            int requestMax = 0;

            do
            {
                try
                {
                    var client = new HttpClient();
                    string requestUrl = salesChannel.URL + $"/api/orders?start={requestStart}&limit={requestLimit}";
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var authenticationString = $"{salesChannel.Username}:{salesChannel.Password}";
                    var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.UTF8.GetBytes(authenticationString));
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + base64EncodedAuthenticationString);

                    HttpResponseMessage response;
                    response = await client.GetAsync(requestUrl).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;

                        _logger.LogInformation("Import Orders from {0} to {1}", requestStart, requestStart + requestLimit);

                        BaseListResponse<OrderResponse> remoteOrders = new();

                        try
                        {
                            remoteOrders = JsonSerializer.Deserialize<BaseListResponse<OrderResponse>>(result);

                            if (remoteOrders.data == null || remoteOrders.success == false)
                            {
                                throw new Exception("No data in response");
                            }

                            requestMax = remoteOrders.total;
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"Import Order error: {ex.Message}");
                        }

                        foreach (var remoteOrder in remoteOrders.data)
                        {
                            _logger.LogInformation("Import Order {0}", remoteOrder.id.ToString());

                            if(remoteOrder.orderStatusId == -1)
                            {
                                _logger.LogInformation("Import Order {0} skip order because cancelled in sales channel", remoteOrder.id.ToString());
                                continue;
                            }

                            string requestDetailUrl = salesChannel.URL + $"/api/orders/{remoteOrder.id}";
                            response = await client.GetAsync(requestDetailUrl).ConfigureAwait(false);

                            if (response.IsSuccessStatusCode)
                            {
                                string detailResult = response.Content.ReadAsStringAsync().Result;

                                try
                                {
                                    var remoteOrderDetailResponse = JsonSerializer.Deserialize<BaseResponse<OrderDetailResponse>>(detailResult);

                                    if (remoteOrderDetailResponse.data == null || remoteOrderDetailResponse.success == false)
                                    {
                                        throw new Exception("No data in response");
                                    }

                                    OrderDetailResponse remoteOrderDetail = remoteOrderDetailResponse.data;

                                    if (remoteOrderDetail.customer == null)
                                    {
                                        remoteOrderDetail.customer = new Customer
                                        {
                                            firstLogin = DateTime.MinValue.ToString()
                                        };
                                    }

                                    var salesChannelImportOrder = new SalesChannelImportOrder
                                    {
                                        RemoteOrderId = remoteOrder.id.ToString(),
                                        RemoteCustomerId = remoteOrderDetail.customer.id.ToString(),
                                        DateOrdered = DateTime.Parse(remoteOrderDetail.orderTime).ToUniversalTime(), // remoteOrder.orderTime,
                                        Status = MapOrderStatus(remoteOrder.orderStatusId),
                                        PaymentStatus = MapPaymentStatus(remoteOrder.paymentStatusId),

                                        Subtotal = remoteOrder.invoiceAmountNet,
                                        ShippingCost = remoteOrder.invoiceShippingNet,
                                        TotalTax = remoteOrder.invoiceAmount - remoteOrder.invoiceAmountNet,
                                        Total = remoteOrder.invoiceAmount,

                                        Customer = new SalesChannelImportCustomer
                                        {
                                            Firstname = remoteOrderDetail.billing.firstName,
                                            Lastname = remoteOrderDetail.billing.lastName,
                                            CompanyName = remoteOrderDetail.billing.company,
                                            Email = String.IsNullOrEmpty(remoteOrderDetail.customer.email) ? string.Empty : remoteOrderDetail.customer.email,
                                            Phone = remoteOrderDetail.billing.phone,
                                            DateEnrollment = DateTime.Parse(remoteOrderDetail.customer.firstLogin).ToUniversalTime()
                                        },

                                        BillingAddress = new SalesChannelImportCustomerAddress
                                        {
                                            Firstname = remoteOrderDetail.billing.firstName,
                                            Lastname = remoteOrderDetail.billing.lastName,
                                            CompanyName = remoteOrderDetail.billing.company,
                                            Street = remoteOrderDetail.billing.street,
                                            City = remoteOrderDetail.billing.city,
                                            Zip = remoteOrderDetail.billing.zipCode,
                                            Country = remoteOrderDetail.billing.country.iso
                                        },

                                        ShippingAddress = new SalesChannelImportCustomerAddress
                                        {
                                            Firstname = remoteOrderDetail.shipping.firstName,
                                            Lastname = remoteOrderDetail.shipping.lastName,
                                            CompanyName = remoteOrderDetail.shipping.company,
                                            Street = remoteOrderDetail.shipping.street,
                                            City = remoteOrderDetail.shipping.city,
                                            Zip = remoteOrderDetail.shipping.zipCode,
                                            Country = remoteOrderDetail.shipping.country.iso
                                        }
                                    };

                                    salesChannelImportOrder.Items = remoteOrderDetail.details.Select(item => new SalesChannelImportOrderItem
                                    {
                                        Name = item.articleName,
                                        SKU = item.articleNumber,
                                        Quantity = (double)item.quantity,
                                        Price = item.price,
                                        TaxRate = item.taxRate,
                                        Ean = item.ean
                                    }).ToList();

                                    await orderImportRepository.ImportOrUpdateFromSalesChannel(salesChannel, salesChannelImportOrder);
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"Import Order error: {ex.Message}");
                                    continue;
                                }
                            }
                            else
                            {
                                _logger.LogError($"Import Order error: {response.StatusCode}");
                            }

                            _logger.LogInformation("Import Order {0} finished", remoteOrder.id.ToString());
                        }

                        response.Dispose();

                        requestStart += requestLimit;

                        await Task.Delay(new TimeSpan(0, 0, 10));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
            while (requestMax != 0 && requestStart <= requestMax);
        }
    }

    private OrderStatus MapOrderStatus(int orderStatusId)
    {
        /* 
         * -1 state cancelled Abgebrochen
         * 0 state open Offen
         * 1 state in_process In Bearbeitung (wartet)
         * 2 state completed Komplett abgeschlossen
         * 3 state partially_completed Teilweise abgeschlossen
         * 4 state cancelled_rejected Storniert / Abgelehnt
         * 5 state ready_for_delivery Zur Lieferung bereit
         * 6 state partially_delivered Teilweise ausgeliefert
         * 7 state completely_delivered Komplett ausgeliefert
         * 8 state clarification_required Klärung notwendig
         * 9 state partially_invoiced Teilweise in Rechnung gestellt
         */

        return orderStatusId switch
        {
            0 => OrderStatus.Pending,
            1 => OrderStatus.Processing,
            8 => OrderStatus.OnHold,
            7 => OrderStatus.Completed,
            4 => OrderStatus.Cancelled,
            -1 => OrderStatus.Failed,
            _ => OrderStatus.Unknown
        };
    }

    private PaymentStatus MapPaymentStatus(int orderPaymentStatusId)
    {
        /* 
         * 11 payment partially_paid Teilweise bezahlt
         * 12 payment completely_paid Komplett bezahlt
         * 13 payment 1st_reminder 1. Mahnung
         * 14 payment 2nd_reminder 2. Mahnung
         * 15 payment 3rd_reminder 3. Mahnung
         * 16 payment encashment Inkasso
         * 17 payment open offen
         * 18 payment reserved Reserviert
         * 19 payment delayed Verzögert
         * 20 payment re_crediting Wiedergutschrift
         * 21 payment review_necessary Überprüfung notwendig
         * 22 payment no_credit_approved Es wurde kein Kredit genehmigt
         * 23 payment the_credit_has_been_preliminarily_accepted Der Kredit wurde vorläufig akzeptiert
         */

        return orderPaymentStatusId switch
        {
            17 => PaymentStatus.Invoiced,
            11 => PaymentStatus.PartiallyPaid,
            12 => PaymentStatus.CompletelyPaid,
            13 => PaymentStatus.FirstReminder,
            14 => PaymentStatus.SecondReminder,
            15 => PaymentStatus.ThirdReminder,
            16 => PaymentStatus.Encashment,
            18 => PaymentStatus.Reserved,
            19 => PaymentStatus.Delayed,
            20 => PaymentStatus.ReCrediting,
            21 => PaymentStatus.ReviewNecessary,
            22 => PaymentStatus.NoCreditApproved,
            23 => PaymentStatus.CreditPreliminarilyAccepted,
            _ => PaymentStatus.Unknown
        };
    }
}