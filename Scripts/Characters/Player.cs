using Godot;

public partial class Player : Node
{
   private CharacterStats _stats;
    [Export] public CharacterStats Stats
    {
        get => _stats;
        set
        {
            _stats = (CharacterStats)value.CreateInstance();
        }
    }

    [Export] Sprite2D sprite2D;
    [Export] StatsUI statsUI;

    public override void _Ready()
    {
        UpdateCharacter();
        Stats.OnStatsChanged += UpdateCharacter;
    }

    void UpdateCharacter()
    {
        sprite2D.Texture = Stats.art;
        statsUI.UpdateStats(Stats);
    }

    public void TakeDamage(int damage)
    {
        Stats.TakeDamage(damage);
        if (Stats.Health <= 0) { QueueFree(); }
    }
}
