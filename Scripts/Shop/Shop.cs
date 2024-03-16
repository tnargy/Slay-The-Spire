using Godot;

public partial class Shop : Control
{
    [Export] Button button;

    public override void _Ready()
    {
        button.Pressed += () => GameEvents.RaiseShopExited();
    }
}
