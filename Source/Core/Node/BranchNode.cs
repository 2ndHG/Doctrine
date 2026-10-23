using System;
using System.Collections.Generic;

namespace MyGame;

public class BranchNode : Node
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public Node? IfNode { get; set; }
    public Node? ElseNode { get; set; }

    public List<ConditionItem>? Conditions { get; set; }

    public bool Evaluate(BattleContext battleContext, CharacterContext targetCharacter)
    {
        if (Conditions == null)
            return false;
        foreach (ConditionItem item in Conditions)
        {
            if (!item.Rule.Evaluate(battleContext, targetCharacter))
                return false;
        }
        return true;
    }
}