using Godot;

public partial class GoldUI : HBoxContainer
{
    [Export] TextureRect icon;
    [Export] Label label;

    [Export] RunStats _runStats;
    public RunStats runStats
    {
        get => _runStats;
        set
        {
            _runStats = value;
            runStats.OnGoldChange += UpdateGold;
            UpdateGold();
        }
    }

    private void UpdateGold()
    {
        label.Text = runStats.Gold.ToString();
    }

}
