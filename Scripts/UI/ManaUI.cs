using Godot;

public partial class ManaUI : Panel
{
    [Export] CharacterStats _characterStats;
    public CharacterStats characterStats
    {
        get => _characterStats;
        set => SetStats(value);
    }
    void SetStats(CharacterStats value)
    {
        if (value == null) { return; }
        _characterStats = value;
        _characterStats.OnStatsChanged += HandleStatsChanged;

        HandleStatsChanged();
    }

    private void HandleStatsChanged()
    {
        manaLabel.Text = $"{characterStats.Mana}/{characterStats.maxMana}";
    }

    
    Label manaLabel;
    
    public override void _Ready()
    {
        manaLabel = GetNode<Label>("%ManaLabel");
    }

    public override void _ExitTree()
    {
        _characterStats.OnStatsChanged -= HandleStatsChanged;
    }
}
