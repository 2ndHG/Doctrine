namespace MyGame;

public record AttributeCompareString(
    string AttributeKey, 
    string? TargetValue, 
    string? DefaultValue, 
    AttributeComparator Comparator
) : Condition
{
    public override bool Evaluate(BattleContext battleContext, CharacterContext targetCharacter)
    {
        string? value = DefaultValue;
        if (targetCharacter.Attributes.TryGetValue(AttributeKey, out object? attr)
            && attr is string stringValue)
            value = stringValue;
        return Comparator switch
        {
            AttributeComparator.Equal => value == TargetValue,
            AttributeComparator.NotEqual => value != TargetValue,
            _ => false
        };
    }
}