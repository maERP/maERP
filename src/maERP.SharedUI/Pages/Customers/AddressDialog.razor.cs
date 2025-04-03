using maERP.Domain.Dtos.CustomerAddress;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Customers;

public partial class AddressDialog
{
    // ReSharper disable once NotAccessedField.Local
    private MudForm? _form;

    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;

    [Parameter] public CustomerAddressListDto Address { get; set; } = new();

    [Parameter] public bool IsNew { get; set; }

    private void Submit()
    {
        // Bei neuen Adressen eine temporäre negative ID vergeben
        if (IsNew)
        {
            // Tempöräre ID generieren (negativ, damit sie sich nicht mit existierenden IDs überschneiden)
            Address.Id = new Random().Next(-1000, -1);
        }

        MudDialog.Close(DialogResult.Ok(Address));
    }

    private void Cancel() => MudDialog.Cancel();
}