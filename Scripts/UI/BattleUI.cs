using System;
using Godot;

public partial class BattleUI : CanvasLayer
{
    [Export] CardPileButton drawPileButton;
    [Export] CardPileButton discardPileButton;

    [Export] private CharacterStats _characterStats;
    public CharacterStats CharStats
    {
        get => _characterStats;
        set
        {
            _characterStats = value;
            hand.CharStats = _characterStats;
            mana.CharStats = _characterStats;
        }
    }
    Hand hand;
    ManaUI mana;
    Button endTurn;
    CardPileView drawPileView;
    CardPileView discardPileView;

    public override void _Ready()
    {
        hand = GetNode<Hand>("Hand");
        mana = GetNode<ManaUI>("ManaUI");
        endTurn = GetNode<Button>("%EndTurn");
        drawPileView = GetNode<CardPileView>("%DrawView");
        discardPileView = GetNode<CardPileView>("%DiscardView");

        GameEvents.OnHandDrawn += () => endTurn.Disabled = false;
        endTurn.Pressed += HandleEndTurn;
        drawPileButton.Pressed += () => drawPileView.ShowCurrentView("Draw Pile", true);
        discardPileButton.Pressed += () => discardPileView.ShowCurrentView("Discard Pile");
    }

    public void InitCardPileUI()
    {
        drawPileButton.cardPile = CharStats.drawPile;
        drawPileView.cardPile = CharStats.drawPile;
        discardPileButton.cardPile = CharStats.discardPile;
        discardPileView.cardPile = CharStats.discardPile;
        discardPileButton.cardPile.RaiseCardPileChanged(discardPileButton.cardPile.cards.Length);
    }

    private void HandleEndTurn()
    {
        endTurn.Disabled = true;
        GameEvents.RaiseEndTurn();
    }

}
