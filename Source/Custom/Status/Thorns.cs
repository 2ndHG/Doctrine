namespace Doctrine;

// When hit, deal Stacks physical damage back to the attacker.
// Ignores reactions so two Thorns owners don't bounce damage forever.
public record Thorns(int Stacks) : IBattleHook
{
    public string Name => $"Thorns {Stacks}";
    public ResolvePhase Phase => ResolvePhase.AfterHit;
    public Side TriggerSide => Side.Defender;

    public void Execute(ResolveContext ctx, CharacterContext owner)
    {
        if (ctx.IsReaction)
            return;
        ctx.Reactions.Add(new TurnInfo
        {
            Attacker = owner,
            Defender = ctx.Attacker,
            Damage = [new DamagePart(Element.Physical, Stacks)]
        });
        ctx.Note($"queued {Stacks} thorns damage");
    }
}
