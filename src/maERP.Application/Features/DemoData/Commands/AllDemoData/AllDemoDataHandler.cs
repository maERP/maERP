using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.DemoData.Commands.AllDemoData;

public class AllDemoDataHandler : IRequestHandler<AllDemoDataCommand, Result<string>>
{
    private readonly IAppLogger<AllDemoDataHandler> _logger;
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly ITaxClassRepository _taxClassRepository;
    public AllDemoDataHandler(
        IAppLogger<AllDemoDataHandler> logger,
        IWarehouseRepository warehouseRepository,
        IProductRepository productRepository,
        ICustomerRepository customerRepository,
        IOrderRepository orderRepository,
        ITaxClassRepository taxClassRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _warehouseRepository = warehouseRepository ?? throw new ArgumentNullException(nameof(warehouseRepository));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _taxClassRepository = taxClassRepository ?? throw new ArgumentNullException(nameof(taxClassRepository));
    }

    public async Task<Result<string>> Handle(AllDemoDataCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to create all demo data");

        var result = new Result<string>();
        var createdItems = new List<string>();

        try
        {
            // Create Tax Classes (19%, 7%, 0%)
            var taxClasses = GetDemoTaxClasses();
            foreach (var taxClass in taxClasses)
            {
                await _taxClassRepository.CreateAsync(taxClass);
            }
            createdItems.Add($"{taxClasses.Count} tax classes");

            // Create 3 Warehouses
            var warehouses = GetDemoWarehouses();
            foreach (var warehouse in warehouses)
            {
                await _warehouseRepository.CreateAsync(warehouse);
            }
            createdItems.Add($"{warehouses.Count} warehouses");

            // Create 10 Customers
            var customers = GetDemoCustomers();
            foreach (var customer in customers)
            {
                await _customerRepository.CreateAsync(customer);
            }
            createdItems.Add($"{customers.Count} customers");

            // Create 20 Products
            var products = GetDemoProducts(taxClasses);
            foreach (var product in products)
            {
                await _productRepository.CreateAsync(product);
            }
            createdItems.Add($"{products.Count} products");

            // Create 20 Orders
            var orders = GetDemoOrders(customers);
            foreach (var order in orders)
            {
                await _orderRepository.CreateAsync(order);
            }
            createdItems.Add($"{orders.Count} orders");

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Created;
            result.Data = $"Successfully created: {string.Join(", ", createdItems)}";

            _logger.LogInformation("Successfully created all demo data: {Items}", string.Join(", ", createdItems));
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while creating demo data: {ex.Message}");

            _logger.LogError("Error creating demo data: {Message}", ex.Message);
        }

        return result;
    }

    private List<Domain.Entities.Warehouse> GetDemoWarehouses()
    {
        return new List<Domain.Entities.Warehouse>
        {
            new() { Name = "Hauptlager Hamburg" },
            new() { Name = "Lager München" },
            new() { Name = "Lager Berlin" }
        };
    }

    private List<Domain.Entities.Customer> GetDemoCustomers()
    {
        var baseDate = DateTimeOffset.UtcNow.AddDays(-30);

        return new List<Domain.Entities.Customer>
        {
            new()
            {
                Firstname = "Max",
                Lastname = "Mustermann",
                CompanyName = "Mustermann GmbH",
                Email = "max.mustermann@demo.com",
                Phone = "+49 30 12345678",
                Website = "www.mustermann-gmbh.de",
                VatNumber = "DE123456789",
                Note = "Demo customer - reliable business partner",
                CustomerStatus = CustomerStatus.Active,
                DateEnrollment = baseDate.AddDays(1)
            },
            new()
            {
                Firstname = "Anna",
                Lastname = "Schmidt",
                CompanyName = "Schmidt Consulting",
                Email = "anna.schmidt@demo.com",
                Phone = "+49 40 87654321",
                Website = "www.schmidt-consulting.de",
                VatNumber = "DE987654321",
                Note = "Demo customer - frequent orders",
                CustomerStatus = CustomerStatus.Active,
                DateEnrollment = baseDate.AddDays(2)
            },
            new()
            {
                Firstname = "Thomas",
                Lastname = "Weber",
                CompanyName = "Weber Technologies",
                Email = "thomas.weber@demo.com",
                Phone = "+49 89 55512345",
                Website = "www.weber-tech.de",
                VatNumber = "DE555123456",
                Note = "Demo customer - tech industry",
                CustomerStatus = CustomerStatus.Active,
                DateEnrollment = baseDate.AddDays(3)
            },
            new()
            {
                Firstname = "Lisa",
                Lastname = "Müller",
                CompanyName = "Müller Design Studio",
                Email = "lisa.mueller@demo.com",
                Phone = "+49 221 99988877",
                Website = "www.mueller-design.de",
                VatNumber = "DE999888777",
                Note = "Demo customer - creative services",
                CustomerStatus = CustomerStatus.Active,
                DateEnrollment = baseDate.AddDays(4)
            },
            new()
            {
                Firstname = "Michael",
                Lastname = "Bauer",
                CompanyName = "Bauer Construction",
                Email = "michael.bauer@demo.com",
                Phone = "+49 711 44455566",
                Website = "www.bauer-construction.de",
                VatNumber = "DE444555666",
                Note = "Demo customer - construction industry",
                CustomerStatus = CustomerStatus.Active,
                DateEnrollment = baseDate.AddDays(5)
            },
            new()
            {
                Firstname = "Sarah",
                Lastname = "Wagner",
                CompanyName = "Wagner Marketing",
                Email = "sarah.wagner@demo.com",
                Phone = "+49 611 77788899",
                Website = "www.wagner-marketing.de",
                VatNumber = "DE777888999",
                Note = "Demo customer - marketing agency",
                CustomerStatus = CustomerStatus.Active,
                DateEnrollment = baseDate.AddDays(6)
            },
            new()
            {
                Firstname = "Daniel",
                Lastname = "Fischer",
                CompanyName = "Fischer Automotive",
                Email = "daniel.fischer@demo.com",
                Phone = "+49 511 33344455",
                Website = "www.fischer-automotive.de",
                VatNumber = "DE333444555",
                Note = "Demo customer - automotive sector",
                CustomerStatus = CustomerStatus.Active,
                DateEnrollment = baseDate.AddDays(7)
            },
            new()
            {
                Firstname = "Julia",
                Lastname = "Hoffmann",
                CompanyName = "Hoffmann Logistics",
                Email = "julia.hoffmann@demo.com",
                Phone = "+49 341 66677788",
                Website = "www.hoffmann-logistics.de",
                VatNumber = "DE666777888",
                Note = "Demo customer - logistics services",
                CustomerStatus = CustomerStatus.Active,
                DateEnrollment = baseDate.AddDays(8)
            },
            new()
            {
                Firstname = "Robert",
                Lastname = "Klein",
                CompanyName = "Klein Electronics",
                Email = "robert.klein@demo.com",
                Phone = "+49 201 11122233",
                Website = "www.klein-electronics.de",
                VatNumber = "DE111222333",
                Note = "Demo customer - electronics retailer",
                CustomerStatus = CustomerStatus.Inactive,
                DateEnrollment = baseDate.AddDays(9)
            },
            new()
            {
                Firstname = "Petra",
                Lastname = "Richter",
                CompanyName = "Richter Fashion",
                Email = "petra.richter@demo.com",
                Phone = "+49 351 88899900",
                Website = "www.richter-fashion.de",
                VatNumber = "DE888999000",
                Note = "Demo customer - fashion industry, pending email confirmation",
                CustomerStatus = CustomerStatus.NoDoi,
                DateEnrollment = baseDate.AddDays(10)
            }
        };
    }

    private List<Domain.Entities.Product> GetDemoProducts(List<Domain.Entities.TaxClass> taxClasses)
    {
        var standardTaxId = taxClasses.First(t => t.TaxRate == 19.0).Id; // Standard VAT
        var reducedTaxId = taxClasses.First(t => t.TaxRate == 7.0).Id;   // Reduced VAT
        var zeroTaxId = taxClasses.First(t => t.TaxRate == 0.0).Id;      // Tax-free

        return new List<Domain.Entities.Product>
        {
            new() { Sku = "LAPTOP-001", Name = "Business Laptop Pro", Price = 1299.99m, Msrp = 1499.99m, Weight = 1.8m, TaxClassId = standardTaxId },
            new() { Sku = "MOUSE-001", Name = "Wireless Mouse Ergonomic", Price = 29.99m, Msrp = 39.99m, Weight = 0.1m, TaxClassId = standardTaxId },
            new() { Sku = "KEYBOARD-001", Name = "Mechanical Keyboard RGB", Price = 149.99m, Msrp = 199.99m, Weight = 1.2m, TaxClassId = standardTaxId },
            new() { Sku = "MONITOR-001", Name = "4K Monitor 27 inch", Price = 399.99m, Msrp = 499.99m, Weight = 6.5m, TaxClassId = standardTaxId },
            new() { Sku = "HEADSET-001", Name = "Noise Cancelling Headset", Price = 199.99m, Msrp = 249.99m, Weight = 0.3m, TaxClassId = standardTaxId },
            new() { Sku = "CHAIR-001", Name = "Ergonomic Office Chair", Price = 299.99m, Msrp = 399.99m, Weight = 15.0m, TaxClassId = standardTaxId },
            new() { Sku = "DESK-001", Name = "Standing Desk Electric", Price = 599.99m, Msrp = 799.99m, Weight = 45.0m, TaxClassId = standardTaxId },
            new() { Sku = "TABLET-001", Name = "Business Tablet 10 inch", Price = 449.99m, Msrp = 549.99m, Weight = 0.5m, TaxClassId = standardTaxId },
            new() { Sku = "PHONE-001", Name = "Smartphone Business Edition", Price = 799.99m, Msrp = 999.99m, Weight = 0.2m, TaxClassId = standardTaxId },
            new() { Sku = "PRINTER-001", Name = "Laser Printer Color", Price = 249.99m, Msrp = 299.99m, Weight = 18.0m, TaxClassId = standardTaxId },
            new() { Sku = "CABLE-001", Name = "USB-C Cable 2m", Price = 19.99m, Msrp = 29.99m, Weight = 0.1m, TaxClassId = standardTaxId },
            new() { Sku = "SPEAKER-001", Name = "Bluetooth Speaker Portable", Price = 79.99m, Msrp = 99.99m, Weight = 0.8m, TaxClassId = standardTaxId },
            new() { Sku = "CAMERA-001", Name = "Webcam HD 1080p", Price = 89.99m, Msrp = 119.99m, Weight = 0.3m, TaxClassId = standardTaxId },
            new() { Sku = "HARDDRIVE-001", Name = "External SSD 1TB", Price = 149.99m, Msrp = 199.99m, Weight = 0.2m, TaxClassId = standardTaxId },
            new() { Sku = "ROUTER-001", Name = "WiFi Router 6 Gigabit", Price = 199.99m, Msrp = 249.99m, Weight = 1.0m, TaxClassId = standardTaxId },
            new() { Sku = "LIGHT-001", Name = "LED Desk Lamp Smart", Price = 59.99m, Msrp = 79.99m, Weight = 1.5m, TaxClassId = standardTaxId },
            new() { Sku = "BAG-001", Name = "Laptop Bag Premium", Price = 89.99m, Msrp = 119.99m, Weight = 0.8m, TaxClassId = standardTaxId },
            new() { Sku = "DOCK-001", Name = "USB-C Docking Station", Price = 179.99m, Msrp = 229.99m, Weight = 0.6m, TaxClassId = standardTaxId },
            new() { Sku = "BOOK-001", Name = "Business Management Guide", Price = 29.99m, Msrp = 39.99m, Weight = 0.5m, TaxClassId = reducedTaxId },
            new() { Sku = "SAMPLE-001", Name = "Free Product Sample", Price = 0.00m, Msrp = 0.00m, Weight = 0.1m, TaxClassId = zeroTaxId }
        };
    }

    private List<Domain.Entities.Order> GetDemoOrders(List<Domain.Entities.Customer> customers)
    {
        var random = new Random(42); // Fixed seed for consistent demo data
        var orders = new List<Domain.Entities.Order>();
        var baseDate = DateTime.UtcNow.AddDays(-20);

        var orderStatuses = new[] { OrderStatus.Pending, OrderStatus.Processing, OrderStatus.ReadyForDelivery, OrderStatus.Completed, OrderStatus.OnHold };
        var paymentStatuses = new[] { PaymentStatus.Invoiced, PaymentStatus.CompletelyPaid, PaymentStatus.PartiallyPaid, PaymentStatus.FirstReminder };
        var paymentMethods = new[] { "Credit Card", "PayPal", "Bank Transfer", "Invoice" };

        for (int i = 1; i <= 20; i++)
        {
            var customer = customers[random.Next(customers.Count)];
            var subtotal = (decimal)(random.NextDouble() * 1000 + 50); // 50-1050€
            var shippingCost = (decimal)(random.NextDouble() * 20 + 5); // 5-25€
            var totalTax = subtotal * 0.19m; // 19% VAT
            var total = subtotal + shippingCost + totalTax;

            orders.Add(new Domain.Entities.Order
            {
                CustomerId = customer.Id,
                RemoteOrderId = $"DEMO-{i:D4}",
                SalesChannelId = 1,
                Status = orderStatuses[random.Next(orderStatuses.Length)],
                PaymentStatus = paymentStatuses[random.Next(paymentStatuses.Length)],
                PaymentMethod = paymentMethods[random.Next(paymentMethods.Length)],
                PaymentProvider = "Demo Provider",
                PaymentTransactionId = $"TXN-{Guid.NewGuid().ToString()[..8]}",
                CustomerNote = i % 5 == 0 ? "Please deliver carefully" : "",
                InternalNote = i % 3 == 0 ? "Priority customer" : "",
                Subtotal = Math.Round(subtotal, 2),
                ShippingCost = Math.Round(shippingCost, 2),
                TotalTax = Math.Round(totalTax, 2),
                Total = Math.Round(total, 2),
                DeliveryAddressFirstName = customer.Firstname,
                DeliveryAddressLastName = customer.Lastname,
                DeliveryAddressCompanyName = customer.CompanyName,
                DeliveryAddressPhone = customer.Phone,
                DeliveryAddressStreet = $"Demo Street {random.Next(1, 100)}",
                DeliveryAddressCity = "Demo City",
                DeliverAddressZip = $"{random.Next(10000, 99999)}",
                DeliveryAddressCountry = "Germany",
                InvoiceAddressFirstName = customer.Firstname,
                InvoiceAddressLastName = customer.Lastname,
                InvoiceAddressCompanyName = customer.CompanyName,
                InvoiceAddressPhone = customer.Phone,
                InvoiceAddressStreet = $"Demo Street {random.Next(1, 100)}",
                InvoiceAddressCity = "Demo City",
                InvoiceAddressZip = $"{random.Next(10000, 99999)}",
                InvoiceAddressCountry = "Germany",
                OrderConfirmationSent = random.Next(2) == 1,
                InvoiceSent = random.Next(2) == 1,
                ShippingInformationSent = random.Next(2) == 1,
                DateOrdered = baseDate.AddDays(random.Next(0, 20))
            });
        }

        return orders;
    }

    private List<Domain.Entities.TaxClass> GetDemoTaxClasses()
    {
        return new List<Domain.Entities.TaxClass>
        {
            new() { TaxRate = 19.0 }, // Standard VAT in Germany
            new() { TaxRate = 7.0 },  // Reduced VAT in Germany
            new() { TaxRate = 0.0 }   // Tax-free
        };
    }
}