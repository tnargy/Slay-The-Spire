using System;
using System.Linq;
using Godot;

public partial class EnemyActionPicker : Node
{
    [Export] Enemy _enemy;
    public Enemy Enemy
    {
        get => _enemy;
        set
        {
            _enemy = value;
            foreach (EnemyAction action in GetChildren().Where(T => T is EnemyAction))
            {
                action.enemy = _enemy;
            }
        }
    }
    [Export] Node2D _target;
    public Node2D Target
    {
        get => _target;
        set
        {
            _target = value;
            foreach (EnemyAction action in GetChildren().Where(T => T is EnemyAction))
            {
                action.target = _target;
            }
        }
    }

    float totalWeight = 0.0f;

    public override void _Ready()
    {
        Target = (Node2D)GetTree().GetFirstNodeInGroup("Player");
        SetupChances();
    }

    public EnemyAction GetAction()
    {
        EnemyAction action = GetFirstConditionalAction();
        if (action != null) { return action; }
        return GetChanceBasedActions();
    }

    public EnemyAction GetChanceBasedActions()
    {
       var roll = Random.Shared.NextDouble() * totalWeight;
       foreach (EnemyAction action in GetChildren().Where(T => T is EnemyAction))
        {
            if (action == null || action.ActionType != EnemyAction.Type.CHANCE_BASED) { continue; }
            if (action.accumulatedWeight > roll) { return action; }
        }

        return null;
    }

    public EnemyAction GetFirstConditionalAction()
    {
        foreach (EnemyAction action in GetChildren().Where(T => T is EnemyAction))
        {
            if (action == null || action.ActionType != EnemyAction.Type.CONDITIONAL) { continue; }
            if (action.IsPerformable()) { return action; }
        }

        return null;
    }

    private void SetupChances()
    {
        foreach (EnemyAction action in GetChildren().Where(T => T is EnemyAction))
        {
            if (action == null || action.ActionType != EnemyAction.Type.CHANCE_BASED) { continue; }
            
            totalWeight += action.chanceWeight;
            action.accumulatedWeight = totalWeight;
        }
    }
}
