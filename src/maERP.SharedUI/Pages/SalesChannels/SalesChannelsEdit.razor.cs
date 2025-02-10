using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Dtos.Warehouse;
using maERP.SharedUI.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.SalesChannels
{
    public partial class SalesChannelsEdit
    {
        [Inject]
        public required NavigationManager NavigationManager { get; set; }

        [Inject]
        public required IHttpService HttpService { get; set; }

        [Inject]
        public required ISnackbar Snackbar { get; set; }

        [Parameter]
        public int salesChannelId { get; set; }

        // ReSharper disable once NotAccessedField.Local
        private MudForm _form = new();

        protected string Title = "hinzufügen";

        protected SalesChannelDetailDto SalesChannel = new();
        protected List<WarehouseListDto> Warehouses = new();

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
            if (salesChannelId != 0)
            {
                await HttpService.PutAsync<SalesChannelDetailDto, SalesChannelDetailDto>("/api/v1/SalesChannels", SalesChannel);
            }
            else
            {
                await HttpService.PostAsync<SalesChannelDetailDto, SalesChannelDetailDto>("/api/v1/SalesChannels", SalesChannel);
            }
        }

        public void ReturnToList()
        {
            NavigationManager.NavigateTo("/SalesChannels");
        }
    }
}