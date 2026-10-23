namespace MyGame;

public class AttributeCompareFloat : Condition
{
    public string AttributeKey = "";
    public float TargetValue { get; set; } = 0;
    public AttributeComparator Comparator { get; set; }
    public override bool Evaluate(BattleContext battleContext, CharacterContext targetCharacter)
    {
        return targetCharacter.Attribute.TryGetValue(AttributeKey, out object? attr)
            && attr is float intValue
            && Comparator switch
            {
                AttributeComparator.Greater => intValue > TargetValue,
                AttributeComparator.GreaterEqual => intValue >= TargetValue,
                AttributeComparator.Less => intValue < TargetValue,
                AttributeComparator.LessEqual => intValue <= TargetValue,
                AttributeComparator.Equal => intValue == TargetValue,
                AttributeComparator.NotEqual => intValue != TargetValue,
                _ => false
            };
    }
}