using Godot;
using System;

public partial class CharacterStats : Stats
{
    [Export] CardPile startingDeck;
    [Export] int cardsPerTurn;
    [Export] public int maxMana;

    private CardPile deck;
    private CardPile discardPile;
    private CardPile drawPile;

    private int _mana;
    public int Mana
    {
        get => _mana;
        set
        {
            _mana = Math.Clamp(value, 0, 999);
            RaiseStatsChanged();
        }
    }

    public void ResetMana() => Mana = maxMana;

    public bool CanPlayCard(Card card) => Mana >= card.cost;

    public override Resource CreateInstance()
    {
        CharacterStats instance = (CharacterStats)this.Duplicate();
        instance.Health = maxHealth;
        instance.Block = 0;
        instance.ResetMana();
        instance.deck = (CardPile)instance.startingDeck.Duplicate();
        instance.discardPile = new();
        instance.drawPile = new();
        return instance;
    }
}
