using Godot;

public partial class BatAttackAction : EnemyAction
{
	const float ENEMY_DASH_INTERVAL = 0.4f;
	const float ENEMY_WAIT_INTERVAL = 0.25f;
	
	[Export] int damage;
	
	public override void PerformAction()
	{
		if (enemy == null || target == null) { return; }

		Vector2 start = enemy.GlobalPosition;
		Vector2 end = target.GlobalPosition + Vector2.Right * 64;
		Damage damageEffect = new()
		{
		    amount = damage,
		    sound = sound
		};
		Node[] targets = new Node[] { target };

		Tween tween = enemy.CreateTween().SetTrans(Tween.TransitionType.Quint);
		tween.TweenProperty(enemy, "global_position", end, ENEMY_DASH_INTERVAL);
		tween.TweenCallback(Callable.From(() => damageEffect.Execute(targets)));
		tween.TweenInterval(ENEMY_WAIT_INTERVAL + .10);
		tween.TweenCallback(Callable.From(() => damageEffect.Execute(targets)));
		tween.TweenInterval(ENEMY_WAIT_INTERVAL);
		tween.TweenProperty(enemy, "global_position", start, ENEMY_DASH_INTERVAL);

		tween.Finished += () => GameEvents.RaiseEnemyActionFinsihed(enemy);
	}
}
