using System.Collections.Immutable;
using maERP.Client.Core.Exceptions;
using maERP.Client.Features.SalesChannels.Services;
using maERP.Domain.Dtos.SalesChannel;

namespace maERP.Client.Features.SalesChannels.Models;

/// <summary>
/// Navigation data for SalesChannelDetailModel.
/// </summary>
public record SalesChannelDetailData(Guid SalesChannelId);

/// <summary>
/// Model for sales channel detail page using MVUX pattern.
/// Receives sales channel ID from navigation data.
/// </summary>
public partial record SalesChannelDetailModel
{
    private readonly ISalesChannelService _salesChannelService;
    private readonly INavigator _navigator;
    private readonly Guid _salesChannelId;

    public SalesChannelDetailModel(
        ISalesChannelService salesChannelService,
        INavigator navigator,
        SalesChannelDetailData data)
    {
        _salesChannelService = salesChannelService;
        _navigator = navigator;
        _salesChannelId = data.SalesChannelId;
    }

    /// <summary>
    /// Feed that loads the sales channel details.
    /// </summary>
    public IFeed<SalesChannelDetailDto> SalesChannel => Feed.Async(async ct =>
    {
        var salesChannel = await _salesChannelService.GetSalesChannelAsync(_salesChannelId, ct);
        return salesChannel ?? throw new InvalidOperationException($"Sales channel {_salesChannelId} not found");
    });

    /// <summary>Recent sync-run audit history.</summary>
    public IListFeed<ChannelSyncRunDto> SyncRuns => ListFeed.Async<ChannelSyncRunDto>(async ct =>
    {
        var runs = await _salesChannelService.GetSyncRunsAsync(_salesChannelId, take: 25, offset: 0, ct);
        return runs.ToImmutableList();
    });

    /// <summary>Outbox rows currently in DeadLetter — surfaced for manual retry.</summary>
    public IListFeed<ChannelExportOutboxDto> DeadLetterRows => ListFeed.Async<ChannelExportOutboxDto>(async ct =>
    {
        var rows = await _salesChannelService.GetDeadLetterAsync(_salesChannelId, ct);
        return rows.ToImmutableList();
    });

    /// <summary>User-facing status line for the most recent orchestration action.</summary>
    public IState<string> StatusMessage => State<string>.Value(this, () => string.Empty);

    /// <summary>Set true while an orchestration action is in flight, so XAML can disable buttons.</summary>
    public IState<bool> IsBusy => State<bool>.Value(this, () => false);

    public Task TriggerSyncProducts() => RunAsync("Products import", ct => _salesChannelService.TriggerSyncAsync(_salesChannelId, "products", ct));
    public Task TriggerSyncSaless() => RunAsync("Saless import", ct => _salesChannelService.TriggerSyncAsync(_salesChannelId, "saless", ct));
    public Task TriggerSyncCustomers() => RunAsync("Customers import", ct => _salesChannelService.TriggerSyncAsync(_salesChannelId, "customers", ct));

    public async Task TestConnection()
    {
        await IsBusy.SetAsync(true);
        try
        {
            var result = await _salesChannelService.TestConnectionAsync(_salesChannelId);
            var ok = result?.Success == true;
            var message = ok ? "Connected." : (result?.Message ?? "Test failed.");
            await StatusMessage.SetAsync($"Test connection: {(ok ? "OK" : "FAIL")} — {message}");
        }
        catch (ApiException ex)
        {
            await StatusMessage.SetAsync($"Test connection failed: {ex.CombinedMessage}");
        }
        finally
        {
            await IsBusy.SetAsync(false);
        }
    }

    public async Task RetryDeadLetter(ChannelExportOutboxDto row)
    {
        await IsBusy.SetAsync(true);
        try
        {
            await _salesChannelService.RetryDeadLetterAsync(_salesChannelId, row.Id);
            await StatusMessage.SetAsync($"Re-queued {row.Operation} ({row.AggregateId:N}).");
        }
        catch (ApiException ex)
        {
            await StatusMessage.SetAsync($"Retry failed: {ex.CombinedMessage}");
        }
        finally
        {
            await IsBusy.SetAsync(false);
        }
    }

    /// <summary>
    /// Navigate to edit sales channel page.
    /// </summary>
    public async Task EditSalesChannel()
    {
        await _navigator.NavigateDataAsync(this, new SalesChannelEditData(_salesChannelId));
    }

    /// <summary>
    /// Navigate back to sales channel list.
    /// </summary>
    public async Task GoBack()
    {
        await _navigator.NavigateBackAsync(this);
    }

    private async Task RunAsync(string label, Func<CancellationToken, Task<SalesChannelSyncResultDto?>> action)
    {
        await IsBusy.SetAsync(true);
        try
        {
            var result = await action(CancellationToken.None);
            var summary = result is null
                ? "no result"
                : $"{result.Status} — processed {result.ItemsProcessed}, failed {result.ItemsFailed}";
            await StatusMessage.SetAsync($"{label}: {summary}");
        }
        catch (ApiException ex)
        {
            await StatusMessage.SetAsync($"{label} failed: {ex.CombinedMessage}");
        }
        finally
        {
            await IsBusy.SetAsync(false);
        }
    }
}
