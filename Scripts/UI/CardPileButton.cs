using System;
using Godot;

public partial class CardPileButton : TextureButton
{
    [Export] CardPile _cardPile;
    public CardPile cardPile
    {
        get => _cardPile;
        set 
        {
            _cardPile = value;
            _cardPile.OnCardPileChanged += HandleCardPileChanged;
        }
    }

    private void HandleCardPileChanged(int amount)
    {
        counter.Text = amount.ToString();
    }


    Label counter;

    public override void _Ready()
    {
        counter = GetNode<Label>("Label");
    }
}
