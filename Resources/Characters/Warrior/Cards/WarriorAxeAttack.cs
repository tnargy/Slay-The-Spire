using Godot;

public partial class WarriorAxeAttack : Card
{
    public override void ApplyEffects(Node[] targets)
    {
        Damage damageEffect = new() { ammount = 6 };
        damageEffect.Execute(targets);
    }
}
