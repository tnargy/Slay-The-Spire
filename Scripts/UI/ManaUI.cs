using Godot;

public partial class ManaUI : Panel
{
    [Export] public CharacterStats characterStats;
    Label manaLabel;

    public override void _Ready()
    {
        manaLabel = GetNode<Label>("Label");
        HandleStatsChanged();
        characterStats.OnStatsChanged += HandleStatsChanged;
    }

    private void HandleStatsChanged()
    {
        manaLabel.Text = $"{characterStats.Mana}/{characterStats.maxMana}";
    }

}
