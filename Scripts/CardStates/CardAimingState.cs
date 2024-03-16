using Godot;

public partial class CardAimingState : CardState
{

    protected override void EnterState()
    {
        cardUI.targets.Clear();

        Vector2 offset = new(cardUI.parent.Size.X / 2, -cardUI.Size.Y / 2);
        offset.X -= cardUI.Size.X / 2;
        cardUI.AnimateToPosition(cardUI.parent.GlobalPosition + offset, 0.2f);
        cardUI.dropPointDetector.Monitoring = false;

        GameEvents.RaiseCardAimingStarted(cardUI);
    }

    protected override void ExitState()
    {
        GameEvents.RaiseCardAimingEnded(cardUI);
    }

    public override void OnInput(InputEvent @event)
    {
        // Cancel cast
        bool mouseAtBottom = cardUI.GetGlobalMousePosition().Y > GameConstants.MOUSE_Y_SNAPBACK_THRESHOLD;
        if ((@event is InputEventMouseMotion && mouseAtBottom) || @event.IsActionPressed(GameConstants.INPUT_RIGHT_CLICK))
        {
            // Cancel
            cardUI.stateMachine.SwitchState<CardBaseState>();
        }
        else if (@event.IsActionPressed(GameConstants.INPUT_LEFT_CLICK) || @event.IsActionReleased(GameConstants.INPUT_LEFT_CLICK))
        {
            // Release
            GetViewport().SetInputAsHandled();
            cardUI.stateMachine.SwitchState<CardReleasedState>();
        }
    }
}
