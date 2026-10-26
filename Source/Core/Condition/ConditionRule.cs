namespace Doctrine;

public abstract record ConditionRule
{
    public abstract bool Evaluate(CharacterContext targetCharacter);
}