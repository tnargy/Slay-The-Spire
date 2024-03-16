using Godot;

public partial class BattleReward : Control
{
    [Export] Button button;

    public override void _Ready()
    {
        button.Pressed += () => GameEvents.RaiseBattleRewardExited();
    }
}
