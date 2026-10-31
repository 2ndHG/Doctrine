namespace Doctrine;

// Owner takes 50% more damage.
public record Vulnerable : IBattleHook
{
    public string Name => "Vulnerable";
    public ResolvePhase Phase => ResolvePhase.DefenderModify;
    public Side TriggerSide => Side.Defender;

    public void Execute(ResolveContext ctx, CharacterContext owner)
    {
        foreach (DamagePart part in ctx.Damage)
            part.More *= 1.5;
    }
}
