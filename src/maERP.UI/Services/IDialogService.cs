using System.Threading.Tasks;
using maERP.UI.Shared.ViewModels;

namespace maERP.UI.Services;

public interface IDialogService
{
    Task<bool> ShowConfirmationDialogAsync(string title, string message, string confirmText = "Ja", string cancelText = "Abbrechen", string icon = "❓");
    Task<System.Guid?> ShowWarehouseSelectionDialogAsync(string title, string message, System.Collections.Generic.List<maERP.Domain.Dtos.Warehouse.WarehouseListDto> warehouses);
}