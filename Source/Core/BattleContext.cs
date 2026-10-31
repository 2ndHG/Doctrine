using System.Collections.Generic;

namespace Doctrine;

public class BattleContext
{
    public Dictionary<string, AttributeValue> Environment { get; set; } = [];
    public CharacterContext Player { get; set; } = new();
    public CharacterContext Enemy { get; set; } = new();
}