using Godot;
using System;

public partial class CharacterStats : Stats
{
    [Export] public CardPile startingDeck;
    [Export] public int cardsPerTurn;
    [Export] public int maxMana;

    public CardPile deck;
    public CardPile discardPile;
    public CardPile drawPile;

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
        CharacterStats instance = (CharacterStats)Duplicate();
        instance.Health = maxHealth;
        instance.Block = 0;
        instance.ResetMana();
        instance.deck = (CardPile)instance.startingDeck.Duplicate();
        instance.discardPile = new();
        instance.drawPile = new();
        return instance;
    }

    public override void TakeDamage(int damage)
    {
        var initHealth = Health;
        base.TakeDamage(damage);
        if (Health < initHealth) GameEvents.RaisePlayerHit();
    }
}
