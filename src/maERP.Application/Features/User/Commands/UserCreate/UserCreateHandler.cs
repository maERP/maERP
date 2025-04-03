using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.User.Commands.UserCreate;

public class UserCreateHandler : IRequestHandler<UserCreateCommand, Result<string>>
{
    private readonly IAppLogger<UserCreateHandler> _logger;
    private readonly IUserRepository _userRepository;

    public UserCreateHandler(
        IAppLogger<UserCreateHandler> logger,
        IUserRepository userRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<Result<string>> Handle(UserCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new user with email: {Email}", request.Email);
        
        var result = new Result<string>();
        
        // Validate incoming data
        var validator = new UserCreateValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.BadRequest;
            result.Messages.AddRange(validationResult.Errors.Select(e => e.ErrorMessage));
            
            _logger.LogWarning("Validation errors in create request for {0}: {1}", 
                nameof(UserCreateCommand), 
                string.Join(", ", result.Messages));
                
            return result;
        }

        try
        {
            // Manuelles Mapping statt AutoMapper
            var userToCreate = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            };
            
            // add to database
            await _userRepository.CreateAsync(userToCreate, request.Password);

            // return record id
            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Created;
            result.Data = userToCreate.Id;
            
            _logger.LogInformation("Successfully created user with ID: {Id}", userToCreate.Id);
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while creating the user: {ex.Message}");
            
            _logger.LogError("Error creating user: {Message}", ex.Message);
        }

        return result;
    }
}