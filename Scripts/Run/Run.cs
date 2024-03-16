using Godot;

public partial class Run : Node
{
    Node currentView;
    Button BattleBtn;
    Button RewardBtn;
    Button CampfireBtn;
    Button MapBtn;
    Button ShopBtn;
    Button TreasureBtn;

    CharacterStats character;
    [Export] public RunStartup runStartup;

    public override void _Ready()
    {
        currentView = GetNode<Node>("CurrentView");
        
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
        

        GameEvents.OnBattleWon += () => ChangeView(GameConstants.BATTLE_REWARD_SCENE);
        GameEvents.OnBattleRewardExited += () => ChangeView(GameConstants.MAP_SCENE);
        GameEvents.OnCampfireExited += () => ChangeView(GameConstants.MAP_SCENE);
        GameEvents.OnShopExited += () => ChangeView(GameConstants.MAP_SCENE);
        GameEvents.OnTreasureRoomExited += () => ChangeView(GameConstants.MAP_SCENE);
        GameEvents.OnMapExited += HandleMapExited;

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

    private void StartRun()
    {
        GD.Print("TODO: procedurally generate map");
    }

    private void ChangeView(string path)
    {
        if (currentView.GetChildCount() > 0)
        {
            currentView.GetChild(0).QueueFree();
        }

        GetTree().Paused = false;
        PackedScene scene = ResourceLoader.Load<PackedScene>(path);
        Node newView = scene.Instantiate();
        currentView.AddChild(newView);   
    }

}