namespace Doctrine;

// Owner deals 25% less damage.
public record Weak : IBattleHook
{
    public string Name => "Weak";
    public ResolvePhase Phase => ResolvePhase.AttackerModify;
    public Side TriggerSide => Side.Attacker;
    // After flat bonuses like Strength (order doesn't change More, but keeps the log readable).
    public int Priority => 100;

    public void Execute(ResolveContext ctx, CharacterContext owner)
    {
        foreach (DamagePart part in ctx.Damage)
            part.More *= 0.75;
    }
}
