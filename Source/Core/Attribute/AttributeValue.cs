using System.Text.Json.Serialization;

namespace Doctrine;

[JsonConverter(typeof(AttributeValueJsonConverter))]
public readonly record struct AttributeValue
{
    public AttributeType Type { get; }


    public static AttributeValue Number(double value) => new(AttributeType.Number, value, null);
    public static AttributeValue Bool(bool value) => new(AttributeType.Bool, value ? 1d : 0d, null);
    public static AttributeValue Text(string value) => new(AttributeType.String, 0d, value);

    public static implicit operator AttributeValue(double value) => Number(value);
    public static implicit operator AttributeValue(bool value) => Bool(value);
    public static implicit operator AttributeValue(string value) => Text(value);

    public bool IsNone => Type == AttributeType.None;
    private AttributeValue(AttributeType type, double number, string? text)
    { 
        (Type, _number, _text) = (type, number, text); 
    }
    private readonly double _number;
    private readonly string? _text;

    public bool TryGetNumber(out double value)
    {
        value = _number;
        return Type == AttributeType.Number;
    }

    public bool TryGetBool(out bool value)
    {
        value = _number != 0d;
        return Type == AttributeType.Bool;
    }

    public bool TryGetText(out string value)
    {
        value = _text ?? "";
        return Type == AttributeType.String;
    }

    public override string ToString() => Type switch
    {
        AttributeType.Number => $"Number({_number})",
        AttributeType.Bool => $"Bool({_number != 0d})",
        AttributeType.String => $"Text(\"{_text}\")",
        _ => "None"
    };
}
