using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class BattleReward : Control
{
    [Export] public RunStats runStats;
    [Export] public CharacterStats characterStats;
    [Export] Button backButton;
    
    VBoxContainer rewards;
    PackedScene rewardButtonScene = GD.Load<PackedScene>("res://Scenes/BattleReward/reward_button.tscn");
    PackedScene cardRewardScene = GD.Load<PackedScene>("res://Scenes/UI/card_rewards.tscn");
    Texture2D goldIcon = GD.Load<Texture2D>("res://Assets/gold.png");
    string goldText = " gold";
    Texture2D cardIcon = GD.Load<Texture2D>("res://Assets/rarity.png");
    string cardText = "Add New Card";

    float cardRewardTotalWeight = 0.0f;
    Dictionary<Card.Rarity, float> cardRarityWeights = new()
    {
        {Card.Rarity.COMMON, 0.0f},
        {Card.Rarity.UNCOMMON, 0.0f},
        {Card.Rarity.RARE, 0.0f},
    };

    public override void _Ready()
    {
        backButton.Pressed += () => GameEvents.RaiseBattleRewardExited();
        rewards = GetNode<VBoxContainer>("%Rewards");
        rewards.ChildOrderChanged += () => backButton.Disabled = rewards.GetChildCount() > 0 ? true : false;

        foreach (Node node in rewards.GetChildren())
        {
            node.QueueFree();
        }
    }

    public void AddCardReward()
    {
        RewardButton cardReward = (RewardButton)rewardButtonScene.Instantiate();
        cardReward.rewardIcon = cardIcon;
        cardReward.rewardText = cardText;
        cardReward.Pressed += ShowCardRewards;
        rewards.CallDeferred("add_child", cardReward);
    }

    public void AddGoldReward(int amount)
    {
        RewardButton goldReward = (RewardButton)rewardButtonScene.Instantiate();
        goldReward.rewardIcon = goldIcon;
        goldReward.rewardText = amount.ToString() + goldText;
        goldReward.Pressed += () => OnGoldRewardTaken(amount);
        rewards.CallDeferred("add_child", goldReward);
    }

    private void ShowCardRewards()
    {
        if (runStats == null || characterStats == null) { return; }

        CardRewards cardRewards = (CardRewards)cardRewardScene.Instantiate();
        AddChild(cardRewards);
        cardRewards.OnCardRewardSelected += OnCardRewardTaken;

        List<Card> cardRewardArray = new();
        List<Card> availableCards = ((CardPile)characterStats.draftCards.Duplicate(true)).cards.ToList();

        for (int i = 0; i < runStats.cardRewards; i++)
        {
            SetupCardChances();
            var roll = Random.Shared.NextDouble() * cardRewardTotalWeight;
            foreach (var rarity in cardRarityWeights)
            {
                if (rarity.Value > roll)
                {
                    ModifyWeights(rarity.Key);
                    Card pickedCard = GetRandomAvailableCard(availableCards, rarity.Key);
                    cardRewardArray.Add(pickedCard);
                    availableCards.Remove(pickedCard);
                    break;
                }
            }
        }

        cardRewards.Rewards = cardRewardArray.ToArray();
        cardRewards.Show();
    }

    private Card GetRandomAvailableCard(List<Card> cards, Card.Rarity key)
    {
        List<Card> allPossibleCards = cards.Where(
            (Card card) => card.rarity == key).ToList();

        return allPossibleCards[Random.Shared.Next(allPossibleCards.Count)];
    }

    private void ModifyWeights(Card.Rarity key)
    {
        if (key == Card.Rarity.RARE)
        {
            runStats.rareWeight = RunStats.BASE_RARE_WEIGHT;
        }
        else
        {
            runStats.rareWeight = Math.Clamp(runStats.rareWeight + 0.3f, RunStats.BASE_RARE_WEIGHT, 5.0f);
        }
    }

    private void SetupCardChances()
    {
        cardRewardTotalWeight = runStats.commonWeight + runStats.uncommonWeight + runStats.rareWeight;
        cardRarityWeights[Card.Rarity.COMMON] = runStats.commonWeight;
        cardRarityWeights[Card.Rarity.UNCOMMON] = runStats.commonWeight + runStats.uncommonWeight;
        cardRarityWeights[Card.Rarity.RARE] = cardRewardTotalWeight;
    }

    private void OnCardRewardTaken(Card card)
    {
        if (characterStats == null || card == null) { return; }
        characterStats.deck.AddCard(card);
    }

    private void OnGoldRewardTaken(int amount)
    {
        if (runStats == null) { return; }
        runStats.Gold += amount;
    }
}
