using Godot;

public partial class StatsUI : HBoxContainer
{
    HBoxContainer Block;
    Label BlockLabel;
    HBoxContainer Health;
    Label HealthLabel;

    public override void _Ready()
    {
        Block = GetNode<HBoxContainer>("Block");
        Health = GetNode<HBoxContainer>("Health");

        BlockLabel = GetNode<Label>("%BlockLabel");
        HealthLabel = GetNode<Label>("%HealthLabel");
    }

    public void UpdateStats(Stats stats)
    {
        BlockLabel.Text = stats.Block.ToString();
        HealthLabel.Text = stats.Health.ToString();

        Block.Visible = stats.Block > 0;
        Health.Visible = stats.Health > 0;
    }
}
