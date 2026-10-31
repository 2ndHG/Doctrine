namespace Doctrine;

// +Stacks flat damage on the main (first) damage part, so multi-element attacks don't gain it twice.
public record Strength(int Stacks) : IBattleHook
{
    public string Name => $"Strength {Stacks}";
    public ResolvePhase Phase => ResolvePhase.AttackerModify;
    public Side TriggerSide => Side.Attacker;

    public void Execute(ResolveContext ctx, CharacterContext owner)
    {
        if (ctx.Damage.Count > 0)
            ctx.Damage[0].Flat += Stacks;
    }
}
