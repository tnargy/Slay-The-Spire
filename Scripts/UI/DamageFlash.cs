using System;
using Godot;

public partial class DamageFlash : CanvasLayer
{
    Timer timer;
    ColorRect colorRect;

    public override void _Ready()
    {
        timer = GetNode<Timer>("Timer");
        timer.Timeout += () => colorRect.Color = new Color(colorRect.Color, 0);
        colorRect = GetNode<ColorRect>("ColorRect");
        GameEvents.OnPlayerHit += HandlePlayerHit;
    }

    public override void _ExitTree()
    {
        GameEvents.OnPlayerHit -= HandlePlayerHit;
    }

    private void HandlePlayerHit()
    {
        colorRect.Color = new Color(colorRect.Color, 0.15f);
        timer.Start();
    }

}
