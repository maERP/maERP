using maERP.Domain.Dtos.Customer;

namespace maERP.Client.Features.Customers.Models;

public partial record CustomerListModel
{
    private readonly ICustomersApiClient _customersApiClient;
    private readonly INavigator _navigator;

    public CustomerListModel(
        ICustomersApiClient customersApiClient,
        INavigator navigator)
    {
        _customersApiClient = customersApiClient;
        _navigator = navigator;
    }

    public IState<string> SearchText => State.Value(this, () => string.Empty);

    public IListFeed<CustomerListDto> Customers => ListFeed.Async(async ct =>
    {
        var searchText = await SearchText;
        var result = await _customersApiClient.GetAllAsync(
            pageNumber: 0,
            pageSize: 100,
            searchString: searchText ?? string.Empty,
            cancellationToken: ct);

        return (result?.Data ?? new List<CustomerListDto>()).ToImmutableList();
    });

    public IState<bool> IsLoading => State.Value(this, () => false);

    public async ValueTask AddCustomer()
    {
        // TODO: Navigate to customer create page when implemented
        await Task.CompletedTask;
    }

    public async ValueTask EditCustomer(Guid customerId)
    {
        // TODO: Navigate to customer edit page when implemented
        await Task.CompletedTask;
    }

    public async ValueTask DeleteCustomer(Guid customerId)
    {
        // TODO: Implement customer deletion with confirmation dialog
        await Task.CompletedTask;
    }
}
