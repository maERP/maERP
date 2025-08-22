using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.UserTenant.Commands.RemoveUserFromTenant;

public class RemoveUserFromTenantHandler : IRequestHandler<RemoveUserFromTenantCommand, Result<bool>>
{
    private readonly IUserTenantRepository _userTenantRepository;
    private readonly IValidator<RemoveUserFromTenantCommand> _validator;

    public RemoveUserFromTenantHandler(
        IUserTenantRepository userTenantRepository,
        IValidator<RemoveUserFromTenantCommand> validator)
    {
        _userTenantRepository = userTenantRepository;
        _validator = validator;
    }

    public async Task<Result<bool>> Handle(RemoveUserFromTenantCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return await Result<bool>.FailAsync(validationResult.ToString());
        }

        // Find the assignment
        var userTenant = _userTenantRepository.Entities
            .FirstOrDefault(ut => ut.UserId == request.UserId && ut.TenantId == request.TenantId);

        if (userTenant == null)
        {
            return await Result<bool>.FailAsync("User is not assigned to this tenant");
        }

        await _userTenantRepository.DeleteAsync(userTenant);

        return await Result<bool>.SuccessAsync(true, "User successfully removed from tenant");
    }
}