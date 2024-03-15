using Godot;

public partial class CrabBlockAction : EnemyAction
{
    [Export] int block = 6;

    public override void PerformAction()
    {
        if (enemy == null) { return; }

        Block blockEffect = new()
        {
            amount = block,
            sound = sound
        };
        Node[] targets = new Node[] { enemy };
        blockEffect.Execute(targets);

        GameEvents.RaiseEnemyActionFinsihed(enemy);
    }
}
