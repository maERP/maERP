using maERP.SharedUI.Contracts;
using maERP.SharedUI.Models.TaxClass;
using Microsoft.AspNetCore.Components;

namespace maERP.SharedUI.Pages.TaxClasses;

public partial class TaxClasses
{
    [Inject]
    public required NavigationManager _navigationManager { get; set; }

    [Inject]
    public required ITaxClassService _taxClassService { get; set; }

    private ICollection<TaxClassVM>? taxClasses;

    protected override async Task OnInitializedAsync()
    {
        taxClasses = await _taxClassService.GetTaxClasses();
    }
}