namespace MyGame;

public abstract record Condition
{
    public abstract bool Evaluate(BattleContext battleContext, CharacterContext targetCharacter);
}