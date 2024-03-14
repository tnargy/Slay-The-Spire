using System.Linq;
using Godot;

public partial class SoundPlayer : Node
{
    public void Play(AudioStream audio, bool single = false)
    {
        if (audio == null) { return; }
        if (single) Stop();
        foreach (AudioStreamPlayer player in GetChildren().Where(T => T is AudioStreamPlayer))
        {
            if (!player.Playing)
            {
                player.Stream = audio;
                player.Play();
                break;
            }
        }
    }

    public void Stop()
    {
        foreach (AudioStreamPlayer player in GetChildren().Where(T => T is AudioStreamPlayer))
        {
            player.Stop();
        }
    }

}
