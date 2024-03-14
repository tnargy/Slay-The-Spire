using System;
using Godot;

public partial class DamageFlash : CanvasLayer
{
    [Export] Timer timer;
    [Export] ColorRect colorRect;

    public override void _Ready()
    {
        GameEvents.OnPlayerHit += HandlePlayerHit;
        timer.Timeout += () => colorRect.Color = new Color(colorRect.Color, 0);
    }

    private void HandlePlayerHit()
    {
        colorRect.Color = new Color(colorRect.Color, 0.15f);
        timer.Start();
    }

}
