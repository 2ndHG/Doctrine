using System;
using System.Collections.Generic;

namespace Doctrine;

public class TurnInfo
{
    public CharacterContext Attacker { get; set; } = new();
    public CharacterContext Defender { get; set; } = new();

    public Dictionary<string, AttributeValue> AttackAttributes { get; set; } = [];
    public Dictionary<string, AttributeValue> DefenseAttributes { get; set; } = [];
}