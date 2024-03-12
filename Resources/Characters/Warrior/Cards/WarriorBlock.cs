using Godot;

public partial class WarriorBlock : Card
{
    public override void ApplyEffects(Node[] targets)
    {
        Block blockEffect = new() { amount = 5 };
        blockEffect.Execute(targets);
    }
}