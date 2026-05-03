using maERP.Domain.Enums;
using maERP.SalesChannels.Abstractions;
using maERP.SalesChannels.Connectors.Common;

namespace maERP.SalesChannels.Connectors.Pos;

/// <summary>
/// Point-of-sale "channel" — fully internal, no remote API. Exists in the registry so the
/// orchestrator/UI can list every channel uniformly. <see cref="Capabilities"/> is
/// <see cref="SalesChannelCapabilities.None"/>; every method falls through to the base
/// no-op responses.
/// </summary>
public sealed class PosConnector : ConnectorBase
{
    public override SalesChannelType Type => SalesChannelType.PointOfSale;

    public override SalesChannelCapabilities Capabilities => SalesChannelCapabilities.None;
}
