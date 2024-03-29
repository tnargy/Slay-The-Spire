using System;
using Godot;

public partial class Tooltip : PanelContainer
{
    [Export] float fade = 0.2f;

    TextureRect tooltipIcon;
    RichTextLabel tooltipText;
    Tween tween;
    bool isVisible = false;

    public override void _Ready()
    {
        tooltipIcon = GetNode<TextureRect>("%TooltipIcon");
        tooltipText = GetNode<RichTextLabel>("%TooltipText");
        Color modulate = Colors.Transparent;
        Hide();

        GameEvents.OnTooltipRequested += ShowTooltip;
        GameEvents.OnTooltipHide += HideTooltip;
    }

    public override void _ExitTree()
    {
        GameEvents.OnTooltipRequested -= ShowTooltip;
        GameEvents.OnTooltipHide -= HideTooltip;
    }

    public void ShowTooltip(Card card)
    {
        isVisible = true;
        if (tween != null && tween.IsRunning())
        {
            tween.Kill();
        }

        tooltipIcon.Texture = card.icon;
        tooltipText.Text = card.tooltopText;

        tween = CreateTween().SetTrans(Tween.TransitionType.Cubic).SetEase(Tween.EaseType.Out);
        tween.TweenCallback(Callable.From(Show));
        tween.TweenProperty(this, "modulate", Colors.White, fade);
    }

    public void HideTooltip()
    {
        isVisible = false;
        if (tween != null && tween.IsRunning())
        {
            tween.Kill();
        }
        
        GetTree().CreateTimer(fade, false).Timeout += HideAnimation;
    }

    private void HideAnimation()
    {
        if (!isVisible)
        {
            tween = CreateTween().SetTrans(Tween.TransitionType.Cubic).SetEase(Tween.EaseType.Out);
            tween.TweenProperty(this, "modulate", Colors.Transparent, fade);
            tween.TweenCallback(Callable.From(Hide));
        }
    }

}
