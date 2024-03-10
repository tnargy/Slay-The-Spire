using Godot;

public partial class Enemy : Area2D
{
    private Stats _stats;
    [Export] public Stats Stats
    {
        get => _stats;
        set
        {
            _stats = (Stats)value.CreateInstance();
        }
    }

    [Export] Sprite2D sprite2D;
    [Export] StatsUI statsUI;
    [Export] Sprite2D selected;
    
    void HandleAreaEntered(Area2D area2D) => selected.Show();
    void HandleAreaExited(Area2D area2D) => selected.Hide();

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
