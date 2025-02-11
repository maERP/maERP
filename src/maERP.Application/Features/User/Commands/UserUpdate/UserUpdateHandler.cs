using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Exceptions;
using maERP.Domain.Entities;
using MediatR;

namespace maERP.Application.Features.User.Commands.UserUpdate;

public class UserUpdateHandler : IRequestHandler<UserUpdateCommand, string>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<UserUpdateHandler> _logger;
    
    public UserUpdateHandler(IMapper mapper,
        IAppLogger<UserUpdateHandler> logger)
    {
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<string> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new UserUpdateValidator();
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(UserUpdateCommand), request.Id);
            throw new ValidationException("Invalid User", validationResult);
        }

        // convert to domain entity object
        var userToUpdate = _mapper.Map<ApplicationUser>(request);
        
        // TODO  add to database
        // await _userRepository.UpdateAsync(userToUpdate);

        // return record id
        return userToUpdate.Id.ToString();
    }
}
