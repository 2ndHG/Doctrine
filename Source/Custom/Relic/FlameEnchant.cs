namespace Doctrine;

// The owner's physical damage becomes fire.
public record FlameEnchant : IBattleHook
{
    public string Name => "Flame Enchant";
    public ResolvePhase Phase => ResolvePhase.AttackerTransform;
    public Side TriggerSide => Side.Attacker;

    public void Execute(ResolveContext ctx, CharacterContext owner)
    {
        foreach (DamagePart part in ctx.PartsOf(Element.Physical))
            part.Element = Element.Fire;
    }
}
