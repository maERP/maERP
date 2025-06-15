using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Validators;

namespace maERP.Application.Features.SalesChannel.Commands.SalesChannelCreate;

/// <summary>
/// Validator for sales channel creation commands.
/// Extends SalesChannelBaseValidator to inherit common validation rules for sales channel data
/// and adds specific validation for sales channel creation operations.
/// </summary>
public class SalesChannelCreateValidator : SalesChannelBaseValidator<SalesChannelCreateCommand>
{
    /// <summary>
    /// Repository for sales channel data operations
    /// </summary>
    private readonly ISalesChannelRepository _salesChannelRepository;

    /// <summary>
    /// Constructor that initializes the validator with required dependencies
    /// </summary>
    /// <param name="salesChannelRepository">Repository for sales channel data access</param>
    public SalesChannelCreateValidator(ISalesChannelRepository salesChannelRepository)
    {
        _salesChannelRepository = salesChannelRepository;

        // Add rule to check if the sales channel name is unique before creating
        RuleFor(s => s)
            .MustAsync(IsUniqueAsync).WithMessage("SalesChannel with the same name already exists.");
    }

    /// <summary>
    /// Asynchronously checks if a sales channel with the same name already exists in the database
    /// </summary>
    /// <param name="command">The sales channel creation command to validate</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the sales channel name is unique, false otherwise</returns>
    private async Task<bool> IsUniqueAsync(SalesChannelCreateCommand command, CancellationToken cancellationToken)
    {
        var salesChannel = new Domain.Entities.SalesChannel
        {
            Name = command.Name
        };

        return await _salesChannelRepository.SalesChannelIsUniqueAsync(salesChannel);
    }
}