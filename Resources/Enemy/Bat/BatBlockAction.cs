using Godot;

public partial class BatBlockAction : EnemyAction
{
	const float ENEMY_DASH_INTERVAL = 0.4f;
	const float ENEMY_WAIT_INTERVAL = 0.25f;
	
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
