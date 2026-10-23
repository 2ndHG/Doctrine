using System;
using System.Text.Json.Serialization;

namespace MyGame;

public class CharacterAttribute
{
    public AttributeType AttributeType { get; init; }
    public string? StringValue { get; set; } = null;

    public double DoubleValue
    {
        get => _numberValue;
        set => _numberValue = value;

    }
    [JsonIgnore]
    public int GetInteger
    {
        get => (int)Math.Floor(_numberValue);
        set => _numberValue = value;
    }
    [JsonIgnore]
    public bool BoolValue
    {
        get => _numberValue != 0;
        set => _numberValue = value ? 1d : 0d;
    }

    private double _numberValue { get; set; }
}