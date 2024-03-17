using System;
using Godot;

public partial class CardMenuUI : CenterContainer
{
    public event Action<Card> OnTooltipRequested;
    void RaiseTooltipRequested(Card card) => OnTooltipRequested?.Invoke(card);

    [Export] CardVisuals visuals;
    [Export] Card _card;
    public Card card
    {
        get => _card;
        set
        {
            _card = value;
            visuals.card = _card;
        }
    }

    private void OnGUIInput(InputEvent @event) 
    {
        if (@event.IsActionPressed(GameConstants.INPUT_LEFT_CLICK))
        {
            RaiseTooltipRequested(card);
        }
    }
    public void OnMouseEntered() => visuals.panel.Set("theme_override_styles/panel", GameConstants.HOVER_STYLEBOX);
    public void OnMouseExited() => visuals.panel.Set("theme_override_styles/panel", GameConstants.BASE_STYLEBOX);

}