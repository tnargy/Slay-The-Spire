using Godot;

public partial class AxeAttack : Card
{
    public override void ApplyEffects(Node[] targets)
    {
        Damage damageEffect = new()
        {
            amount = 6,
            sound = sound
        };
        damageEffect.Execute(targets);
    }
}
