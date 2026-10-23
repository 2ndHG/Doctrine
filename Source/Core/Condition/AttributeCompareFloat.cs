namespace MyGame;

public record AttributeCompareFloat(
    string AttributeKey,
    float TargetValue,
    bool DefaultReturn,
    AttributeComparator Comparator
) : Condition
{
    public override bool Evaluate(BattleContext battleContext, CharacterContext targetCharacter)
    {
        if (targetCharacter.Attributes.TryGetValue(AttributeKey, out object? attr)
            && attr is float floatValue
        )
        {
            return Comparator switch
            {
                AttributeComparator.Greater => floatValue > TargetValue,
                AttributeComparator.GreaterEqual => floatValue >= TargetValue,
                AttributeComparator.Less => floatValue < TargetValue,
                AttributeComparator.LessEqual => floatValue <= TargetValue,
                AttributeComparator.Equal => floatValue == TargetValue,
                AttributeComparator.NotEqual => floatValue != TargetValue,
                _ => false
            };
        }
        return DefaultReturn;

    }
}