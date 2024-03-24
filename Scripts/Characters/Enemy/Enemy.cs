using Godot;

public partial class Enemy : Area2D
{
    [Export] EnemyStats _stats;
    public EnemyStats Stats
    {
        get => _stats;
        set => SetEnemyStats(value);
    }
    void SetEnemyStats(EnemyStats value)
    {
        if (value == null) { return; }
        _stats = (EnemyStats)value.CreateInstance();
        _stats.OnStatsChanged += UpdateCharacter;
        _stats.OnStatsChanged += UpdateAction;

        UpdateCharacter();
    }

    Sprite2D sprite2D;
    StatsUI statsUI;
    IntentUI intentUI;
    Sprite2D selected;

    EnemyActionPicker enemyAI;
    EnemyAction _currentAction;
    public EnemyAction CurrentAction
    {
        get => _currentAction;
        set
        {
            _currentAction = value;
            if (_currentAction != null) 
            { 
                intentUI.UpdateIntent(CurrentAction.intent);
            }
        }
    }
    
    void HandleAreaEntered(Area2D area2D) => selected.Show();
    void HandleAreaExited(Area2D area2D) => selected.Hide();

    public override void _Ready()
    {
        sprite2D = GetNode<Sprite2D>("Sprite2D");
        statsUI = GetNode<StatsUI>("StatsUI");
        intentUI = GetNode<IntentUI>("IntentUI");
        selected = GetNode<Sprite2D>("Selected");
        
        SetEnemyStats(_stats);
    }

    public void UpdateAction()
    {
        if (enemyAI == null) { return; }
        if (CurrentAction == null)
        {
            CurrentAction = enemyAI.GetAction();
            return;
        }

        var newCondAction = enemyAI.GetFirstConditionalAction();
        if (newCondAction != null && newCondAction != CurrentAction)
        {
            CurrentAction = newCondAction;
        }
    }


    void UpdateCharacter()
    {
        sprite2D.Texture = Stats.art;
        statsUI.UpdateStats(Stats);
        SetupAI();
    }

    public void TakeTurn()
    {
        Stats.Block = 0;
        if (CurrentAction == null) { return; }
        CurrentAction.PerformAction();
    }

    public void TakeDamage(int damage)
    {
        sprite2D.Material = GameConstants.WHITE_SPRITE_MATERIAL;

        Tween tween = CreateTween();
        tween.TweenCallback(Callable.From(() => Shaker.Shake(this, 16, 0.15f)));
        tween.TweenCallback(Callable.From(() => Stats.TakeDamage(damage)));
        tween.TweenInterval(0.17);

        tween.Finished += () => 
        { 
            sprite2D.Material = null;
            if (Stats.Health <= 0) 
            { 
                QueueFree(); 
            }
        };

    }

    void SetupAI()
    {
        if (enemyAI == null) 
        {
            EnemyActionPicker newAI = (EnemyActionPicker)Stats.ai.Instantiate();
            AddChild(newAI);
            enemyAI = newAI;
            enemyAI.Enemy = this;
        }
    }
}
