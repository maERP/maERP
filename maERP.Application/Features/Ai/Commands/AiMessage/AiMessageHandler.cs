using maERP.Application.Contracts.Ai;
using maERP.Application.Contracts.Logging;
using MediatR;

namespace maERP.Application.Features.Ai.Commands.AiMessage;

public class AiMessageHandler : IRequestHandler<AiMessageCommand, string>
{
    private readonly IAppLogger<AiMessageHandler> _logger;
    private readonly IAiServiceWrapper _aiServiceWrapper;

    public AiMessageHandler(IAppLogger<AiMessageHandler> logger,
        IAiServiceWrapper aiServiceWrapper)
    {
        _logger = logger;
        _aiServiceWrapper = aiServiceWrapper;
    }

    public async Task<string> Handle(AiMessageCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("AiMessageCommand.Handle");
        return await _aiServiceWrapper.AskAsync(request.PromptText);
    }
}
