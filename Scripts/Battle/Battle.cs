using Godot;

public partial class Battle : Node2D
{
    [Export] AudioStream music;
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
    EnemyHandler enemyHandler;
    SoundPlayer MusicPlayer;

    public override void _Ready()
    {
        MusicPlayer = GetNode<SoundPlayer>("/root/MusicPlayer");
        battleUI = GetNode<BattleUI>("BattleUI");
        playerHandler = GetNode<PlayerHandler>("PlayerHandler");
        player = GetNode<Player>("Player");
        enemyHandler = GetNode<EnemyHandler>("EnemyHandler");

        // Normally, we would do this on a 'Run'
        // level so we keep our health, gold, and deck
        // between battles.
        CharacterStats new_stats = (CharacterStats)CharStats.CreateInstance();
        battleUI.CharStats = new_stats;
        player.Stats = new_stats;

        enemyHandler.ChildOrderChanged += HandleEnemyChanges;
        GameEvents.OnEnemyTurnEnded += HandleEnemyTurnEnded;
        GameEvents.OnEndTurn += () => playerHandler.EndTurn();
        GameEvents.OnHandDiscarded += () => enemyHandler.StartTurn();
        GameEvents.OnPlayerDied += 
            () => GameEvents.RaiseBattleOverScreen("Game Over", BattleOverPanel.Type.LOSE);

        StartBattle(new_stats);
    }

    private void HandleEnemyChanges()
    {
        if (enemyHandler.GetChildCount() == 0)
        {
            GameEvents.RaiseBattleOverScreen("Victory", BattleOverPanel.Type.WIN);
        }
    }


    private void HandleEnemyTurnEnded()
    {
        playerHandler.StartTurn();
        enemyHandler.ResetEnemyActions();   
    }

    private void StartBattle(CharacterStats stats)
    {
        GetTree().Paused = false;
        MusicPlayer.Play(music, true);
        enemyHandler.ResetEnemyActions();
        playerHandler.StartBattle(stats);
    }

}
