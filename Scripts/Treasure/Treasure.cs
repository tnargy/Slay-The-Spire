using Godot;

public partial class Treasure : Control
{
    [Export] Button button;

    public override void _Ready()
    {
        button.Pressed += () => GameEvents.RaiseTreasureRoomExited();
    }
}
