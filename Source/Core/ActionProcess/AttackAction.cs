using System.Collections.Generic;

namespace Doctrine;

public abstract record AttackAction
{
    public List<Condition> Requirement { get; set; } = [];
    public virtual bool CheckRequirement(CharacterContext user, CharacterContext opponent)
    {
        foreach (Condition condition in Requirement)
            if (!condition.Evaluate(user, opponent))
                return false;
        return true;
    }
    public abstract void Declare(CharacterContext user, TurnInfo turn);
}