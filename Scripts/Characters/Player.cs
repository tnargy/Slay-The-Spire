using Godot;

public partial class Player : Node2D
{
   private CharacterStats _stats;
    [Export] public CharacterStats Stats
    {
        get => _stats;
        set
        {
            _stats = value;
        }
    }

    [Export] Sprite2D sprite2D;
    [Export] StatsUI statsUI;

    public override async void _Ready()
    {
        await ToSignal(Owner, SignalName.Ready);
        UpdateCharacter();
        Stats.OnStatsChanged += UpdateCharacter;
    }

    void UpdateCharacter()
    {
        if (!IsInstanceValid(this)) { return; }
        sprite2D.Texture = Stats.art;
        statsUI.UpdateStats(Stats);
    }

    public void TakeDamage(int damage)
    {
        Stats.TakeDamage(damage);
        if (Stats.Health <= 0)
        {
            GameEvents.RaisePlayerDied(); 
            QueueFree(); 
        }
    }
}
