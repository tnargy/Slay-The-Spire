using Godot;
using System;

public partial class MainMenu : Control
{
    [Export] Button continueBtn;
    [Export] Button newGameBtn;
    [Export] Button exitGameBtn;

    public override void _Ready()
    {
        GetTree().Paused = false;
        newGameBtn.Pressed += HandleNewGameBtnPressed;
        continueBtn.Pressed += HandleContinueBtnPressed;
        exitGameBtn.Pressed += () => GetTree().Quit();
    }

    private void HandleNewGameBtnPressed()
    {
        PackedScene scene = ResourceLoader.Load<PackedScene>(GameConstants.CHAR_SELECTOR_SCENE);
        GetTree().ChangeSceneToPacked(scene);
    }

    private void HandleContinueBtnPressed()
    {
        throw new NotImplementedException();
    }

}
