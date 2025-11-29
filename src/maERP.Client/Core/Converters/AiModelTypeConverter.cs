using Microsoft.UI.Xaml.Data;
using maERP.Domain.Enums;

namespace maERP.Client.Core.Converters;

/// <summary>
/// Converts AiModelType enum values to display-friendly strings.
/// </summary>
public class AiModelTypeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is int intValue)
        {
            var aiModelType = (AiModelType)intValue;
            return aiModelType switch
            {
                AiModelType.None => "-",
                AiModelType.Ollama => "Ollama",
                AiModelType.VLlm => "vLLM",
                AiModelType.LmStudio => "LM Studio",
                AiModelType.ChatGpt4O => "ChatGPT-4o",
                AiModelType.Claude35 => "Claude 3.5",
                _ => aiModelType.ToString()
            };
        }

        if (value is AiModelType enumValue)
        {
            return enumValue switch
            {
                AiModelType.None => "-",
                AiModelType.Ollama => "Ollama",
                AiModelType.VLlm => "vLLM",
                AiModelType.LmStudio => "LM Studio",
                AiModelType.ChatGpt4O => "ChatGPT-4o",
                AiModelType.Claude35 => "Claude 3.5",
                _ => enumValue.ToString()
            };
        }

        return value?.ToString() ?? string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
