using Godot;
using System;

public partial class WizzardFireball : Card
{
    public override void ApplyEffects(Node[] targets)
	{
		Damage damageEffect = new()
		{
			amount = 10,
			sound = sound
		};
		damageEffect.Execute(targets);
	}
}
