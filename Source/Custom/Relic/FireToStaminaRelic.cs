using System.Linq;

namespace Doctrine;

// Incoming fire damage is removed and gained as stamina.
// Runs in DefenderTransform: sees fire converted by the attacker, but not later damage multipliers.
public record FireToStaminaRelic : IBattleHook
{
    public string Name => "Ember Heart";
    public ResolvePhase Phase => ResolvePhase.DefenderTransform;
    public Side TriggerSide => Side.Defender;

    public void Execute(ResolveContext ctx, CharacterContext owner)
    {
        double absorbed = ctx.PartsOf(Element.Fire).Sum(part => part.Amount);
        if (absorbed <= 0)
            return;
        ctx.Damage.RemoveAll(part => part.Element == Element.Fire);
        owner.Attributes.AddNumber("Stamina", absorbed);
        ctx.Note($"absorbed {absorbed:0.##} fire as Stamina");
    }
}
