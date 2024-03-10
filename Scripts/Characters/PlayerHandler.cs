using System;
using Godot;

public partial class PlayerHandler : Node
{
    [Export] Hand hand;
    CharacterStats character;

    public void StartBattle(CharacterStats characterStats)
    {
        character = characterStats;
        character.drawPile = (CardPile)character.deck.Duplicate(true);
        character.drawPile.Shuffle();
        character.discardPile = new();
        StartTurn();
    }

    private void StartTurn()
    {
        character.Block = 0;
        character.ResetMana();
        DrawCards(character.cardsPerTurn);
    }

    private void DrawCards(int ammount)
    {
        var tween = CreateTween();
        for (int i = 0; i < ammount; i++)
        {
            tween.TweenCallback(Callable.From(DrawCard));
            tween.TweenInterval(GameConstants.HAND_DRAW_INTERVAL);
        }

        tween.Finished += () => GameEvents.RaiseHandDrawn();
    }
    
    private void DrawCard()
    {
        hand.AddCard(character.drawPile.DrawCard());
    }
}
