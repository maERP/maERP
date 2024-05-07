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
        public required NavigationManager _navigationManager { get; set; }

        [Inject]
        public required ISalesChannelService _salesChannelService { get; set; }

        [Inject]
        public required IWarehouseService _warehouseService { get; set; }

        [Inject]
        public required ISnackbar _snackbar { get; set; }

        [Parameter]
        public int salesChannelId { get; set; }

        private MudForm _form = new();

        protected string Title = "hinzufügen";

        protected SalesChannelVM salesChannel = new();
        protected List<WarehouseVM> warehouses = new();

        protected override async Task OnParametersSetAsync()
        {
            warehouses = await _warehouseService.GetWarehouses();

            if (salesChannelId > 0)
            {
                salesChannel = await _salesChannelService.GetSalesChannelDetails(salesChannelId);
            }
        }

        protected async Task Save()
        {
            Response<Guid> response = new();

            if (salesChannelId != 0)
            {
                response = await _salesChannelService.UpdateSalesChannel(salesChannelId, salesChannel);
            }
            else
            {
                await _salesChannelService.CreateSalesChannel(salesChannel);
            }

            if (response.Success)
            {
                _snackbar.Add("Vertriebskanal gespeichert", Severity.Success);
                ReturnToList();
            }
            else
            {
                _snackbar.Add("Fehler beim Speichern des Vertriebskanals", Severity.Error);

                foreach (var error in response.ValidationErrors)
                {
                    _snackbar.Add(error.ToString(), Severity.Error);
                }
            }
        }

        public void ReturnToList()
        {
            _navigationManager.NavigateTo("/SalesChannels");
        }
    }
}