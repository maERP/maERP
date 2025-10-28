using maERP.Application.Features.SalesChannel.Commands.SalesChannelCreate;
using maERP.Application.Features.SalesChannel.Commands.SalesChannelUpdate;
using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Wrapper;

namespace maERP.Client.Services.Api.Clients;

/// <summary>
/// Implementation of sales channels API client
/// </summary>
public class SalesChannelsApiClient : ApiClientBase, ISalesChannelsApiClient
{
    private const string BaseEndpoint = "api/v1/SalesChannels";

    public SalesChannelsApiClient(HttpClient httpClient, ILogger<SalesChannelsApiClient> logger)
        : base(httpClient, logger)
    {
    }

    public async Task<PaginatedResult<SalesChannelListDto>?> GetAllAsync(
        int pageNumber = 0,
        int pageSize = 10,
        string searchString = "",
        string orderBy = "",
        CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string?>
        {
            { "pageNumber", pageNumber.ToString() },
            { "pageSize", pageSize.ToString() },
            { "searchString", searchString },
            { "orderBy", orderBy }
        };

        var url = BuildUrl(BaseEndpoint, queryParams);
        return await GetAsync<PaginatedResult<SalesChannelListDto>>(url, cancellationToken);
    }

    public async Task<SalesChannelDetailDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await GetAsync<SalesChannelDetailDto>($"{BaseEndpoint}/{id}", cancellationToken);
    }

    public async Task<HttpResponseMessage> CreateAsync(
        SalesChannelCreateCommand command,
        CancellationToken cancellationToken = default)
    {
        return await PostAsync(BaseEndpoint, command, cancellationToken);
    }

    public async Task<HttpResponseMessage> UpdateAsync(
        Guid id,
        SalesChannelUpdateCommand command,
        CancellationToken cancellationToken = default)
    {
        return await PutAsync($"{BaseEndpoint}/{id}", command, cancellationToken);
    }

    public async Task<HttpResponseMessage> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DeleteAsync($"{BaseEndpoint}/{id}", cancellationToken);
    }
}
