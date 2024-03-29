using Godot;

public partial class BatBlockAction : EnemyAction
{
	[Export] int block;
	
	public override void PerformAction()
	{
		if (enemy == null || target == null) { return; }

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
