using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.DemoData.Commands.AiDemoData;

public class AiDemoDataHandler : IRequestHandler<AiDemoDataCommand, Result<string>>
{
    private readonly IAppLogger<AiDemoDataHandler> _logger;
    private readonly IAiModelRepository _aiModelRepository;
    private readonly IAiPromptRepository _aiPromptRepository;

    public AiDemoDataHandler(
        IAppLogger<AiDemoDataHandler> logger,
        IAiModelRepository aiModelRepository,
        IAiPromptRepository aiPromptRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _aiModelRepository = aiModelRepository ?? throw new ArgumentNullException(nameof(aiModelRepository));
        _aiPromptRepository = aiPromptRepository ?? throw new ArgumentNullException(nameof(aiPromptRepository));
    }

    public async Task<Result<string>> Handle(AiDemoDataCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to create AI demo data (models and prompts)");

        var result = new Result<string>();
        var createdItems = new List<string>();

        try
        {
            // Create AI Models
            var aiModels = GetDemoAiModels();
            foreach (var aiModel in aiModels)
            {
                await _aiModelRepository.CreateAsync(aiModel);
            }
            createdItems.Add($"{aiModels.Count} AI models");

            // Create AI Prompts for the first model
            var aiPrompts = GetDemoAiPrompts(aiModels.First().Id);
            foreach (var aiPrompt in aiPrompts)
            {
                await _aiPromptRepository.CreateAsync(aiPrompt);
            }
            createdItems.Add($"{aiPrompts.Count} AI prompts");

            result.Succeeded = true;
            result.StatusCode = ResultStatusCode.Created;
            result.Data = $"Successfully created: {string.Join(", ", createdItems)}";

            _logger.LogInformation("Successfully created AI demo data: {Items}", string.Join(", ", createdItems));
        }
        catch (Exception ex)
        {
            result.Succeeded = false;
            result.StatusCode = ResultStatusCode.InternalServerError;
            result.Messages.Add($"An error occurred while creating AI demo data: {ex.Message}");

            _logger.LogError("Error creating AI demo data: {Message}", ex.Message);
        }

        return result;
    }

    private List<Domain.Entities.AiModel> GetDemoAiModels()
    {
        return new List<Domain.Entities.AiModel>
        {
            new()
            {
                AiModelType = AiModelType.Ollama,
                Name = "Llama 3.1 8B",
                ApiUrl = "http://localhost:11434",
                ApiKey = "",
                ApiUsername = "",
                ApiPassword = "",
                NCtx = 8192
            },
            new()
            {
                AiModelType = AiModelType.ChatGpt4O,
                Name = "GPT-4o",
                ApiUrl = "https://api.openai.com/v1/chat/completions",
                ApiKey = "your-openai-api-key-here",
                ApiUsername = "",
                ApiPassword = "",
                NCtx = 128000
            },
            new()
            {
                AiModelType = AiModelType.Claude35,
                Name = "Claude 3.5 Sonnet",
                ApiUrl = "https://api.anthropic.com/v1/messages",
                ApiKey = "your-anthropic-api-key-here",
                ApiUsername = "",
                ApiPassword = "",
                NCtx = 200000
            },
            new()
            {
                AiModelType = AiModelType.LmStudio,
                Name = "Local LM Studio",
                ApiUrl = "http://localhost:1234/v1",
                ApiKey = "lm-studio",
                ApiUsername = "",
                ApiPassword = "",
                NCtx = 4096
            },
            new()
            {
                AiModelType = AiModelType.Ollama,
                Name = "Llama 3.1 70B",
                ApiUrl = "http://localhost:11434",
                ApiKey = "",
                ApiUsername = "",
                ApiPassword = "",
                NCtx = 32768
            }
        };
    }

    private List<Domain.Entities.AiPrompt> GetDemoAiPrompts(int aiModelId)
    {
        return new List<Domain.Entities.AiPrompt>
        {
            new()
            {
                AiModelId = aiModelId,
                Identifier = "product-description",
                PromptText = "You are an expert product description writer for e-commerce. Create compelling, SEO-optimized product descriptions that highlight key features and benefits. Focus on clarity, persuasive language, and technical accuracy."
            },
            new()
            {
                AiModelId = aiModelId,
                Identifier = "customer-support",
                PromptText = "You are a helpful customer support assistant for an ERP system. Provide clear, professional, and empathetic responses to customer inquiries. Always aim to resolve issues efficiently while maintaining a friendly tone."
            },
            new()
            {
                AiModelId = aiModelId,
                Identifier = "order-analysis",
                PromptText = "You are a business analyst specializing in order data analysis. Analyze order patterns, identify trends, and provide actionable insights for business improvement. Focus on data-driven recommendations."
            },
            new()
            {
                AiModelId = aiModelId,
                Identifier = "inventory-optimization",
                PromptText = "You are an inventory management expert. Analyze stock levels, predict demand, and recommend optimal inventory strategies. Consider seasonal variations, lead times, and storage costs in your recommendations."
            },
            new()
            {
                AiModelId = aiModelId,
                Identifier = "email-template",
                PromptText = "You are a professional email writer for business communications. Create clear, concise, and professional email templates for various business scenarios including order confirmations, shipping notifications, and customer follow-ups."
            },
            new()
            {
                AiModelId = aiModelId,
                Identifier = "financial-report",
                PromptText = "You are a financial analyst specializing in ERP data analysis. Generate comprehensive financial reports with insights on revenue trends, profit margins, and cost analysis. Present data in a clear, executive-friendly format."
            },
            new()
            {
                AiModelId = aiModelId,
                Identifier = "supplier-communication",
                PromptText = "You are a procurement specialist skilled in supplier relationship management. Draft professional communications for supplier negotiations, order requests, and quality discussions. Maintain a collaborative yet assertive tone."
            },
            new()
            {
                AiModelId = aiModelId,
                Identifier = "sales-forecast",
                PromptText = "You are a sales forecasting expert with deep knowledge of market trends and business cycles. Analyze historical sales data and provide accurate forecasts with confidence intervals and key assumptions."
            },
            new()
            {
                AiModelId = aiModelId,
                Identifier = "quality-control",
                PromptText = "You are a quality assurance specialist for product management. Create detailed quality control checklists, identify potential issues, and recommend improvement processes. Focus on preventing defects and ensuring customer satisfaction."
            },
            new()
            {
                AiModelId = aiModelId,
                Identifier = "marketing-content",
                PromptText = "You are a marketing content creator specializing in B2B communications. Develop engaging marketing materials that highlight product benefits, address customer pain points, and drive conversions. Maintain brand consistency and professional tone."
            }
        };
    }
}