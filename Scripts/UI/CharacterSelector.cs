using Godot;

public partial class CharacterSelector : Control
{
    [Export] Button startBtn;
    [Export] Button warrior;
    [Export] Button wizzard;
    [Export] Button rogue;

    [Export] Label title;
    [Export] Label desc;
    [Export] TextureRect portrait;

    [Export] public RunStartup runStartup;

    CharacterStats _characterStatus;
    public CharacterStats Stats
    {
        get => _characterStatus;
        set
        {
            _characterStatus = value;
            title.Text = _characterStatus.name;
            desc.Text = _characterStatus.desc;
            portrait.Texture = _characterStatus.portrait;
        }
    }

    public override void _Ready()
    {
        GetTree().Paused = false;
        Stats = GameConstants.WARRIOR_STATS;

        startBtn.Pressed += HandleStartBtn;
        warrior.Pressed += () => Stats = GameConstants.WARRIOR_STATS;
        wizzard.Pressed += () => Stats = GameConstants.WIZZARD_STATS;
        rogue.Pressed += () => Stats = GameConstants.ROGUE_STATS;
    }

    private void HandleStartBtn()
    {
        runStartup.runType = RunStartup.RunType.NEW;
        runStartup.selectedCharacterStats = Stats;
        PackedScene scene = GD.Load<PackedScene>(GameConstants.RUN_SCENE);
        GetTree().ChangeSceneToPacked(scene);
    }

}
