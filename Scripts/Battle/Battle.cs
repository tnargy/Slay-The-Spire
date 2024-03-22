using System.Reflection.Metadata;
using Godot;

public partial class Battle : Node2D
{
    [Export] AudioStream music;
    [Export] public BattleStats battleStats;
    [Export] public CharacterStats characterStats;
    
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

        enemyHandler.ChildOrderChanged += HandleEnemyChanges;
        GameEvents.OnEnemyTurnEnded += HandleEnemyTurnEnded;
        GameEvents.OnEndTurn += HandleEndTurn;
        GameEvents.OnHandDiscarded += HandleHandDiscarded;
        GameEvents.OnPlayerDied += HandlePlayerDied;
    }

    public override void _ExitTree()
    {
        enemyHandler.ChildOrderChanged -= HandleEnemyChanges;
        GameEvents.OnEnemyTurnEnded -= HandleEnemyTurnEnded;
        GameEvents.OnEndTurn -= HandleEndTurn;
        GameEvents.OnHandDiscarded -= HandleHandDiscarded;
        GameEvents.OnPlayerDied -= HandlePlayerDied;
    }

    public void StartBattle()
    {
        GetTree().Paused = false;
        MusicPlayer.Play(music, true);

        battleUI.CharStats = characterStats;
        player.characterStats = characterStats;
        enemyHandler.Setup_Enemies(battleStats);
        enemyHandler.ResetEnemyActions();
        playerHandler.StartBattle(characterStats);
        battleUI.InitCardPileUI();
    }

    void HandleEndTurn() => playerHandler.EndTurn();
    void HandleHandDiscarded() => enemyHandler.StartTurn();
    void HandlePlayerDied() => GameEvents.RaiseBattleOverScreen("Game Over", BattleOverPanel.Type.LOSE);

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

}
