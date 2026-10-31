using System;
using System.Collections.Generic;
using System.Linq;

namespace Doctrine;

public static class DamageResolver
{
    // Guards against reaction loops (thorns vs thorns).
    public const int MaxReactionDepth = 8;

    public const string HpKey = "Hp";
    public const string DeadKey = "Dead";
    public const string BlockKey = "block";

    // Resolves the declared attack, then every reaction it spawns, breadth-first.
    public static List<string> Resolve(TurnInfo turn)
    {
        List<string> log = [];
        Queue<(TurnInfo Turn, int Depth)> queue = new();
        queue.Enqueue((turn, 0));

        while (queue.Count > 0)
        {
            (TurnInfo current, int depth) = queue.Dequeue();
            if (depth > MaxReactionDepth)
            {
                log.Add($"[Chain] depth {depth} exceeds {MaxReactionDepth}, reaction dropped");
                continue;
            }

            ResolveContext ctx = ResolveOne(current, depth);
            log.AddRange(ctx.Log);
            foreach (TurnInfo reaction in ctx.Reactions)
                queue.Enqueue((reaction, depth + 1));
        }
        return log;
    }

    public static ResolveContext ResolveOne(TurnInfo turn, int depth = 0)
    {
        ResolveContext ctx = new() { Turn = turn, Depth = depth };
        string indent = new(' ', depth * 2);
        ctx.Log.Add($"{indent}== Resolve {(ctx.IsReaction ? "reaction" : "attack")}: {Describe(ctx.Damage)}");

        List<(IBattleHook Hook, CharacterContext Owner)> hooks = CollectHooks(turn);

        foreach (ResolvePhase phase in Enum.GetValues<ResolvePhase>())
        {
            foreach ((IBattleHook hook, CharacterContext owner) in hooks.Where(h => h.Hook.Phase == phase))
            {
                string before = Describe(ctx.Damage);
                int logIndex = ctx.Log.Count;
                hook.Execute(ctx, owner);
                string after = Describe(ctx.Damage);
                // Inserted before the hook's own notes so the log reads top-down.
                ctx.Log.Insert(logIndex, before == after
                    ? $"{indent}  [{phase}] {hook.Name}"
                    : $"{indent}  [{phase}] {hook.Name}: {before} -> {after}");
            }

            RunBuiltIn(phase, ctx, indent);

            if (ctx.Cancelled)
            {
                ctx.Log.Add($"{indent}  [{phase}] cancelled");
                break;
            }
            if (!ctx.Hit)
            {
                ctx.Log.Add($"{indent}  [{phase}] missed");
                break;
            }
        }
        return ctx;
    }

    // Sorted by phase, priority, side (attacker first), then registration order (OrderBy is stable).
    private static List<(IBattleHook Hook, CharacterContext Owner)> CollectHooks(TurnInfo turn)
    {
        IEnumerable<(IBattleHook Hook, CharacterContext Owner, Side Side)> attacker =
            turn.Attacker.Hooks.Concat(turn.AttackerHooks).Select(h => (h, turn.Attacker, Side.Attacker));
        IEnumerable<(IBattleHook Hook, CharacterContext Owner, Side Side)> defender =
            turn.Defender.Hooks.Concat(turn.DefenderHooks).Select(h => (h, turn.Defender, Side.Defender));

        return attacker.Concat(defender)
            .Where(h => h.Hook.TriggerSide == h.Side)
            .OrderBy(h => h.Hook.Phase)
            .ThenBy(h => h.Hook.Priority)
            .ThenBy(h => h.Side)
            .Select(h => (h.Hook, h.Owner))
            .ToList();
    }

    // Built-in rules run after the phase's hooks, so hooks can prepare or override the result.
    private static void RunBuiltIn(ResolvePhase phase, ResolveContext ctx, string indent)
    {
        switch (phase)
        {
            case ResolvePhase.Apply:
                ApplyDamage(ctx, indent);
                break;
            case ResolvePhase.DeathCheck:
                if (ctx.Defender.Attributes.GetNumber(HpKey) <= 0)
                {
                    ctx.DefenderDied = true;
                    ctx.Defender.Attributes[DeadKey] = true;
                    ctx.Log.Add($"{indent}  [DeathCheck] defender died");
                }
                break;
        }
    }

    // Block is a turn-scoped pool declared by the defense action; it absorbs damage before Hp.
    private static void ApplyDamage(ResolveContext ctx, string indent)
    {
        double total = ctx.TotalDamage;
        double block = ctx.Turn.DefenseAttributes.GetNumber(BlockKey);
        double blocked = Math.Min(block, total);
        double hpDamage = total - blocked;

        ctx.Turn.DefenseAttributes[BlockKey] = block - blocked;
        ctx.Defender.Attributes.AddNumber(HpKey, -hpDamage);
        ctx.BlockedAmount = blocked;
        ctx.HpDamage = hpDamage;

        ctx.Log.Add($"{indent}  [Apply] {total:0.##} total, {blocked:0.##} blocked, {hpDamage:0.##} to Hp " +
                    $"(Hp now {ctx.Defender.Attributes.GetNumber(HpKey):0.##})");
    }

    private static string Describe(List<DamagePart> damage) =>
        damage.Count == 0 ? "none" : string.Join(" + ", damage);
}
