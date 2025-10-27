using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using maERP.Domain.Dtos.Warehouse;

namespace maERP.UI.Shared.Services;

public interface IDialogService
{
    Task<bool> ShowConfirmationDialogAsync(string title, string message, string confirmText = "Ja", string cancelText = "Abbrechen", string icon = "❓");
    Task<Guid?> ShowWarehouseSelectionDialogAsync(string title, string message, List<WarehouseListDto> warehouses);
}
