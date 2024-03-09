using Godot;

public partial class WarriorBlock : Card
{
    public override void ApplyEffects(Node[] targets)
    {
        Block blockEffect = new() { ammount = 5 };
        blockEffect.Execute(targets);
    }
}