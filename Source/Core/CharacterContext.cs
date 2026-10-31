using System.Collections.Generic;

namespace Doctrine;

public class CharacterContext
{
    public Dictionary<string, AttributeValue> Attributes { get; init; } = [];
    // Persistent hooks: relics, statuses.
    public List<IBattleHook> Hooks { get; init; } = [];
}
