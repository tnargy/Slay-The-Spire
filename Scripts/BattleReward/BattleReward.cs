using System;
using Godot;

public partial class BattleReward : Control
{
    [Export] RunStats runStats;
    [Export] Button backButton;
    
    VBoxContainer rewards;
    PackedScene rewardButtonScene = ResourceLoader.Load<PackedScene>("res://Scenes/BattleReward/reward_button.tscn");
    Texture2D goldIcon = ResourceLoader.Load<Texture2D>("res://Assets/gold.png");
    string goldText = " gold";
    Texture2D cardIcon = ResourceLoader.Load<Texture2D>("res://Assets/rarity.png");
    string cardText = "Add New Card";

    public override void _Ready()
    {
        backButton.Pressed += () => GameEvents.RaiseBattleRewardExited();
        rewards = GetNode<VBoxContainer>("%Rewards");

        foreach (Node node in rewards.GetChildren())
        {
            node.QueueFree();
        }

        // Test
        runStats = new();
        runStats.OnGoldChange += () => GD.Print("gold: ", runStats.Gold);
        AddGoldReward(100);
    }

    void AddGoldReward(int amount)
    {
        RewardButton goldReward = (RewardButton)rewardButtonScene.Instantiate();
        goldReward.rewardIcon = goldIcon;
        goldReward.rewardText = amount.ToString() + goldText;
        goldReward.Pressed += () => OnGoldRewardTaken(amount);
        rewards.CallDeferred("add_child", goldReward);
    }

    private void OnGoldRewardTaken(int amount)
    {
        if (runStats == null) { return; }
        runStats.Gold += amount;
    }
}
