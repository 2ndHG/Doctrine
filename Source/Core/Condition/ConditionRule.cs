namespace Doctrine;

public abstract record ConditionRule
{
    public abstract bool Evaluate(BattleContext battleContext, CharacterContext targetCharacter);
}