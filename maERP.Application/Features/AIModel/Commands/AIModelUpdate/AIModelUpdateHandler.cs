﻿using AutoMapper;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Exceptions;
using MediatR;

namespace maERP.Application.Features.AIModel.Commands.AIModelUpdate;

public class AIModelUpdateHandler : IRequestHandler<AIModelUpdateCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<AIModelUpdateHandler> _logger;
    private readonly IAIModelRepository _aiModelRepository;


    public AIModelUpdateHandler(IMapper mapper,
        IAppLogger<AIModelUpdateHandler> logger,
        IAIModelRepository aiModelRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _aiModelRepository = aiModelRepository;
    }

    public async Task<int> Handle(AIModelUpdateCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new AIModelUpdateValidator(_aiModelRepository);
        var validationResult = await validator.ValidateAsync(request);

        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(AIModelUpdateCommand), request.Name);
            throw new ValidationException("Invalid AIModel", validationResult);
        }
        
        var aiModelToUpdate = _mapper.Map<Domain.Entities.AIModel>(request);
        
        await _aiModelRepository.UpdateAsync(aiModelToUpdate);

        return aiModelToUpdate.Id;
    }
}