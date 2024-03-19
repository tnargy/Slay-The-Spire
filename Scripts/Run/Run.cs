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
    [Export] Map map;

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
        MapBtn.Pressed += ShowMap;
        BattleBtn.Pressed += () => ChangeView(GameConstants.BATTLE_SCENE);
        ShopBtn.Pressed += () => ChangeView(GameConstants.SHOP_SCENE);
        TreasureBtn.Pressed += () => ChangeView(GameConstants.TREASURE_SCENE);
        RewardBtn.Pressed += () => ChangeView(GameConstants.BATTLE_REWARD_SCENE);
        CampfireBtn.Pressed += () => ChangeView(GameConstants.CAMPFIRE_SCENE);
        

        GameEvents.OnBattleRewardExited += ShowMap;
        GameEvents.OnCampfireExited += ShowMap;
        GameEvents.OnShopExited += ShowMap;
        GameEvents.OnTreasureRoomExited += ShowMap;
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

    private void HandleMapExited(Room room)
    {
        switch (room.roomType)
        {
            case Room.RoomType.MONSTER:
                ChangeView(GameConstants.BATTLE_SCENE);
                break;
            case Room.RoomType.TREASURE:
                ChangeView(GameConstants.TREASURE_SCENE);
                break;
            case Room.RoomType.CAMPFIRE:
                ChangeView(GameConstants.CAMPFIRE_SCENE);
                break;
            case Room.RoomType.SHOP:
                ChangeView(GameConstants.SHOP_SCENE);
                break;
            case Room.RoomType.BOSS:
                ChangeView(GameConstants.BATTLE_SCENE);
                break;
        }
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
        map.GenerateNewMap();
        map.UnlockFloor(0);
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
        map.HideMap();

        return newView;
    }

    void ShowMap()
    {
        if (currentView.GetChildCount() > 0)
        {
            currentView.GetChild(0).QueueFree();
        }

        map.ShowMap();
        map.UnlockNextRooms();
    }

}
