using System;
using System.Collections.Generic;

namespace Doctrine;

public class TurnInfo
{
    public CharacterContext Attacker { get; set; } = new();
    public CharacterContext Defender { get; set; } = new();

    public Dictionary<string, AttributeValue> AttackAttributes { get; set; } = [];
    public Dictionary<string, AttributeValue> DefenseAttributes { get; set; } = [];

    public List<DamagePart> Damage { get; set; } = [];

    // Hooks that only exist for this turn, registered by Declare (e.g. a parry stance).
    public List<IBattleHook> AttackerHooks { get; set; } = [];
    public List<IBattleHook> DefenderHooks { get; set; } = [];
}
