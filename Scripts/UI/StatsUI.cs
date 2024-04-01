using Godot;

public partial class StatsUI : HBoxContainer
{
    HBoxContainer Block;
    HealthUI Health;
    Label BlockLabel;
    

    public override void _Ready()
    {
        Block = GetNode<HBoxContainer>("Block");
        Health = GetNode<HealthUI>("Health");
        BlockLabel = GetNode<Label>("%BlockLabel");
    }

    public void UpdateStats(Stats stats)
    {
        BlockLabel.Text = stats.Block.ToString();
        Health.UpdateStats(stats);
        
        Block.Visible = stats.Block > 0;
        Health.Visible = stats.Health > 0;
    }
}
