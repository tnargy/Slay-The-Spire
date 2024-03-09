using Godot;
using System;

public partial class Stats : Resource
{
    [Export(PropertyHint.Range, "1,100")] protected int maxHealth = 1;
    [Export] public Texture2D art { get; private set; }

    public event Action OnStatsChanged;
    public void RaiseStatsChanged() => OnStatsChanged?.Invoke();

    private int _health;
    public int Health
    {
        get => _health;
        set 
        {
            _health = Math.Clamp(value, 0, maxHealth);
            RaiseStatsChanged();
        }
    }

    private int _block;
    public int Block
    {
        get => _block;
        set 
        {
            _block = Math.Clamp(value, 0, 999);
            RaiseStatsChanged();
        }
    }

    public void Heal(int potion) => Health += potion;

    public void TakeDamage(int damage)
    {
        if (damage <= 0) { return; }

        int blockedDamage = Math.Clamp(damage - Block, 0, damage);  // Shield damage
        Block = Math.Clamp(Block - damage, 0, Block);               // Reduce shield by damage
        Health -= blockedDamage;
    }

    public virtual Resource CreateInstance()
    {
        Stats instance = (Stats)this.Duplicate();
        instance.Health = maxHealth;
        instance.Block = 0;
        return instance;
    }
}
