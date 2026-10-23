namespace MyGame;

public class BattleContext
{
    public CharacterContext Player { get; private set; } = new();
    public CharacterContext Enemy { get; private set; } = new();
}