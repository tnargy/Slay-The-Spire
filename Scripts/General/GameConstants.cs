using Godot;

public class GameConstants
{
    // Inputs
    public const string INPUT_LEFT_CLICK = "left_mouse";
    public const string INPUT_RIGHT_CLICK = "right_mouse";
    public const string INPUt_SCROLL_UP = "scroll_up";
    public const string INPUt_SCROLL_DOWN = "scroll_down";
    public const string INPUT_UI_CANCEL = "ui_cancel";
    public const int SCROLL_SPEED = 15;

    // Notifications
    public const int NOTIFICATION_ENTER_STATE = 5001;
    public const int NOTIFICATION_EXIT_STATE = 5002;

    // Stylebox
    public static readonly StyleBoxFlat BASE_STYLEBOX = GD.Load<StyleBoxFlat>("res://Scenes/UI/Card UI/card_base_stylebox.tres");
    public static readonly StyleBoxFlat DRAG_STYLEBOX = GD.Load<StyleBoxFlat>("res://Scenes/UI/Card UI/card_drag_stylebox.tres");
    public static readonly StyleBoxFlat HOVER_STYLEBOX = GD.Load<StyleBoxFlat>("res://Scenes/UI/Card UI/card_hover_stylebox.tres");
    public static readonly Material WHITE_SPRITE_MATERIAL = GD.Load<Material>("res://Resources/General/white_sprite_material.tres");

    // Player
    public static readonly CharacterStats ROGUE_STATS = GD.Load<CharacterStats>("res://Resources/Characters/Rogue/Rogue.tres");
    public static readonly CharacterStats WARRIOR_STATS = GD.Load<CharacterStats>("res://Resources/Characters/Warrior/Warrior.tres");
    public static readonly CharacterStats WIZZARD_STATS = GD.Load<CharacterStats>("res://Resources/Characters/Wizzard/Wizzard.tres");

    // Scenes
    public const string CHAR_SELECTOR_SCENE = "res://Scenes/UI/character_selector.tscn";
    public const string RUN_SCENE = "res://Scenes/Run/run.tscn";
    public const string BATTLE_SCENE = "res://Scenes/Battle/battle.tscn";
    public const string BATTLE_REWARD_SCENE = "res://Scenes/BattleReward/battle_reward.tscn";
    public const string CAMPFIRE_SCENE = "res://Scenes/Campfire/campfire.tscn";
    //public const string MAP_SCENE = "res://Scenes/Map/map.tscn";
    public const string MAP_ROOM_SCENE = "res://Scenes/Map/map_room.tscn";
    public const string MAP_LINE_SCENE = "res://Scenes/Map/map_line.tscn";
    public const string SHOP_SCENE = "res://Scenes/Shop/shop.tscn";
    public const string TREASURE_SCENE = "res://Scenes/Treasure/treasure.tscn";
    public const string CARD_MENU_UI_SCENE = "res://Scenes/UI/Card UI/card_menu_ui.tscn";

    // Icons
    public static readonly Texture2D MONSTER = GD.Load<Texture2D>("res://Assets/Tiny Dungeon/Tiles/tile_0103.png");
    public static readonly Texture2D TREASURE = GD.Load<Texture2D>("res://Assets/Tiny Dungeon/Tiles/tile_0089.png");
    public static readonly Texture2D CAMPFIRE = GD.Load<Texture2D>("res://Assets/Tiny Dungeon/Tiles/Heart.png");
    public static readonly Texture2D SHOP = GD.Load<Texture2D>("res://Assets/gold.png");
    public static readonly Texture2D BOSS = GD.Load<Texture2D>("res://Assets/Tiny Dungeon/Tiles/tile_0105.png");
}