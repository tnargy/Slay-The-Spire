using Godot;

public partial class Map : Control
{
    [Export] Button button;

    public override void _Ready()
    {
        button.Pressed += () => GameEvents.RaiseMapExited();
    }
}
