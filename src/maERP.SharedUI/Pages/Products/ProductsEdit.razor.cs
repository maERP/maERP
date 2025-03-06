using System.Net.Http.Json;
using maERP.Domain.Dtos.Product;
using maERP.Domain.Wrapper;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Validators;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace maERP.SharedUI.Pages.Products;

public partial class ProductsEdit
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }
    
    [Inject]
    public required ISnackbar Snackbar { get; set; }

    [Inject]
    public required IHttpService HttpService { get; set; }

    [Inject]
    public required ProductInputValidator Validator { get; set; }

    [Parameter]
    public int productId { get; set; }
    
    public MudForm? Form;

    protected string Title = string.Empty;

    public ProductInputDto Product = new();

    public bool ProductAiHelperVisible;

    protected override async Task OnInitializedAsync()
    {
        if (productId == 0)
        {
            Title = "Produkt hinzufügen";
        }
        else
        {
            Title = "Produkt bearbeiten";
            
            var result = await HttpService.GetAsync<Result<ProductInputDto>>($"/api/v1/Products/{productId}");
            
            if (result != null && result.Succeeded)
            {
                Product = result.Data;
            }
        }
        
        StateHasChanged();
    }

    protected async Task Save()
    {
        HttpResponseMessage httpResponseMessage;

        if (productId == 0)
        {
            httpResponseMessage = await HttpService.PostAsJsonAsync("/api/v1/Products", Product);
        }
        else
        {
            httpResponseMessage = await HttpService.PutAsJsonAsync($"/api/v1/Products/{productId}", Product);
        }

        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Result<int>>() ?? null;
        
        if (result != null)
        {
            if (result.Succeeded)
            {
                Snackbar.Add("Produkt gespeichert", Severity.Success);
                NavigateToList();
            }
            else
            {
                foreach (var errorMessage in result.Messages)
                {
                    Snackbar.Add("SERVER: " + errorMessage, Severity.Error);
                }
            }
        }
        else
        {
            Snackbar.Add("Produkt konnte nicht gespeichert werden", Severity.Error);
        }
    }

    protected async Task OnValidSubmit()
    {
        if (Form is not null)
        {
            await Form.Validate();
            
            if (Form.IsValid)
            {
                await Save();
            }
        }
    }
    
    public void NavigateToList()
    {
        StateHasChanged();
        NavigationManager.NavigateTo("/Products");
    }
    
    public void OpenProductAiHelper()
    {
        ProductAiHelperVisible = true;
    }
}