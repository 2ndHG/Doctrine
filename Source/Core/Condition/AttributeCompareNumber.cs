using System;

namespace Doctrine;

public record AttributeCompareNumber(
    string AttributeKey,
    double TargetValue,
    bool DefaultReturn,
    AttributeComparator Comparator
) : ConditionRule
{
    public override bool Evaluate(CharacterContext targetCharacter)
    {
        if (!targetCharacter.Attributes.TryGetValue(AttributeKey, out AttributeValue attr))
            return DefaultReturn;

        if (!attr.TryGetNumber(out double value))
            throw new InvalidOperationException(
                $"Attribute \"{AttributeKey}\" is {attr}, but {nameof(AttributeCompareNumber)} expects a Number.");

        return Comparator.Compare(value, TargetValue);
    }
}
