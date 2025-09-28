using FluentValidation;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace maERP.Application.Features.Superadmin.UserTenants.Commands.RemoveUserFromTenant;

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
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return Result<bool>.Fail(ResultStatusCode.BadRequest, errors);
        }

        // Find the assignment
        var userTenant = await _userTenantRepository.Entities
            .FirstOrDefaultAsync(ut => ut.UserId == request.UserId && ut.TenantId == request.TenantId, cancellationToken);

        if (userTenant == null)
        {
            return Result<bool>.Fail(ResultStatusCode.BadRequest, "User is not assigned to this tenant");
        }

        try
        {
            await _userTenantRepository.DeleteAsync(userTenant);
        }
        catch (DbUpdateConcurrencyException)
        {
            return Result<bool>.Fail(ResultStatusCode.BadRequest, "User is not assigned to this tenant");
        }

        var success = Result<bool>.Success(true, "User successfully removed from tenant");
        success.StatusCode = ResultStatusCode.Ok;
        return success;
    }
}
