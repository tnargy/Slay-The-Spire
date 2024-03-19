using Godot;
using System;
using System.Collections.Generic;

public partial class MapRoom : Area2D
{
    [Export] AnimationPlayer animationPlayer;
    [Export] Sprite2D sprite2D;
    [Export] Line2D line2D;

    Dictionary<Room.RoomType, (Texture2D, Vector2)> ICONS = new()
    {
        {Room.RoomType.NOT_ASSIGNED, (null, Vector2.One)},
        {Room.RoomType.MONSTER, (GameConstants.MONSTER, Vector2.One)},
        {Room.RoomType.TREASURE, (GameConstants.TREASURE, Vector2.One)},
        {Room.RoomType.CAMPFIRE, (GameConstants.CAMPFIRE, new Vector2(0.6f, 0.6f))},
        {Room.RoomType.SHOP, (GameConstants.SHOP, new Vector2(0.6f, 0.6f))},
        {Room.RoomType.BOSS, (GameConstants.BOSS, new Vector2(1.25f,1.25f))}
    };

    bool _available;
    public bool Available
    {
        get => _available;
        set => SetAvailable(value);
    }
    Room _room;
    public Room room
    {
        get => _room;
        set => SetRoom(value);
    }

    private void SetAvailable(bool value)
    {
        _available = value;
        if (_available) { animationPlayer.Play("Highlight"); }
        else if (!room.selected) { animationPlayer.Play("RESET"); }
    }

    private void SetRoom(Room value)
    {
        _room = value;
        Position = room.position;
        line2D.RotationDegrees = Random.Shared.Next(360);
        sprite2D.Texture = ICONS[room.roomType].Item1;
        sprite2D.Scale = ICONS[room.roomType].Item2;
    }

    public event Action<Room> OnRoomSelected;
    void RaiseRoomSelected (Room room) => OnRoomSelected?.Invoke(room);

    public override void _Ready()
    {
        animationPlayer.AnimationFinished += HandleAnimationFinished;
    }

    public override void _InputEvent(Viewport viewport, InputEvent @event, int shapeIdx)
    {
        if (!Available || !@event.IsActionPressed(GameConstants.INPUT_LEFT_CLICK))
        {
            return;
        }

        room.selected = true;
        animationPlayer.Play("Select");
    }

    private void HandleAnimationFinished(StringName animName)
    {
        if (animName == "Select") { RaiseRoomSelected(room); }
    }

    public void ShowSelected()
    {
        line2D.Modulate = Colors.White;
    }
}
