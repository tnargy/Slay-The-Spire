using System.Linq;
using Godot;

public partial class EnemyHandler : Node2D
{
    public override void _Ready()
    {
        GameEvents.OnEnemyActionFinished += HandleEnemyActionFinished;
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
        if (GetChildCount() == 0) { return; }

        Enemy firstEnemy = GetChild<Enemy>(0);
        firstEnemy.TakeTurn();
    }
}