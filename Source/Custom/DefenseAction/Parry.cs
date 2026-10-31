namespace Doctrine;

// Turn-scoped hook: a declared defense registers itself to make melee attacks miss this turn.
public record Parry : DefenseAction, IBattleHook
{
    public string Name => "Parry";
    public ResolvePhase Phase => ResolvePhase.HitCheck;
    public Side TriggerSide => Side.Defender;

    public override void Declare(CharacterContext user, TurnInfo turn)
    {
        turn.DefenderHooks.Add(this);
    }

    public void Execute(ResolveContext ctx, CharacterContext owner)
    {
        if (ctx.Turn.AttackAttributes.GetText("rangeType") == "melee")
            ctx.Hit = false;
    }
}
