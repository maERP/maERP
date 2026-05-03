using maERP.Domain.Enums;

namespace maERP.SalesChannels.Abstractions;

public sealed class SalesChannelConnectorRegistry : ISalesChannelConnectorRegistry
{
    private readonly Dictionary<SalesChannelType, ISalesChannelConnector> _byType;

    public SalesChannelConnectorRegistry(IEnumerable<ISalesChannelConnector> connectors)
    {
        _byType = connectors.ToDictionary(c => c.Type);
    }

    public ISalesChannelConnector? Resolve(SalesChannelType type)
        => _byType.TryGetValue(type, out var connector) ? connector : null;

    public ISalesChannelConnector Get(SalesChannelType type)
        => Resolve(type) ?? throw new InvalidOperationException(
            $"No connector registered for SalesChannelType.{type}. " +
            "Did you forget to add it to SalesChannelServiceRegistration?");

    public IEnumerable<ISalesChannelConnector> All() => _byType.Values;
}
