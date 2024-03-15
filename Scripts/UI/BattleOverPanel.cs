using Godot;

public partial class BattleOverPanel : Panel
{
    public enum Type { WIN, LOSE };

    Label label;
    Button continueBtn;
    Button restartBtn;

    public override void _Ready()
    {
        label = GetNode<Label>("%Label");
        continueBtn = GetNode<Button>("%ContinueBtn");
        restartBtn = GetNode<Button>("%RestartBtn");

        continueBtn.Pressed += () => GetTree().Quit();;
        restartBtn.Pressed += () => GetTree().ReloadCurrentScene();
        GameEvents.OnBattleOverScreenRequested += ShowScreen;
    }

    void ShowScreen(string text, Type type)
    {
        label.Text = text;
        continueBtn.Visible = type == Type.WIN;
        restartBtn.Visible = type == Type.LOSE;
        Show();
        GetTree().Paused = true;
    }
}