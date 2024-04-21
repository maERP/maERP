using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.User.Commands.UpdateUserCommand;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, string>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<UpdateUserCommandHandler> _logger;
    
    public UpdateUserCommandHandler(IMapper mapper,
        IAppLogger<UpdateUserCommandHandler> logger)
    {
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<string> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new UpdateUserCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(CreateUserCommand), request.Id);
            throw new ValidationException("Invalid User", validationResult);
        }

        // convert to domain entity object
        var userToUpdate = _mapper.Map<Domain.Models.User>(request);
        
        // TODO  add to database
        // await _userRepository.UpdateAsync(userToUpdate);

        // return record id
        return userToUpdate.Id;
    }
}
