using System;
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
    EnemyHandler enemyHandler;

    public override void _Ready()
    {
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

        GameEvents.OnEnemyTurnEnded += HandleEnemyTurnEnded;
        GameEvents.OnEndTurn += () => playerHandler.EndTurn();
        GameEvents.OnHandDiscarded += () => enemyHandler.StartTurn();
        GameEvents.OnPlayerDied += HandlePlayerDied;
        enemyHandler.ChildOrderChanged += HandleEnemyChanges;

        StartBattle(new_stats);
    }

    private void HandlePlayerDied()
    {
        GD.Print("Game Over");
    }


    private void HandleEnemyChanges()
    {
        if (enemyHandler.GetChildCount() == 0)
        {
            GD.Print("Victory");
        }
    }


    private void HandleEnemyTurnEnded()
    {
        playerHandler.StartTurn();
        enemyHandler.ResetEnemyActions();   
    }

    private void StartBattle(CharacterStats stats)
    {
        enemyHandler.ResetEnemyActions();
        playerHandler.StartBattle(stats);
    }

}
