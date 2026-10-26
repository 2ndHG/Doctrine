using System;
using System.Collections.Generic;

namespace Doctrine;

public class BranchNode : Node
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public Node? IfNode { get; set; }
    public Node? ElseNode { get; set; }

    public List<Condition>? Conditions { get; set; }

    public bool Evaluate(CharacterContext self, CharacterContext opponent)
    {
        if (Conditions == null)
            return false;
        foreach (Condition condition in Conditions)
        {
            if (!condition.Evaluate(self, opponent))
                return false;
        }
        return true;
    }
}