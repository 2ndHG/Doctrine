namespace MyGame;

public class AttributeCompareString : Condition
{
    public string AttributeKey = "";
    public string? TargetValue { get; set; } = "";
    public AttributeComparator Comparator { get; set; }
    public override bool Evaluate(BattleContext battleContext, CharacterContext targetCharacter)
    {
        return targetCharacter.Attribute.TryGetValue(AttributeKey, out object? attr)
            && attr is string intValue
            && Comparator switch
            {
                AttributeComparator.Equal => intValue == TargetValue,
                AttributeComparator.NotEqual => intValue != TargetValue,
                _ => false
            };
    }
}