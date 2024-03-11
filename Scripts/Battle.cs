using Godot;

public partial class Battle : Node2D
{
    [Export] private CharacterStats _characterStats;
    public CharacterStats CharStats
    {
        get => _characterStats;
        set
        {
            _characterStats = value;
            battleUI.CharStats = _characterStats;
        }
    }
    BattleUI battleUI;
    PlayerHandler playerHandler;
    Player player;

    public override void _Ready()
    {
        battleUI = GetNode<BattleUI>("BattleUI");
        playerHandler = GetNode<PlayerHandler>("PlayerHandler");
        player = GetNode<Player>("Player");

        // Normally, we would do this on a 'Run'
        // level so we keep our health, gold, and deck
        // between battles.
        CharacterStats new_stats = (CharacterStats)CharStats.CreateInstance();
        battleUI.CharStats = new_stats;
        player.Stats = new_stats;

        GameEvents.OnEndTurn += () => playerHandler.EndTurn();
        // Temporary Code
        // TODO: Enemy AI
        GameEvents.OnHandDiscarded += () => playerHandler.StartTurn();

        StartBattle(new_stats);
    }

    private void StartBattle(CharacterStats stats)
    {
        GD.Print("FIGHT!");
        playerHandler.StartBattle(stats);
    }

}
