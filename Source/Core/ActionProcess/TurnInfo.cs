using System.Collections.Generic;

namespace Doctrine;

public class TurnInfo
{
    public CharacterContext? Attacker { get; set; }
    public CharacterContext? Defender { get; set; }

    public Dictionary<string, AttributeValue> AttackAttributes { get; set; } = [];
    public Dictionary<string, AttributeValue> DefenseAttributes { get; set; } = [];
}