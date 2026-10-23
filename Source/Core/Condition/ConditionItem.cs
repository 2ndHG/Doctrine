using System;

namespace MyGame;

public class ConditionItem
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Condition Rule { get; init; }
}
