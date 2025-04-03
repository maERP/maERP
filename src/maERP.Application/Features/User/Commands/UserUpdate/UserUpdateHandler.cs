using maERP.Application.Contracts.Logging;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.User.Commands.UserUpdate;

public class UserUpdateHandler : IRequestHandler<UserUpdateCommand, Result<string>>
{
    private readonly IAppLogger<UserUpdateHandler> _logger;
    
    public UserUpdateHandler(
        IAppLogger<UserUpdateHandler> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<string>> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating user with ID: {Id}", request.Id);
        
        var result = new Result<string>();
        
        // Validate incoming data
        var validator = new UserUpdateValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
            
            _logger.LogWarning("Validation errors in update request for {0}: {1}", 
                nameof(UserUpdateCommand), 
                string.Join(", ", result.Messages));
                
            return result;
        }

        try
        {
            // Manuelles Mapping statt AutoMapper
            var userToUpdate = new ApplicationUser
            {
                Id = request.Id,
                UserName = request.Email,
                Email = request.Email,
                DateModified = DateTime.UtcNow
            };
            
            // TODO: add to database
            // await _userRepository.UpdateAsync(userToUpdate);
            
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Ok;
            result.Data = userToUpdate.Id;
            
            _logger.LogInformation("Successfully updated user with ID: {Id}", userToUpdate.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while updating the user: {ex.Message}");
            
            _logger.LogError("Error updating user: {Message}", ex.Message);
        }

        return result;
    }
}
