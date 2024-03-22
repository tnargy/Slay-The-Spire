using Godot;

public partial class CrabMegaBlockAction : EnemyAction
{
    [Export] int block = 15;
    [Export] int healthThreshold = 8;

    bool alreadyUsed = false;

    public override bool IsPerformable()
    {
        if (enemy == null || alreadyUsed) { return false; }

        bool isLowHealth = enemy.Stats.Health <= healthThreshold;
        alreadyUsed = isLowHealth;
        return isLowHealth;
    }

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
