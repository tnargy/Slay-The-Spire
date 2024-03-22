using System;
using System.Linq;
using Godot;

public partial class EnemyHandler : Node2D
{
    public override void _Ready()
    {
        GameEvents.OnEnemyActionFinished += HandleEnemyActionFinished;
    }

    public override void _ExitTree()
    {
        GameEvents.OnEnemyActionFinished -= HandleEnemyActionFinished;
    }

    public void ResetEnemyActions()
    {
        foreach (Enemy enemy in GetChildren().Where(T => T is Enemy))
        {
            enemy.CurrentAction = null;
            enemy.UpdateAction();
        }
    }

    public void StartTurn()
    {
        if (!IsInstanceValid(this) || GetChildCount() == 0) { return; }

        Enemy firstEnemy = GetChild<Enemy>(0);
        firstEnemy.TakeTurn();
    }

    public void Setup_Enemies(BattleStats battleStats)
    {
        if (battleStats == null ) { return; }

        foreach (Enemy enemy in GetChildren().Where(T => T is Enemy))
        {
            enemy.QueueFree();
        }

        var allNewEnemies = battleStats.enemies.Instantiate();
        foreach (Enemy enemy in allNewEnemies.GetChildren().Cast<Enemy>())
        {
            Enemy newEnemy = (Enemy)enemy.Duplicate();
            AddChild(newEnemy);
        }
        allNewEnemies.QueueFree();
    }

    private void HandleEnemyActionFinished(Enemy enemy)
    {
        if (enemy.GetIndex() == GetChildCount() - 1)
        {
            GameEvents.RaiseEnemyTurnEnded();
            return;
        }

        Enemy nextEnemy = GetChild<Enemy>(enemy.GetIndex() + 1);
        nextEnemy.TakeTurn();
    }

}
