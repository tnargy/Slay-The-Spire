using Godot;

public partial class CardTooltipPopup : Control
{
    [Export] CenterContainer cardTooltip;
    [Export] RichTextLabel description;
    [Export] Color backgroundColor = new("00000079");
    ColorRect background;

    public override void _Ready()
    {
        background = GetNode<ColorRect>("%Background");
        background.Color = backgroundColor;
        foreach (CardMenuUI card in cardTooltip.GetChildren())
        {
            card.QueueFree();
        }
    }

    public void Hide_Tooltip()
    {
        if (!Visible) { return; }
        foreach (CardMenuUI card in cardTooltip.GetChildren())
        {
            card.QueueFree();
        }
        Hide();
    }

    public void Show_Tooltip(Card card)
    {
        PackedScene scene = GD.Load<PackedScene>(GameConstants.CARD_MENU_UI_SCENE);
        CardMenuUI newCard = (CardMenuUI)scene.Instantiate();
        cardTooltip.AddChild(newCard);

        newCard.OnTooltipRequested += (card) => Hide_Tooltip();
        newCard.card = card;
        description.Text = card.tooltopText;
        Show();
    }

    void OnGUIInput(InputEvent @event) 
    {
        if (@event.IsActionPressed(GameConstants.INPUT_LEFT_CLICK))
        {
            Hide_Tooltip();
        }
    }
}
