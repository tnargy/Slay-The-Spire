using System;
using Godot;

public partial class CardRewards : ColorRect
{
    [Export] HBoxContainer cards;
    [Export] Button skipButton;
    [Export] Button takeButton;
    [Export] Card[] _rewards;
    public Card[] Rewards
    {
        get => _rewards;
        set => SetRewards(value);
    }

    private void SetRewards(Card[] value)
    {
        _rewards = value;
        ClearRewards();
        foreach (Card card in _rewards)
        {
            PackedScene scene = GD.Load<PackedScene>(GameConstants.CARD_MENU_UI_SCENE);
            CardMenuUI newCard = (CardMenuUI)scene.Instantiate();
            newCard.card = card;
            cards.AddChild(newCard);
            newCard.OnTooltipRequested += ShowTooltip;
        }
    }


    CardTooltipPopup cardTooltipPopup;
    Card selectedCard;
    public event Action<Card> OnCardRewardSelected;
    void RaiseCardRewardSelected (Card card) => OnCardRewardSelected?.Invoke(card);

    public override void _Ready()
    {
        cardTooltipPopup = GetNode<CardTooltipPopup>("%CardTooltipPopup");
        skipButton.Pressed += HandleSkipReward;
        takeButton.Pressed += HandleTakeReward;

        SetRewards(Rewards);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed(GameConstants.INPUT_UI_CANCEL))
        {
            if (cardTooltipPopup.Visible)
            {
                cardTooltipPopup.Hide_Tooltip();
            }
        }
    }

    private void ClearRewards()
    {
        foreach (Node node in cards.GetChildren())
        {
            node.QueueFree();
        }
        cardTooltipPopup.Hide_Tooltip();
        selectedCard = null;
    }

    private void ShowTooltip(Card card)
    {
        selectedCard = card;
        cardTooltipPopup.Show_Tooltip(card);
    }

    private void HandleTakeReward()
    {
        RaiseCardRewardSelected(selectedCard);
        QueueFree();
    }

    private void HandleSkipReward()
    {
        RaiseCardRewardSelected(null);
        QueueFree();
    }

}