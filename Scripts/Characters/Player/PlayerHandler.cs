using Godot;

    

public partial class PlayerHandler : Node
{
    const float HAND_DRAW_INTERVAL = 0.25f;
    const float HAND_DISCARD_INTERVAL = 0.25f;

    Hand hand;
    CharacterStats character;

    public override void _Ready()
    {
        hand = GetNode<Hand>("%Hand");
        GameEvents.OnCardPlayed += HandleCardPlayed;
    }

    public override void _ExitTree()
    {
        GameEvents.OnCardPlayed -= HandleCardPlayed;
    }

    private void HandleCardPlayed(Card card)
    {
        if (card.duplicateOnPlayed) 
        {
            character.discardPile.AddCard(card);
        }
        character.discardPile.AddCard(card);
    }

    public void StartBattle(CharacterStats characterStats)
    {
        character = characterStats;
        character.drawPile = (CardPile)character.deck.Duplicate(true);
        character.drawPile.Shuffle();
        character.discardPile.cards = new Card[0];
        StartTurn();
    }

    public void StartTurn()
    {
        character.Block = 0;
        character.ResetMana();
        DrawCards(character.cardsPerTurn);
    }

    public void EndTurn()
    {
        hand.DisableHand();
        DiscardCards();
    }

    private void DiscardCards()
    {
        if (!IsInstanceValid(hand) || hand.GetChildCount() == 0)
        {
            GameEvents.RaiseHandDiscarded();
            return;
        }

        var tween = CreateTween();
        foreach (CardUI cardUI in hand.GetChildren())
        {
            tween.TweenCallback(Callable.From(() => character.discardPile.AddCard(cardUI.card)));
            tween.TweenCallback(Callable.From(() => hand.DiscardCard(cardUI)));
            tween.TweenInterval(HAND_DISCARD_INTERVAL);
        }

        tween.Finished += () => GameEvents.RaiseHandDiscarded();
    }

    private void DrawCards(int ammount)
    {
        var tween = CreateTween();
        for (int i = 0; i < ammount; i++)
        {
            tween.TweenCallback(Callable.From(DrawCard));
            tween.TweenInterval(HAND_DRAW_INTERVAL);
        }

        tween.Finished += () => GameEvents.RaiseHandDrawn();
    }
    
    private void DrawCard()
    {
        ReshuffleDiscardPile();
        hand.AddCard(character.drawPile.DrawCard());
        ReshuffleDiscardPile();
    }

    private void ReshuffleDiscardPile()
    {
        if (!character.drawPile.IsEmpty()) { return; }
        while (!character.discardPile.IsEmpty())
        {
            character.drawPile.AddCard(character.discardPile.DrawCard());
        }
        character.drawPile.Shuffle();
    }

}
