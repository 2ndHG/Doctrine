using System;

namespace MyGame;

public class Condition
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required ConditionRule Rule { get; init; }
}
