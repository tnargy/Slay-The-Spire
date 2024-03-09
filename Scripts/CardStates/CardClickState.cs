using Godot;

public partial class CardClickState : CardState
{
    protected override void EnterState()
    {
        cardUI.dropPointDetector.Monitoring = true;
    }

    public override void OnInput(InputEvent @event)
    {
        if (@event is InputEventMouseMotion)
        {
            cardUI.stateMachine.SwitchState<CardDraggingState>();
        }
    }
}
