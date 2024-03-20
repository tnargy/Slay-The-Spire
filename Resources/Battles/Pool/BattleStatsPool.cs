using Godot;
using System;
using System.Linq;

public partial class BattleStatsPool : Resource
{
    [Export] public BattleStats[] pool;

    float[] totalWeightsByTier = {0.0f, 0.0f, 0.0f};
    public BattleStats[] GetAllBattlesForTier(int tier) => pool.Where((BattleStats bs) => bs.battleTier == tier).ToArray();

    void SetupWeightForTier(int tier)
    {
        BattleStats[] battles = GetAllBattlesForTier(tier);
        totalWeightsByTier[tier] = 0.0f;

        foreach (BattleStats battle in battles)
        {
            totalWeightsByTier[tier] += battle.weight;
            battle.accumulatedWeight = totalWeightsByTier[tier];
        }
    }

    public BattleStats GetRandomBattleForTier(int tier)
    {
        var roll = Random.Shared.NextDouble() * totalWeightsByTier[tier];
        var battles = GetAllBattlesForTier(tier);
        foreach (BattleStats battle in battles)
        {
            if (battle.accumulatedWeight > roll) { return battle; }
        }

        return null;
    }

    public void Setup()
    {
        for (int i = 0; i < totalWeightsByTier.Length; i++)
        {
            SetupWeightForTier(i);
        }
    }
}
