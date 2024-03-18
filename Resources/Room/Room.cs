using Godot;

public partial class Room : Resource
{
    public enum RoomType
    {
        NOT_ASSIGNED,
        MONSTER,
        TREASURE,
        CAMPFIRE,
        SHOP,
        BOSS
    }

    [Export] public RoomType roomType;
    [Export] public int row;
    [Export] public int column;
    [Export] public Vector2 position;
    [Export] public Room[] nextRooms;
    [Export] public bool selected = false;

    public override string ToString()
    {
        return $"{column} ({roomType.ToString()[0]}), ";
    }
}
