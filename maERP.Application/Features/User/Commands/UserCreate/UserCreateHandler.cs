using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using maERP.Domain.Entities;
using MediatR;

namespace maERP.Application.Features.User.Commands.UserCreate;

public class UserCreateHandler : IRequestHandler<UserCreateCommand, string>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<UserCreateHandler> _logger;
    private readonly IUserRepository _userRepository;

    public UserCreateHandler(IMapper mapper,
        IAppLogger<UserCreateHandler> logger,
        IUserRepository userRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<string> Handle(UserCreateCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new UserCreateValidator();
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in create request for {0} - {1}", nameof(UserCreateCommand), request.Email);
            throw new ValidationException("Invalid User", validationResult);
        }

        // convert to domain entity object
        var userToCreate = _mapper.Map<ApplicationUser>(request);

        // add to database
        await _userRepository.CreateAsync(userToCreate, request.Password);

        // return record id
        return userToCreate.Id;
    }
}