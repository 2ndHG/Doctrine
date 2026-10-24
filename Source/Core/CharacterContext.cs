using System.Collections.Generic;

namespace Doctrine;

public class CharacterContext
{
    public Dictionary<string, AttributeValue> Attributes { get; init; } = [];
}
