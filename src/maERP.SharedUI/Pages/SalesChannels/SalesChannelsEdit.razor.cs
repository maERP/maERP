using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.SalesChannel;
using maERP.SharedUI.Models.Warehouse;
using maERP.SharedUI.Services.Base;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.SalesChannels
{
    public partial class SalesChannelsEdit
    {
        [Inject]
        public required NavigationManager NavigationManager { get; set; }

        [Inject]
        public required ISalesChannelService SalesChannelService { get; set; }

        [Inject]
        public required IWarehouseService WarehouseService { get; set; }

        [Inject]
        public required ISnackbar Snackbar { get; set; }

        [Parameter]
        public int salesChannelId { get; set; }

        // ReSharper disable once NotAccessedField.Local
        private MudForm _form = new();

        protected string Title = "hinzufügen";

        protected SalesChannelVm SalesChannel = new();
        protected List<WarehouseVm> Warehouses = new();

        /*
        protected override async Task OnParametersSetAsync()
        {
            warehouses = await _warehouseService.GetWarehouses();

            if (salesChannelId > 0)
            {
                salesChannel = await _salesChannelService.GetSalesChannelDetails(salesChannelId);
            }
        }
        */

        protected async Task Save()
        {
            Response<Guid> response = new();

            if (salesChannelId != 0)
            {
                response = await SalesChannelService.UpdateSalesChannel(salesChannelId, SalesChannel);
            }
            else
            {
                await SalesChannelService.CreateSalesChannel(SalesChannel);
            }

            if (response.Success)
            {
                Snackbar.Add("Vertriebskanal gespeichert", Severity.Success);
                ReturnToList();
            }
            else
            {
                Snackbar.Add("Fehler beim Speichern des Vertriebskanals", Severity.Error);

                foreach (var error in response.ValidationErrors)
                {
                    Snackbar.Add(error.ToString(), Severity.Error);
                }
            }
        }

        public void ReturnToList()
        {
            NavigationManager.NavigateTo("/SalesChannels");
        }
    }
}