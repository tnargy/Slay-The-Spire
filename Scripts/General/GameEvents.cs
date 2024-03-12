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
    public static void RaiseCardDragStarted (CardUI cardUI) => OnCardDragStarted?.Invoke(cardUI);
    public static void RaiseCardDragEnded (CardUI cardUI) => OnCardDragEnded?.Invoke(cardUI);
    public static void RaiseCardAimingStarted (CardUI cardUI) => OnCardAimingStarted?.Invoke(cardUI);
    public static void RaiseCardAimingEnded (CardUI cardUI) => OnCardAimingEnded?.Invoke(cardUI);
    public static void RaiseCardPlayed (Card card) => OnCardPlayed?.Invoke(card);
    public static void RaiseTooltipRequested (Card card) => OnTooltipRequested?.Invoke(card);
    public static void RaiseTooltipHide () => OnTooltipHide?.Invoke();

    // Player Related
    public static event Action OnHandDrawn;
    public static event Action OnHandDiscarded;
    public static event Action OnEndTurn;
    public static event Action OnPlayerDied;
    public static void RaiseHandDrawn () => OnHandDrawn?.Invoke();
    public static void RaiseHandDiscarded () => OnHandDiscarded?.Invoke();
    public static void RaiseEndTurn () => OnEndTurn?.Invoke();
    public static void RaisePlayerDied () => OnPlayerDied?.Invoke();

    // Enemy Related
    public static event Action<Enemy> OnEnemyActionFinished;
    public static event Action OnEnemyTurnEnded;
    public static void RaiseEnemyActionFinsihed (Enemy enemy) => OnEnemyActionFinished?.Invoke(enemy);
    public static void RaiseEnemyTurnEnded () => OnEnemyTurnEnded?.Invoke();
}