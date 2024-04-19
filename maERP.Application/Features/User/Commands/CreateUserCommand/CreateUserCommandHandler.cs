using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Models;
using MediatR;

namespace maERP.Application.Features.User.Commands.CreateUserCommand;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<CreateUserCommandHandler> _logger;
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IMapper mapper,
        IAppLogger<CreateUserCommandHandler> logger,
        IUserRepository userRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new CreateUserCommandValidator(_userRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in create request for {0} - {1}", nameof(CreateUserCommand), request.Email);
            throw new Exceptions.ValidationException("Invalid User", validationResult);
        }

        // convert to domain entity object
        var userToCreate = _mapper.Map<ApplicationUser>(request);

        // add to database
        await _userRepository.CreateAsync(userToCreate, request.Password);

        // return record id
        return userToCreate.Id;
    }
}