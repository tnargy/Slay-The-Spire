using Godot;

public partial class Damage : Effects
{
    public override void Execute(Node[] targets)
    {
        foreach (var target in targets)
        {
            if (target == null) { continue; }
            if (target is Enemy)
            {
                Enemy enemy = (Enemy)target;
                enemy.TakeDamage(ammount);
            }
            else if (target is Player)
            {
                Player player = (Player)target;
                player.TakeDamage(ammount);
            }
        }
    }
}