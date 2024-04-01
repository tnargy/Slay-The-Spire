using Godot;

public partial class Campfire : Control
{
    [Export] TextureRect portrait;
    [Export] private CharacterStats _characterStats;
    public CharacterStats characterStats
    {
        get => _characterStats;
        set => SetStatus(value);
    }
    void SetStatus(CharacterStats value)
    {
        if (value == null) { return; }
        _characterStats = value;
        portrait.Texture = _characterStats.portrait;
        _characterStats.OnStatsChanged += UpdateCharacter;
        UpdateCharacter();
    }


    Button rest;
    Label heal;
    HealthUI healthUI;


    public override void _Ready()
    {
        healthUI = GetNode<HealthUI>("%HealthUI");
        rest = GetNode<Button>("%Rest");
        heal = GetNode<Label>("%Heal");
        rest.Pressed += HandleRest;
        SetStatus(_characterStats);
    }

    public override void _ExitTree() => _characterStats.OnStatsChanged -= UpdateCharacter;

    private void UpdateCharacter() => healthUI.UpdateStats(characterStats);

    private void HandleRest()
    {
        int amount = (int)(characterStats.maxHealth * 0.3f);
        characterStats.Heal(amount);
        heal.Text = $"+{amount}";
        Tween tween = CreateTween().SetTrans(Tween.TransitionType.Cubic).SetEase(Tween.EaseType.Out);
        tween.TweenProperty(heal, "modulate", Colors.White, 0.5f);
        tween.TweenInterval(0.3f);
        tween.TweenProperty(heal, "modulate", Colors.Transparent, 0.5f);

        tween.Finished += () => GameEvents.RaiseCampfireExited();
    }
}
