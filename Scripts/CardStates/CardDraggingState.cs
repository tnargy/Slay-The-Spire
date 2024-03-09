using Godot;

public partial class CardDraggingState : CardState
{
    protected override void EnterState()
    {
        cardUI.panel.Set("theme_override_styles/panel", GameConstants.DRAG_STYLEBOX);
        Node ui_layer = GetTree().GetFirstNodeInGroup("UI_Layer");
        if (ui_layer != null)
        {
            cardUI.Reparent(ui_layer);
        }
        GameEvents.RaiseCardDragStarted(cardUI);
    }

    protected override void ExitState()
    {
        GameEvents.RaiseCardDragEnded(cardUI);
    }

    public override void OnInput(InputEvent @event)
    {
        
        if (@event is InputEventMouseMotion)
        {
            if (cardUI.card.IsSingleTargeted() && cardUI.targets.Count > 0)
            {
                // Transition to Aiming
                cardUI.stateMachine.SwitchState<CardAimingState>();
                return;
            }
            
            cardUI.GlobalPosition = cardUI.GetGlobalMousePosition() - cardUI.PivotOffset;
        }
        else if (@event.IsActionPressed(GameConstants.INPUT_RIGHT_CLICK))
        {
            // Return to Base
            cardUI.stateMachine.SwitchState<CardBaseState>();
        }
        else if (@event.IsActionPressed(GameConstants.INPUT_LEFT_CLICK))
        {
            // Release
            GetViewport().SetInputAsHandled();
            cardUI.stateMachine.SwitchState<CardReleasedState>();
        }
    }
}
