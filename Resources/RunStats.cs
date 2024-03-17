using System;
using Godot;

public partial class RunStats : Resource
{
	const int STARTING_GOLD = 70;
	const int BASE_CARD_REWARD = 3;
	const float BASE_COMMON_WEIGHT = 6.0f;
	const float BASE_UNCOMMON_WEIGHT = 3.7f;
	const float BASE_RARE_WEIGHT = 0.3f;

	public event Action OnGoldChange;
    void RaiseGoldChange () => OnGoldChange?.Invoke();

	[Export] public int card_rewards = BASE_CARD_REWARD;
	[Export(PropertyHint.Range, "0.0, 10.0, 0.1")] public float common_weight = BASE_COMMON_WEIGHT;
	[Export(PropertyHint.Range, "0.0, 10.0, 0.1")] public float uncommon_weight = BASE_UNCOMMON_WEIGHT;
	[Export(PropertyHint.Range, "0.0, 10.0, 0.1")] public float rare_weight = BASE_RARE_WEIGHT;

	[Export] int _gold = STARTING_GOLD;
	public int Gold
	{
		get => _gold;
		set
		{
			_gold = value;
			RaiseGoldChange();
		}
	}

	public void ResetWeights()
	{
		common_weight = BASE_COMMON_WEIGHT;
		uncommon_weight = BASE_UNCOMMON_WEIGHT;
		rare_weight = BASE_RARE_WEIGHT;
	}
}
