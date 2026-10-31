namespace Doctrine;

// Gain block for this turn. Consumed by the built-in Apply step before Hp.
public record Guard : DefenseAction
{
    public double Block { get; init; } = 8;

    public override void Declare(CharacterContext user, TurnInfo turn)
    {
        turn.DefenseAttributes.AddNumber(DamageResolver.BlockKey, Block);
    }
}
