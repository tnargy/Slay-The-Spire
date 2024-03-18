using System;
using Godot;

public partial class Run : Node
{
    Node currentView;
    GoldUI goldUI;
    CardPileButton deckButton;
    CardPileView deckView;

    // DEBUG Menu
    Button BattleBtn;
    Button RewardBtn;
    Button CampfireBtn;
    Button MapBtn;
    Button ShopBtn;
    Button TreasureBtn;

    RunStats stats;
    CharacterStats character;
    [Export] public RunStartup runStartup;

    public override void _Ready()
    {
        currentView = GetNode<Node>("%CurrentView");
        goldUI = GetNode<GoldUI>("%GoldUI");
        deckButton = GetNode<CardPileButton>("%DeckButton");
        deckView = GetNode<CardPileView>("%DeckView");
        
        // DEBUG Menu
        MapBtn = GetNode<Button>("%MapBtn");
        BattleBtn = GetNode<Button>("%BattleBtn");
        ShopBtn = GetNode<Button>("%ShopBtn");
        TreasureBtn = GetNode<Button>("%TreasureBtn");
        RewardBtn = GetNode<Button>("%RewardBtn");
        CampfireBtn = GetNode<Button>("%CampfireBtn");
        MapBtn.Pressed += () => ChangeView(GameConstants.MAP_SCENE);
        BattleBtn.Pressed += () => ChangeView(GameConstants.BATTLE_SCENE);
        ShopBtn.Pressed += () => ChangeView(GameConstants.SHOP_SCENE);
        TreasureBtn.Pressed += () => ChangeView(GameConstants.TREASURE_SCENE);
        RewardBtn.Pressed += () => ChangeView(GameConstants.BATTLE_REWARD_SCENE);
        CampfireBtn.Pressed += () => ChangeView(GameConstants.CAMPFIRE_SCENE);
        

        GameEvents.OnBattleRewardExited += () => ChangeView(GameConstants.MAP_SCENE);
        GameEvents.OnCampfireExited += () => ChangeView(GameConstants.MAP_SCENE);
        GameEvents.OnShopExited += () => ChangeView(GameConstants.MAP_SCENE);
        GameEvents.OnTreasureRoomExited += () => ChangeView(GameConstants.MAP_SCENE);
        GameEvents.OnMapExited += HandleMapExited;
        GameEvents.OnBattleWon += HandleBattleWon;

        if (runStartup == null) { return; }
        switch (runStartup.runType)
        {
            case RunStartup.RunType.NEW:
                character = (CharacterStats)runStartup.selectedCharacterStats.CreateInstance();
                StartRun();
                break;
            case RunStartup.RunType.CONTINUE:
                GD.Print("TODO: load previous Run");
                break;
        }
    }

    private void HandleMapExited()
    {
        GD.Print("TODO: from the MAP, change view based on room type");
    }

    private void HandleBattleWon()
    {
        BattleReward rewardScene = (BattleReward)ChangeView(GameConstants.BATTLE_REWARD_SCENE);
        rewardScene.runStats = stats;
        rewardScene.characterStats = character;

        // DEBUG
        rewardScene.AddGoldReward(100);
        rewardScene.AddCardReward();
    }

    private void StartRun()
    {
        stats = new();
        SetupTopBar();
        GD.Print("TODO: procedurally generate map");
    }

    private void SetupTopBar()
    {
        goldUI.runStats = stats;

        deckButton.cardPile = character.deck;
        deckView.cardPile = character.deck;
        deckButton.cardPile.RaiseCardPileChanged(deckButton.cardPile.cards.Length);
        deckButton.Pressed += () => deckView.ShowCurrentView("Deck");
    }

    private Node ChangeView(string path)
    {
        if (currentView.GetChildCount() > 0)
        {
            currentView.GetChild(0).QueueFree();
        }

        GetTree().Paused = false;
        PackedScene scene = ResourceLoader.Load<PackedScene>(path);
        Node newView = scene.Instantiate();
        currentView.AddChild(newView);

        return newView;
    }

}
