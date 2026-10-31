namespace Doctrine;

// Order matters: phases run top to bottom.
// Rule of thumb: cancel first, decide WHAT the damage is (Transform) before HOW MUCH it is (Modify),
// and the attacker acts before the defender reacts.
public enum ResolvePhase
{
    PreResolve = 0,     // cancel / counter before anything happens
    HitCheck,           // set ctx.Hit = false to miss (dodge, parry)
    AttackerTransform,  // change element / add damage parts (physical -> fire)
    DefenderTransform,  // react to element (absorb, immune)
    AttackerModify,     // Flat / Increased / More on parts (strength, weak)
    DefenderModify,     // defender-side multipliers (vulnerable, resistance)
    Apply,              // built-in: block then Hp; hooks here run right before it
    AfterHit,           // on-hit / on-damaged triggers (thorns, lifesteal)
    DeathCheck          // built-in: mark dead; hooks here run right before it (revive)
}
