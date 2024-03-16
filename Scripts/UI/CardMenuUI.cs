using System;
using Godot;

public partial class CardMenuUI : CenterContainer
{
    public event Action<Card> OnTooltipRequested;
    void RaiseTooltipRequested(Card card) => OnTooltipRequested?.Invoke(card);

    [Export] Panel panel;
    [Export] Label cost;
    [Export] TextureRect icon;

    [Export] Card _card;
    public Card Card
    {
        get => _card;
        set
        {
            _card = value;
            cost.Text = _card.cost.ToString();
            icon.Texture = _card.icon;
        }
    }

    private void OnGUIInput(InputEvent @event) 
    {
        if (@event.IsActionPressed(GameConstants.INPUT_LEFT_CLICK))
        {
            RaiseTooltipRequested(Card);
        }
    }
    public void OnMouseEntered() => panel.Set("theme_override_styles/panel", GameConstants.HOVER_STYLEBOX);
    public void OnMouseExited() => panel.Set("theme_override_styles/panel", GameConstants.BASE_STYLEBOX);

}