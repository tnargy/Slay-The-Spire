using Godot;

public partial class BlockAction : Card
{
    public override void ApplyEffects(Node[] targets)
    {
        Block blockEffect = new()
        {
            amount = 5,
            sound = sound
        };
        blockEffect.Execute(targets);
    }
}