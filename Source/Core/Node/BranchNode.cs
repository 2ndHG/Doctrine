using System.Collections.Generic;

namespace MyGame;

public class BranchNode : Node
{
    public string Name { get; set; } = "";
    public Node? IfNode { get; set; }
    public Node? ElseNode { get; set; }

    public List<Condition> Conditions { get; init; } = [];

    public bool Evaluate(BattleContext battleContext, CharacterContext targetCharacter)
    {
        foreach(Condition condition in Conditions)
        {
            if(!condition.Evaluate(battleContext, targetCharacter))
                return false;
        }
        return true;
    }
}