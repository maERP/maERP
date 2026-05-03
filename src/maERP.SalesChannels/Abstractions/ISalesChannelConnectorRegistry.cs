using maERP.Domain.Enums;

namespace maERP.SalesChannels.Abstractions;

/// <summary>
/// Resolves the connector for a given <see cref="SalesChannelType"/>. Channel-specific
/// <c>if/switch</c> statements live here and only here — handlers, the orchestrator, and the
/// outbox drainer all go through this registry.
/// </summary>
public interface ISalesChannelConnectorRegistry
{
    /// <summary>Returns the connector for the given type, or null if none is registered.</summary>
    ISalesChannelConnector? Resolve(SalesChannelType type);

    /// <summary>Returns the connector for the given type, throwing if none is registered.</summary>
    ISalesChannelConnector Get(SalesChannelType type);

    IEnumerable<ISalesChannelConnector> All();
}
