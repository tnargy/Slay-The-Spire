using System;

public class GameEvents
{
    // Card Related
    public static event Action<CardUI> OnCardDragStarted;
    public static event Action<CardUI> OnCardDragEnded;
    public static event Action<CardUI> OnCardAimingStarted;
    public static event Action<CardUI> OnCardAimingEnded;
    public static event Action<Card> OnCardPlayed;
    public static event Action<Card> OnTooltipRequested;
    public static event Action OnTooltipHide;
    public static void RaiseCardDragStarted(CardUI cardUI) => OnCardDragStarted?.Invoke(cardUI);
    public static void RaiseCardDragEnded(CardUI cardUI) => OnCardDragEnded?.Invoke(cardUI);
    public static void RaiseCardAimingStarted(CardUI cardUI) => OnCardAimingStarted?.Invoke(cardUI);
    public static void RaiseCardAimingEnded(CardUI cardUI) => OnCardAimingEnded?.Invoke(cardUI);
    public static void RaiseCardPlayed(Card card) => OnCardPlayed?.Invoke(card);
    public static void RaiseTooltipRequested(Card card) => OnTooltipRequested?.Invoke(card);
    public static void RaiseTooltipHide () => OnTooltipHide?.Invoke();

    // Player Related
    public static event Action OnHandDrawn;
    public static void RaiseHandDrawn () => OnHandDrawn?.Invoke();
}