using Godot;

public partial class CardVisuals : Control
{
    [Export] public Panel panel;
    [Export] public Label cost;
    [Export] public TextureRect icon;
    [Export] TextureRect rarity;

    [Export] Card _card;
    public Card card
    {
        get => _card;
        set => SetCard(value);
    }

    async void SetCard(Card value)
    {
        if (!IsNodeReady())
        {
            await ToSignal(GetParent(), SignalName.Ready);
        }

        _card = value;
        cost.Text = _card.cost.ToString();
        icon.Texture = _card.icon;
        rarity.Modulate = Card.RARITY_COLORS[card.rarity];
    }

    public override void _Ready()
    {
        // SetCard(card);
    }

    public void OnMouseEntered() => panel.Set("theme_override_styles/panel", GameConstants.HOVER_STYLEBOX);
    public void OnMouseExited() => panel.Set("theme_override_styles/panel", GameConstants.BASE_STYLEBOX);
}
