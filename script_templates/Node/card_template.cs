// meta-name: Card Logic
// meta-description: What happens when a card is played.
using Godot;

public partial class MyCard : Card
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