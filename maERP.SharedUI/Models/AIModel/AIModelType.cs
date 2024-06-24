using System.ComponentModel;

namespace maERP.SharedUI.Models.AIModel;

public enum AIModelType
{
    [Description("Kein Typ")]
    None,

    [Description("Shopware 5")]
    ChatGPT4o,
    
    [Description("Claude Opus 3.5")]
    Claude35,
}