namespace maERP.Client.Core.Models;

/// <summary>
/// Payload for adding a user to a tenant by email.
/// </summary>
internal record AddUserToTenantPayload(string Email);

/// <summary>
/// Payload for assigning a user to a tenant by tenant ID.
/// </summary>
internal record AssignUserToTenantPayload(Guid TenantId);
