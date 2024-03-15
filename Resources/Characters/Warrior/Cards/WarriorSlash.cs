using Godot;

public partial class WarriorSlash : Card
{
	public override void ApplyEffects(Node[] targets)
	{
		Damage damageEffect = new()
		{
			amount = 4,
			sound = sound
		};
		damageEffect.Execute(targets);
	}
}
