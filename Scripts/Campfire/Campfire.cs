using Godot;

public partial class Campfire : Control
{
    [Export] Button button;

    public override void _Ready()
    {
        button.Pressed += () => GameEvents.RaiseCampfireExited();
    }
}
