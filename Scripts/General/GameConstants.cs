using Godot;

public class GameConstants
{
    // Inputs
    public const string INPUT_LEFT_CLICK = "left_mouse";
    public const string INPUT_RIGHT_CLICK = "right_mouse";

    // Notifications
    public const int NOTIFICATION_ENTER_STATE = 5001;
    public const int NOTIFICATION_EXIT_STATE = 5002;

    // Aiming
    public const int MOUSE_Y_SNAPBACK_THRESHOLD = 600;
    public const int ARC_POINTS = 8;

    // Stylebox
    public static readonly StyleBoxFlat BASE_STYLEBOX = (StyleBoxFlat)GD.Load("res://Scenes/Card UI/card_base_stylebox.tres");
    public static readonly StyleBoxFlat DRAG_STYLEBOX = (StyleBoxFlat)GD.Load("res://Scenes/Card UI/card_drag_stylebox.tres");
    public static readonly StyleBoxFlat HOVER_STYLEBOX = (StyleBoxFlat)GD.Load("res://Scenes/Card UI/card_hover_stylebox.tres");
}