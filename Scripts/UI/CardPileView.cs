using Godot;

public partial class CardPileView : Control
{
    [Export] public CardPile cardPile;

    Label title;
    Button button;
    GridContainer cards;
    CardTooltipPopup cardTooltipPopup;

    public override void _Ready()
    {
        cardTooltipPopup = GetNode<CardTooltipPopup>("%CardTooltipPopup");
        title = GetNode<Label>("%Title");
        button = GetNode<Button>("%Button");
        cards = GetNode<GridContainer>("%Cards");
        button.Pressed += Hide;

        foreach (Node card in cards.GetChildren())
        {
            card.QueueFree();
        }
        cardTooltipPopup.Hide_Tooltip();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed(GameConstants.INPUT_UI_CANCEL))
        {
            if (cardTooltipPopup.Visible)
            {
                cardTooltipPopup.Hide_Tooltip();
            }
            else
            {
                Hide();
            }
        }
    }

    public void ShowCurrentView(string title, bool shuffled = false)
    {
        foreach (Node card in cards.GetChildren())
        {
            card.QueueFree();
        }
        cardTooltipPopup.Hide_Tooltip();

        this.title.Text = title;
        CallDeferred("UpdateView", shuffled);
    }

    private void UpdateView(bool shuffled)
    {
        if (cardPile == null) { return; }
        CardPile allCards = (CardPile)cardPile.Duplicate(true);
        if (shuffled) { allCards.Shuffle(); }

        foreach (Card card in allCards.cards)
        {
            PackedScene scene = GD.Load<PackedScene>(GameConstants.CARD_MENU_UI_SCENE);
            CardMenuUI newCard = (CardMenuUI)scene.Instantiate();
            cards.AddChild(newCard);
            newCard.card = card;
            newCard.OnTooltipRequested += cardTooltipPopup.Show_Tooltip;
        }

        Show();
    }
}
