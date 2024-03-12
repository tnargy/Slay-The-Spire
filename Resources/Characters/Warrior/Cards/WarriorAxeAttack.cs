using Godot;

public partial class WarriorAxeAttack : Card
{
    public override void ApplyEffects(Node[] targets)
    {
        Damage damageEffect = new() { amount = 6 };
        damageEffect.Execute(targets);
    }
}
