using System;
using Godot;

public partial class RunStats : Resource
{
	const int STARTING_GOLD = 70;

	public event Action OnGoldChange;
    void RaiseGoldChange () => OnGoldChange?.Invoke();

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
}
