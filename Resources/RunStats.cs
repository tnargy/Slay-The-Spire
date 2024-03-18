using System;
using Godot;

public partial class RunStats : Resource
{
	const int STARTING_GOLD = 70;
	const int BASE_CARD_REWARD = 3;
	public const float BASE_COMMON_WEIGHT = 6.0f;
	public const float BASE_UNCOMMON_WEIGHT = 3.7f;
	public const float BASE_RARE_WEIGHT = 0.3f;

	public event Action OnGoldChange;
    void RaiseGoldChange () => OnGoldChange?.Invoke();

	[Export] public int cardRewards = BASE_CARD_REWARD;
	[Export(PropertyHint.Range, "0.0, 10.0, 0.1")] public float commonWeight = BASE_COMMON_WEIGHT;
	[Export(PropertyHint.Range, "0.0, 10.0, 0.1")] public float uncommonWeight = BASE_UNCOMMON_WEIGHT;
	[Export(PropertyHint.Range, "0.0, 10.0, 0.1")] public float rareWeight = BASE_RARE_WEIGHT;

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
		commonWeight = BASE_COMMON_WEIGHT;
		uncommonWeight = BASE_UNCOMMON_WEIGHT;
		rareWeight = BASE_RARE_WEIGHT;
	}
}
