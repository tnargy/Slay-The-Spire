using Godot;

public partial class HealthUI : HBoxContainer
{
	[Export] public bool ShowMaxHP;

    Label HealthLabel;
    Label MaxHealthLabel;

	public override void _Ready()
    {
        HealthLabel = GetNode<Label>("%HealthLabel");
        MaxHealthLabel = GetNode<Label>("%MaxHealthLabel");
    }

    public void UpdateStats(Stats stats)
    {
        HealthLabel.Text = stats.Health.ToString();
        MaxHealthLabel.Text = "/" + stats.maxHealth.ToString();
		MaxHealthLabel.Visible = ShowMaxHP;
    }
}
