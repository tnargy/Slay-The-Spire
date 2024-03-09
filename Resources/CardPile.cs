using Godot;
using System;
using System.Linq;

public partial class CardPile : Resource
{
    [Export] Card[] cards;

    public event Action<int> OnCardPileChanged;
    private void RaiseCardPileChanged(int pileSize) => OnCardPileChanged?.Invoke(pileSize);

    bool IsEmpty() => cards.Length == 0;
    void Shuffle() => cards = cards.OrderBy(x=> Random.Shared.Next()).ToArray();

    Card DrawCard()
    {
        Card draw = cards.First();
        cards = cards.Skip(1).ToArray();
        RaiseCardPileChanged(cards.Length);
        return draw;
    }

    void AddCard(Card card)
    {
        cards.Append(card);
        RaiseCardPileChanged(cards.Length);
    }
    
    void Clear()
    {
        Array.Clear(cards);
        RaiseCardPileChanged(cards.Length);
    }
}
