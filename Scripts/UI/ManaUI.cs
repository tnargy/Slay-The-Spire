using Godot;

public partial class ManaUI : Panel
{
    [Export] private CharacterStats _characterStats;
    public CharacterStats CharStats
    {
        get => _characterStats;
        set
        {
            _characterStats = value;
        }
    }
    Label manaLabel;

    public override async void _Ready()
    {
        manaLabel = GetNode<Label>("Label");
        await ToSignal(Owner, SignalName.Ready);
        HandleStatsChanged();
        CharStats.OnStatsChanged += HandleStatsChanged;
        // GetTree().CreateTimer(2).Timeout += () => characterStats.Mana = 2;
    }

    private void HandleStatsChanged()
    {
        manaLabel.Text = $"{CharStats.Mana}/{CharStats.maxMana}";
    }

}
