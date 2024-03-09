using Godot;
using System;

public partial class CardState : Node
{
    [Export] public States state;
    public CardUI cardUI;

    public override void _Notification(int what)
    {
        base._Notification(what);
        if (what == GameConstants.NOTIFICATION_ENTER_STATE)
        {
            EnterState();
        }
        else if (what == GameConstants.NOTIFICATION_EXIT_STATE)
        {
            ExitState();
        }
    }

    protected virtual void EnterState() {}
    protected virtual void ExitState() {}
    public virtual void OnInput(InputEvent @event) {}
    public virtual void OnGUIInput(InputEvent @event) {}
    public virtual void OnMouseEntered() {}
    public virtual void OnMouseExited() {}
}