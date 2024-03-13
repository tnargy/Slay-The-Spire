using Godot;

public partial class Enemy : Area2D
{
    private EnemyStats _stats;
    [Export] public EnemyStats Stats
    {
        get => _stats;
        set
        {
            _stats = (EnemyStats)value.CreateInstance();
        }
    }

    [Export] Sprite2D sprite2D;
    [Export] StatsUI statsUI;
    [Export] IntentUI intentUI;
    [Export] Sprite2D selected;

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
        UpdateCharacter();
        SetupAI();
        Stats.OnStatsChanged += UpdateCharacter;
        Stats.OnStatsChanged += UpdateAction;
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
        if (!IsInstanceValid(this)) { return; }
        sprite2D.Texture = Stats.art;
        statsUI.UpdateStats(Stats);
    }

    public void TakeTurn()
    {
        Stats.Block = 0;
        if (CurrentAction == null) { return; }
        CurrentAction.PerformAction();
    }

    public void TakeDamage(int damage)
    {
        Stats.TakeDamage(damage);
        if (Stats.Health <= 0) { QueueFree(); }
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
