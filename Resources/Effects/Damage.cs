using Godot;

public partial class Damage : Effects
{
    public override void Execute(Node[] targets)
    {
        foreach (var target in targets)
        {
            if (target == null) { continue; }
            if (target is Enemy enemy)
            {
                enemy.TakeDamage(amount);
            }
            else if (target is Player player)
            {
                player.TakeDamage(amount);
            }
        }
    }
}