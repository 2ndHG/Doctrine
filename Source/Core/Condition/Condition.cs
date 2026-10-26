using System;

namespace Doctrine;

public class Condition
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public CharacterType TargetCharacter { get; set; } = CharacterType.Self;
    public required ConditionRule Rule { get; init; }
    public bool Evaluate(CharacterContext self, CharacterContext opponent)
    {
        return Rule.Evaluate(TargetCharacter == CharacterType.Self ? self : opponent);
    }
}
