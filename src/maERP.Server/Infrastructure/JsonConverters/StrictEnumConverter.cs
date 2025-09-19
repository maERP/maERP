using System.Text.Json;
using System.Text.Json.Serialization;

namespace maERP.Server.Infrastructure.JsonConverters;

/// <summary>
/// A strict enum converter that throws an exception when invalid enum values are encountered.
/// This prevents invalid numeric enum values from being silently converted to the default value.
/// </summary>
/// <typeparam name="T">The enum type</typeparam>
public class StrictEnumConverter<T> : JsonConverter<T> where T : struct, Enum
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var stringValue = reader.GetString();
            if (Enum.TryParse<T>(stringValue, ignoreCase: true, out var enumValue) && Enum.IsDefined(typeof(T), enumValue))
            {
                return enumValue;
            }
            throw new JsonException($"Invalid enum value '{stringValue}' for type {typeof(T).Name}");
        }
        
        if (reader.TokenType == JsonTokenType.Number)
        {
            var numericValue = reader.GetInt32();
            if (Enum.IsDefined(typeof(T), numericValue))
            {
                return (T)Enum.ToObject(typeof(T), numericValue);
            }
            throw new JsonException($"Invalid enum value '{numericValue}' for type {typeof(T).Name}");
        }

        throw new JsonException($"Unexpected token type {reader.TokenType} when parsing enum {typeof(T).Name}");
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(Convert.ToInt32(value));
    }
}