using Godot;

public partial class Block : Effects
{
    public override void Execute(Node[] targets)
    {
        foreach (var target in targets)
        {
            if (target == null) { continue; }
            if (target is Enemy enemy)
            {
                enemy.Stats.Block += amount;
            }
            else if (target is Player player)
            {
                player.Stats.Block += amount;
            }
        }
    }
}