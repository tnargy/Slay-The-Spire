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
    public static event Action OnPlayerHit;
    public static event Action OnPlayerDied;
    public static void RaiseHandDrawn () => OnHandDrawn?.Invoke();
    public static void RaiseHandDiscarded () => OnHandDiscarded?.Invoke();
    public static void RaiseEndTurn () => OnEndTurn?.Invoke();
    public static void RaisePlayerHit () => OnPlayerHit?.Invoke();
    public static void RaisePlayerDied () => OnPlayerDied?.Invoke();

    // Enemy Related
    public static event Action<Enemy> OnEnemyActionFinished;
    public static event Action OnEnemyTurnEnded;
    public static void RaiseEnemyActionFinsihed (Enemy enemy) => OnEnemyActionFinished?.Invoke(enemy);
    public static void RaiseEnemyTurnEnded () => OnEnemyTurnEnded?.Invoke();

    // Battle Related
    public static event Action<string, BattleOverPanel.Type> OnBattleOverScreenRequested;
    public static void RaiseBattleOverScreen (string text, BattleOverPanel.Type type) => OnBattleOverScreenRequested?.Invoke(text, type);
    public static event Action OnBattleWon;
    public static void RaiseBattleWon () => OnBattleWon?.Invoke();

    // Map Related
    public static event Action OnMapExited;
    public static void RaiseMapExited () => OnMapExited?.Invoke();
    
    // Shop Related
    public static event Action OnShopExited;
    public static void RaiseShopExited () => OnShopExited?.Invoke();
    
    // Campfire Related
    public static event Action OnCampfireExited;
    public static void RaiseCampfireExited () => OnCampfireExited?.Invoke();
    
    // BattleReward Related
    public static event Action OnBattleRewardExited;
    public static void RaiseBattleRewardExited () => OnBattleRewardExited?.Invoke();
 
    // TreasureRoom Related
    public static event Action OnTreasureRoomExited;
    public static void RaiseTreasureRoomExited () => OnTreasureRoomExited?.Invoke();
}