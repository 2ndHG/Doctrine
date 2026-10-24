namespace Doctrine;

public class BattleContext
{
    public CharacterContext Player { get; set; } = new();
    public CharacterContext Enemy { get; set; } = new();
}