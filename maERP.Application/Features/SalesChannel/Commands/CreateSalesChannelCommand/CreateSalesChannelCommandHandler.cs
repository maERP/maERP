using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Features.SalesChannel.Commands.CreateSalesChannelCommand;
using MediatR;

namespace maERP.Application.Features.SalesChannel.Commands.CreateSalesChannelCommand;

public class CreateSalesChannelCommandHandler : IRequestHandler<CreateSalesChannelCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<CreateSalesChannelCommandHandler> _logger;
    private readonly ISalesChannelRepository _salesChannelRepository;

    public CreateSalesChannelCommandHandler(IMapper mapper,
        IAppLogger<CreateSalesChannelCommandHandler> logger,
        ISalesChannelRepository salesChannelRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _salesChannelRepository = salesChannelRepository;
    }

    public async Task<int> Handle(CreateSalesChannelCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new CreateSalesChannelCommandValidator(_salesChannelRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in create request for {0} - {1}", nameof(CreateSalesChannelCommand), request.TaxRate);
            throw new Exceptions.ValidationException("Invalid SalesChannel", validationResult);
        }

        // convert to domain entity object
        var salesChannelToCreate = _mapper.Map<Domain.Models.SalesChannel>(request);

        // add to database
        await _salesChannelRepository.CreateAsync(salesChannelToCreate);

        // return record id
        return salesChannelToCreate.Id;
    }
}