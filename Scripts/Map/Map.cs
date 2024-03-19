using System;
using System.Linq;
using Godot;

public partial class Map : Node2D
{
    [Export] Node2D visuals;
    [Export] Node2D lines;
    [Export] Node2D rooms;
    [Export] Camera2D camera2D;

    MapGenerator mapGenerator;
    Room[,] mapData;
    int floorsClimbed;
    Room lastRoom;
    float cameraEdgeY;

    public override void _Ready()
    {
        mapGenerator = GetNode<MapGenerator>("%MapGenerator");
        cameraEdgeY = MapGenerator.Y_DIST * (MapGenerator.FLOORS - 1);

        GenerateNewMap();
        UnlockFloor(0);
    }

    public override void _Input(InputEvent @event)
    {
        float clampY;
        if (@event.IsActionPressed(GameConstants.INPUt_SCROLL_UP))
        {
            clampY = Math.Clamp(camera2D.Position.Y - GameConstants.SCROLL_SPEED, -cameraEdgeY, 0);
            camera2D.Position =  new Vector2(camera2D.Position.X, clampY);
        }
        else if (@event.IsActionPressed(GameConstants.INPUt_SCROLL_DOWN))
        {
            clampY = Math.Clamp(camera2D.Position.Y + GameConstants.SCROLL_SPEED, -cameraEdgeY, 0);
            camera2D.Position =  new Vector2(camera2D.Position.X, clampY);
        }
    }

    public void GenerateNewMap()
    {
        floorsClimbed = 0;
        mapData = mapGenerator.GenerateMap();
        CreateMap();
    }

    private void CreateMap()
    {
        for (int i = 0; i < MapGenerator.FLOORS; i++)
        {
            for (int j = 0; j < MapGenerator.MAP_WIDTH; j++)
            {
                if (mapData[i,j].nextRooms.Length > 0)
                {
                    SpawnRoom(mapData[i,j]);
                }
            }
        }

        // Boss Room doesn't have next rooms
        int middle = MapGenerator.MAP_WIDTH / 2;
        SpawnRoom(mapData[MapGenerator.FLOORS-1,middle]);

        int mapWidthPixels = MapGenerator.X_DIST * (MapGenerator.MAP_WIDTH - 1);
        float positionX = (GetViewportRect().Size.X - mapWidthPixels) / 2;
        float positionY = GetViewportRect().Size.Y / 2;
        visuals.Position = new Vector2(positionX, positionY);
    }

    private void SpawnRoom(Room room)
    {
        PackedScene scene = ResourceLoader.Load<PackedScene>(GameConstants.MAP_ROOM_SCENE);
        MapRoom newMapRoom = (MapRoom)scene.Instantiate();
        rooms.AddChild(newMapRoom);
        newMapRoom.room = room;
        newMapRoom.OnRoomSelected += HandleRoomSelected;
        ConnectLines(room);

        if (room.selected && room.row < floorsClimbed)
        {
            newMapRoom.ShowSelected();
        }
    }

    private void HandleRoomSelected(Room room)
    {
        foreach (MapRoom mapRoom in rooms.GetChildren())
        {
            if (mapRoom.room.row == room.row)
            {
                mapRoom.Available = false;
            }
        }

        lastRoom = room;
        floorsClimbed++;
        GameEvents.RaiseMapExited(room);
    }

    private void ConnectLines(Room room)
    {
        if (room.nextRooms.Length == 0) { return; }

        foreach (Room next in room.nextRooms)
        {
            PackedScene scene = ResourceLoader.Load<PackedScene>(GameConstants.MAP_LINE_SCENE);
            Line2D newMapLine = (Line2D)scene.Instantiate();
            newMapLine.AddPoint(room.position);
            newMapLine.AddPoint(next.position);
            lines.AddChild(newMapLine);
        }
    }

    public void UnlockFloor(int v)
    {
        foreach (MapRoom mapRoom in rooms.GetChildren())
        {
            if (mapRoom.room.row == v)
            {
                mapRoom.Available = true;
            }
        }
    }

    public void UnlockNextRooms()
    {
        foreach (MapRoom mapRoom in rooms.GetChildren())
        {
            if (lastRoom.nextRooms.Contains(mapRoom.room))
            {
                mapRoom.Available = true;
            }
        }
    }

    public void ShowMap()
    {
        Show();
        camera2D.Enabled = true;
    }

    public void HideMap()
    {
        Hide();
        camera2D.Enabled = false;
    }
}
