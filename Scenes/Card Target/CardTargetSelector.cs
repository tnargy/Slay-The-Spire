using System;
using System.Collections.Generic;
using Godot;

public partial class CardTargetSelector : Node2D
{
    [Export] private Area2D area2D;
    [Export] private Line2D arc;
    private CardUI currentCard;
    private bool targeting = false;
    
    private double EaseOutCubic(double number) => 1.0 - Math.Pow(1.0 - number, 3.0);

    public override void _Ready()
    {
        GameEvents.OnCardAimingStarted += HandleCardAimingStarted;
        GameEvents.OnCardAimingEnded += HandleCardAimingEndeded;
    }

    public override void _Process(double delta)
    {
        if (!targeting) { return; }
        
        area2D.Position = GetLocalMousePosition();
        arc.Points = GetPoints().ToArray();
    }

    private List<Vector2> GetPoints()
    {
        List<Vector2> points = new();
        Vector2 start = currentCard.GlobalPosition;
        start.X += currentCard.Size.X / 2;
        Vector2 target = GetLocalMousePosition();
        Vector2 distance = (target - start);

        for (int i = 0; i < GameConstants.ARC_POINTS; i++)
        {
            double t = (1.0 / GameConstants.ARC_POINTS) * i;
            float x = start.X + (distance.X / GameConstants.ARC_POINTS) * i;
            float y = start.Y + (float)EaseOutCubic(t) * distance.Y;
            points.Add(new Vector2(x,y));
        }
        points.Add(target);
        
        return points;
    }

    private void HandleCardAimingStarted(CardUI cardUI)
    {
        if (!cardUI.card.IsSingleTargeted()) { return; }
        targeting = true;
        area2D.Monitoring = true;
        area2D.Monitorable = true;
        currentCard = cardUI;
    }


    private void HandleCardAimingEndeded(CardUI cardUI)
    {
        targeting = false;
        arc.ClearPoints();
        area2D.Position = Vector2.Zero;
        area2D.Monitoring = false;
        area2D.Monitorable = false;
        currentCard = null;;
    }

    private void HandleArea2DEntered(Area2D area)
    {
        if (currentCard == null || !targeting) { return; }
        currentCard.targets.Add(area);
    }

    private void HandleArea2DExited(Area2D area)
    {
        if (currentCard == null || !targeting) { return; }
        currentCard.targets.Remove(area);
    }
}
