namespace MyGame;

public abstract class Condition
{
    public abstract bool Evaluate(BattleContext battleContext, CharacterContext targetCharacter);
}