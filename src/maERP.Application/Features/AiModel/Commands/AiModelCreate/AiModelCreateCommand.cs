using System.ComponentModel.DataAnnotations;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using MediatR;

namespace maERP.Application.Features.AiModel.Commands.AiModelCreate;

/// <summary>
/// Command to create a new AI model
/// </summary>
public class AiModelCreateCommand : IRequest<Result<int>>
{
    /// <summary>
    /// The type of AI model (from AiModelType enum)
    /// </summary>
    public int AiModelType { get; set; } = (int)Domain.Enums.AiModelType.None;
    
    /// <summary>
    /// The name of the AI model (required, max 50 characters)
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// API username for authentication (if using username/password authentication)
    /// </summary>
    public string ApiUsername { get; set; } = string.Empty;
    
    /// <summary>
    /// API password for authentication (if using username/password authentication)
    /// </summary>
    public string ApiPassword { get; set; } = string.Empty;
    
    /// <summary>
    /// API key for authentication (if using API key authentication)
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;
}