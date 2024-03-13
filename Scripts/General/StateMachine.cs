using Godot;
using System.Linq;

public partial class StateMachine : Node
{
    [Export] public CardState currentState { get; private set; }
    public CardState[] states;

    public override void _Ready()
    {
        states = GetChildren().Where(
			(element) => element is CardState
		).Cast<CardState>().ToArray();
    }

    public void SwitchState<T>()
    {
        CardState newState = states.Where(
            (state) => state is T)
            .FirstOrDefault();

        // Not a valid state or repeating transition
        if (newState == null || currentState is T) { return; }
        
        // Disable Current State
        currentState.Notification(GameConstants.NOTIFICATION_EXIT_STATE);

        // Set and enable Current State
        currentState = newState;
        currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }
}
