namespace Doctrine;

// Once per battle, survive a lethal hit with some Hp.
// DeathCheck hooks run before the built-in death check, so restoring Hp here prevents death.
public record LizardTail : IBattleHook
{
    public string Name => "Lizard Tail";
    public ResolvePhase Phase => ResolvePhase.DeathCheck;
    public Side TriggerSide => Side.Defender;
    public double ReviveHp { get; init; } = 10;
    public bool Used { get; set; }

    public void Execute(ResolveContext ctx, CharacterContext owner)
    {
        if (Used || owner.Attributes.GetNumber(DamageResolver.HpKey) > 0)
            return;
        Used = true;
        owner.Attributes[DamageResolver.HpKey] = ReviveHp;
        ctx.Note($"revived with {ReviveHp:0.##} Hp");
    }
}
