using System.Collections.Generic;
using System.Linq;

namespace Doctrine;

// Mutable state of a single attack going through the pipeline. Hooks read and write this.
public class ResolveContext
{
    public required TurnInfo Turn { get; init; }
    // How many reactions deep this resolution is (0 = the declared attack).
    public int Depth { get; init; }

    public List<DamagePart> Damage => Turn.Damage;
    public CharacterContext Attacker => Turn.Attacker;
    public CharacterContext Defender => Turn.Defender;

    public bool Cancelled { get; set; }
    public bool Hit { get; set; } = true;

    // Filled by the built-in Apply step.
    public double BlockedAmount { get; set; }
    public double HpDamage { get; set; }
    public bool DefenderDied { get; set; }

    // New attacks spawned by hooks (thorns, counters). Resolved after this one finishes.
    public List<TurnInfo> Reactions { get; } = [];
    public List<string> Log { get; } = [];

    public bool IsReaction => Depth > 0;
    public double TotalDamage => Damage.Sum(part => part.Amount);

    public CharacterContext OpponentOf(CharacterContext character) =>
        character == Attacker ? Defender : Attacker;

    public IEnumerable<DamagePart> PartsOf(string element) =>
        Damage.Where(part => part.Element == element);

    // For hooks to explain side effects the damage diff can't show (e.g. gained stamina).
    public void Note(string message) => Log.Add($"{new string(' ', Depth * 2)}      - {message}");
}
