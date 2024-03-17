using Godot;
using System;
using System.Linq;

public partial class CardPile : Resource
{
    [Export] public Card[] cards;

    public event Action<int> OnCardPileChanged;
    public void RaiseCardPileChanged(int pileSize) => OnCardPileChanged?.Invoke(pileSize);

    public bool IsEmpty() => cards.Length == 0;
    public void Shuffle() => cards = cards.OrderBy(x=> Random.Shared.Next()).ToArray();

    public Card DrawCard()
    {
        Card draw = cards.First();
        cards = cards.Skip(1).ToArray();
        RaiseCardPileChanged(cards.Length);
        return draw;
    }

    public void AddCard(Card card)
    {
        if (cards == null)
        {
            cards = new Card[] { card };
        }
        cards = cards.Append(card).ToArray();
        RaiseCardPileChanged(cards.Length);
    }
    
    public void Clear()
    {
        Array.Clear(cards);
        RaiseCardPileChanged(cards.Length);
    }
}
