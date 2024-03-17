using Godot;

public partial class WarriorBigSlam : Card
{
	public override void ApplyEffects(Node[] targets)
	{
		Damage damageEffect = new()
		{
			amount = 10,
			sound = sound
		};
		damageEffect.Execute(targets);
		GD.Print("TODO: this will all apply status effect later on!");
	}
}
