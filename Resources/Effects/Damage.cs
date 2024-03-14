using Godot;

public partial class Damage : Effects
{
    public override void Execute(Node[] targets)
    {
        foreach (var target in targets)
        {
            if (target == null) { continue; }
            SoundPlayer SFXPlayer = target.GetNode<SoundPlayer>("/root/SFXPlayer");
            if (target is Enemy enemy )
            {
                enemy.TakeDamage(amount);
                SFXPlayer.Play(sound);
            }
            else if (target is Player player)
            {
                player.TakeDamage(amount);
                SFXPlayer.Play(sound);
            }
        }
    }
}