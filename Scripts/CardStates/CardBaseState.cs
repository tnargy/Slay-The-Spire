using Godot;

public partial class CardBaseState : CardState
{
    protected override void EnterState()
    {
        if (cardUI.tween != null && cardUI.tween.IsRunning())
        {
            cardUI.tween.Kill();
        }

        cardUI.panel.Set("theme_override_styles/panel", GameConstants.BASE_STYLEBOX);
        CardUI.RaiseReparentRequested(cardUI);
        cardUI.PivotOffset = Vector2.Zero;
        GameEvents.RaiseTooltipHide();
    }

    public override void OnGUIInput(InputEvent @event)
    {
        if (!cardUI.Playable || cardUI.disabled) { return; }
        if (@event is InputEventMouseButton && @event.IsActionPressed(GameConstants.INPUT_LEFT_CLICK))
        {
            cardUI.PivotOffset = cardUI.GetGlobalMousePosition() - cardUI.GlobalPosition;
            cardUI.stateMachine.SwitchState<CardClickState>();
        }
    }

    public override void OnMouseEntered()
    {
        if (!cardUI.Playable || cardUI.disabled) { return; }
        cardUI.panel.Set("theme_override_styles/panel", GameConstants.HOVER_STYLEBOX);
        GameEvents.RaiseTooltipRequested(cardUI.card);
    }

    public override void OnMouseExited()
    {
        if (!cardUI.Playable || cardUI.disabled) { return; }
        cardUI.panel.Set("theme_override_styles/panel", GameConstants.BASE_STYLEBOX);
        GameEvents.RaiseTooltipHide();
    }
}
