﻿namespace maERP.Application.Features.AIPrompt.Queries.AIPromptDetail;

public class AIPromptDetailResponse
{
    public int Id { get; set; }
    public string Identifier { get; set; } = string.Empty;
    public string PromptText { get; set; } = string.Empty;
}