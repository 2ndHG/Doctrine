namespace Doctrine;

// Anything that reacts during damage resolution: relics, statuses, turn-scoped defense effects.
public interface IBattleHook
{
    string Name { get; }
    ResolvePhase Phase { get; }
    // Only fires when its owner is on this side of the current attack.
    Side TriggerSide { get; }
    // Lower runs first within the same phase. Ties: attacker before defender, then registration order.
    int Priority => 0;

    void Execute(ResolveContext ctx, CharacterContext owner);
}
