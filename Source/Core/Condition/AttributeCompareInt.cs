namespace MyGame;

public record AttributeCompareInt(
    string AttributeKey,
    int TargetValue,
    int DefaultValue,
    AttributeComparator Comparator
) : Condition
{
    public override bool Evaluate(BattleContext battleContext, CharacterContext targetCharacter)
    {
        float value = DefaultValue;
        if (targetCharacter.Attributes.TryGetValue(AttributeKey, out object? attr)
            && attr is int floatValue)
            value = floatValue;

        return Comparator switch
        {
            AttributeComparator.Greater => value > TargetValue,
            AttributeComparator.GreaterEqual => value >= TargetValue,
            AttributeComparator.Less => value < TargetValue,
            AttributeComparator.LessEqual => value <= TargetValue,
            AttributeComparator.Equal => value == TargetValue,
            AttributeComparator.NotEqual => value != TargetValue,
            _ => false
        };
    }
}