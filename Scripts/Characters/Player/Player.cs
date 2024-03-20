using System;
using Godot;

public partial class Player : Node2D
{
    [Export] CharacterStats _characterStats;
    public CharacterStats characterStats
    {
        get => _characterStats;
        set
        {
            _characterStats = value;
            _characterStats.OnStatsChanged += UpdateCharacter;
        }
    }
    
    Sprite2D sprite2D;
    StatsUI statsUI;

    public override void _Ready()
    {
        sprite2D = GetNode<Sprite2D>("Sprite2D");
        statsUI = GetNode<StatsUI>("StatsUI");
    }

    void UpdateCharacter()
    {
        if (!IsInstanceValid(this)) { return; }
        sprite2D.Texture = characterStats.art;
        statsUI.UpdateStats(characterStats);
    }

    public void TakeDamage(int damage)
    {
        sprite2D.Material = GameConstants.WHITE_SPRITE_MATERIAL;

        Tween tween = CreateTween();
        tween.TweenCallback(Callable.From(() => Shaker.Shake(this, 16, 0.15f)));
        tween.TweenCallback(Callable.From(() => characterStats.TakeDamage(damage)));
        tween.TweenInterval(0.17);

        tween.Finished += () => 
        {
            sprite2D.Material = null;
            if (characterStats.Health <= 0)
            {
                GameEvents.RaisePlayerDied(); 
                QueueFree(); 
            }
        };
    }
}
