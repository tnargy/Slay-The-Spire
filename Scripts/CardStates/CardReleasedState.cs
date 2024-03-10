using Godot;

public partial class CardReleasedState : CardState
{
    private bool played = false;

    protected override void EnterState()
    {

        if (cardUI.targets.Count > 0)
        {
            GameEvents.RaiseTooltipHide();
            played = true;
            cardUI.Play();
        }
    }

    public override void OnInput(InputEvent @event)
    {
        if (played) { return; }

        cardUI.stateMachine.SwitchState<CardBaseState>();
    }
}
