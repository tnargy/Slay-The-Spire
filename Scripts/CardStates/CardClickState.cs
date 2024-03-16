using Godot;

public partial class CardClickState : CardState
{
    protected override void EnterState()
    {
        cardUI.dropPointDetector.Monitoring = true;
        cardUI.origIndex = cardUI.GetIndex();

        if (!cardUI.Playable || cardUI.disabled) { return; }
        cardUI.panel.Set("theme_override_styles/panel", GameConstants.HOVER_STYLEBOX);
        GameEvents.RaiseTooltipRequested(cardUI.card);
    }

    protected override void ExitState()
    {
        if (!cardUI.Playable || cardUI.disabled) { return; }
        cardUI.panel.Set("theme_override_styles/panel", GameConstants.BASE_STYLEBOX);
        GameEvents.RaiseTooltipHide();
    }

    public override void OnInput(InputEvent @event)
    {
        if (@event is InputEventMouseMotion)
        {
            cardUI.stateMachine.SwitchState<CardDraggingState>();
        }
    }
}
