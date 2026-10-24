using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Doctrine;

public class AttributeValueJsonConverter : JsonConverter<AttributeValue>
{
    public override AttributeValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.Number => AttributeValue.Number(reader.GetDouble()),
            JsonTokenType.True => AttributeValue.Bool(true),
            JsonTokenType.False => AttributeValue.Bool(false),
            JsonTokenType.String => AttributeValue.Text(reader.GetString()!),
            JsonTokenType.Null => default,
            _ => throw new JsonException($"Cannot convert {reader.TokenType} to {nameof(AttributeValue)}.")
        };
    }

    public override void Write(Utf8JsonWriter writer, AttributeValue value, JsonSerializerOptions options)
    {
        if (value.TryGetNumber(out double number))
            writer.WriteNumberValue(number);
        else if (value.TryGetBool(out bool flag))
            writer.WriteBooleanValue(flag);
        else if (value.TryGetText(out string text))
            writer.WriteStringValue(text);
        else
            writer.WriteNullValue();
    }
}
