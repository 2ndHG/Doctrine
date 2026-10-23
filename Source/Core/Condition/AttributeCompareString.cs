using System;

namespace MyGame;

public record AttributeCompareString(
    string AttributeKey,
    string? TargetValue,
    bool DefaultReturn,
    AttributeComparator Comparator
) : ConditionRule
{
    public override bool Evaluate(BattleContext battleContext, CharacterContext targetCharacter)
    {
        if (!targetCharacter.Attributes.TryGetValue(AttributeKey, out AttributeValue attr))
            return DefaultReturn;

        if (!attr.TryGetText(out string value))
            throw new InvalidOperationException(
                $"Attribute \"{AttributeKey}\" is {attr}, but {nameof(AttributeCompareString)} expects a String.");

        return Comparator switch
        {
            AttributeComparator.Equal => value == TargetValue,
            AttributeComparator.NotEqual => value != TargetValue,
            _ => false
        };
    }
}
