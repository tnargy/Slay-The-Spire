using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class CardUI : Control
{
	[Export] private Card _card;
	public Card card
	{
		get => _card;
		set
		{
			_card = value;
			cost.Text = Convert.ToString(_card.cost);
			icon.Texture = _card.icon;
		}
	}
	[Export] private CharacterStats _characterStats;
    public CharacterStats CharStats
    {
        get => _characterStats;
        set
        {
            _characterStats = value;
			CharStats.OnStatsChanged += () => Playable = CharStats.CanPlayCard(card);
        }
    }

    public Panel panel { get; private set; }
	public Label cost { get; private set; }
	public TextureRect icon { get; private set; }
	public StateMachine stateMachine { get; private set; }
	public Area2D dropPointDetector { get; private set; }
	public HashSet<Area2D> targets { get; private set; }
    public Control parent { get; private set; }
	public Tween tween {get; private set; }
	public int origIndex = 0;

	private bool _playable = true;
	public bool Playable
	{
		get => _playable;
		set
		{
			if (!IsInstanceValid(this)) { return; }
			_playable = value;
			if (!_playable)
			{
				cost.AddThemeColorOverride("font_color", Colors.Red);
				icon.Modulate = new Color(1, 1, 1, 0.5f);
			}
			else
			{
				cost.RemoveThemeColorOverride("font_color");
				icon.Modulate = new Color(1, 1, 1, 1);
			}
		}
	}
	public bool disabled = false;

	public static event Action<CardUI> OnReparentRequest;
	public static void RaiseReparentRequested(CardUI cardui) => OnReparentRequest?.Invoke(cardui);
	private void HandleDropPointDetectorEntered(Area2D area2D) => targets.Add(area2D);
	private void HandleDropPointDetectorExited(Area2D area2D) => targets.Remove(area2D);

    public override void _Ready()
    {
		panel = GetNode<Panel>("Panel");
		cost = GetNode<Label>("Cost");
		icon = GetNode<TextureRect>("Icon");
		stateMachine = GetNode<StateMachine>("StateMachine");
		dropPointDetector = GetNode<Area2D>("DropPointDetector");
		targets = new();
		parent = GetParent<Control>();

        foreach (var item in stateMachine.states)
		{
			item.cardUI = this;
		}
		stateMachine.currentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);

		GameEvents.OnCardAimingStarted += HandleCardAimingOrDraggingStarted;
		GameEvents.OnCardDragStarted += HandleCardAimingOrDraggingStarted;
		GameEvents.OnCardAimingEnded += HandleCardAimingOrDraggingEnded;
		GameEvents.OnCardDragEnded += HandleCardAimingOrDraggingEnded;
    }

    private void HandleCardAimingOrDraggingStarted(CardUI usedCard)
    {
        if (usedCard == this) { return; }
		disabled = true;
    }

    private void HandleCardAimingOrDraggingEnded(CardUI uI)
    {
        disabled = false;
		Playable = CharStats.CanPlayCard(card);
    }

    public override void _Input(InputEvent @event) => stateMachine.currentState.OnInput(@event);
    private void OnGUIInput(InputEvent @event) => stateMachine.currentState.OnGUIInput(@event);
    public void OnMouseEntered() => stateMachine.currentState.OnMouseEntered();
    public void OnMouseExited() => stateMachine.currentState.OnMouseExited();

    public void AnimateToPosition(Vector2 newPosition, float duration)
	{
		tween = CreateTween().SetTrans(Tween.TransitionType.Circ).SetEase(Tween.EaseType.Out);
		tween.TweenProperty(this, "global_position", newPosition, duration);
	}

	public void Play()
	{
		if (card == null) { return; }
		
		card.Play(targets.ToArray(), CharStats);
		QueueFree();
	}
}
