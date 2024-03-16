using Godot;

public partial class CardPileButton : TextureButton
{
    [Export] Label counter;
    [Export] CardPile _cardPile;
    public CardPile cardPile
    {
        get => _cardPile;
        set 
        {
            _cardPile = value;
            _cardPile.OnCardPileChanged += (amount) => counter.Text = amount.ToString();
        }
    }

}
