using Godot;

public partial class DualAttack : Card
{
	public override void ApplyEffects(Node[] targets)
	{
		Damage damageEffect = new()
		{
			amount = 4,
			sound = sound
		};
		Tween tween = targets[0].CreateTween();
		tween.TweenCallback(Callable.From(() => damageEffect.Execute(targets)));
		tween.TweenInterval(0.25f);
		tween.TweenCallback(Callable.From(() => damageEffect.Execute(targets)));
	}
}
