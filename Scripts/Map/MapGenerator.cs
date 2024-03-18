using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class MapGenerator : Node
{
    const int X_DIST = 30;
    const int Y_DIST = 25;
    const int PLACEMENT_RANDOMNESS = 5;
    const int FLOORS = 15;
    const int MAP_WIDTH = 7;
    const int PATHS = 6;
    const float MONSTER_ROOM_WEIGHT = 10.0f;
    const float CAMPFIRE_ROOM_WEIGHT = 4.0f;
    const float SHOP_ROOM_WEIGHT = 2.5f;

    Dictionary<Room.RoomType, float> randomRoomTypeWeights = new()
    {
        {Room.RoomType.MONSTER, 0.0f},
        {Room.RoomType.CAMPFIRE, 0.0f},
        {Room.RoomType.SHOP, 0.0f}
    };
    float randomRoomTypeTotalWeights = 0;
    Room[,] mapData = new Room[FLOORS, MAP_WIDTH];

    public override void _Ready()
    {
        GenerateMap();
    }

    Room[,] GenerateMap()
    {
        mapData = GenerateInitGrid();
        
        int[] startingPoints = GetRandomStartingPoints();

       foreach ( int j in startingPoints)
        {
            var cur_j = j;
            for (int i = 0; i < FLOORS-1; i++)
            {
                cur_j = SetupConnection(i, cur_j);
            }
        }

        SetupBossRoom();
        SetupRandomRoomWeights();
        SetupRoomTypes();

        // DEBUG
        // for (int i = 0; i < FLOORS; i++)
        // {
        //     string floor = $"floor {i}:\t[";
        //     for (int j = 0; j < MAP_WIDTH; j++)
        //     {
        //         if (mapData[i,j].nextRooms.Length>0 || mapData[i,j].roomType == Room.RoomType.BOSS)
        //         {
        //             floor += $"{mapData[i,j]}";
        //         }
        //     }
        //     GD.Print(floor+"]");
        // }
        
        return mapData;
    }

    private void SetupRoomTypes()
    {
        // first floore always battle
        for (int j = 0; j < MAP_WIDTH; j++)
        {
            if (mapData[0,j].nextRooms.Length>0)
            {
                mapData[0,j].roomType = Room.RoomType.MONSTER;                
            }
        }

        // 9th floore always treasure
        int floorIdx = FLOORS / 2;
        for (int j = 0; j < MAP_WIDTH; j++)
        {
            if (mapData[floorIdx,j].nextRooms.Length>0)
            {
                mapData[floorIdx,j].roomType = Room.RoomType.TREASURE;                
            }
        }
        
        // last floore always campfire
        floorIdx = FLOORS - 2;
        for (int j = 0; j < MAP_WIDTH; j++)
        {
            if (mapData[floorIdx,j].nextRooms.Length>0)
            {
                mapData[floorIdx,j].roomType = Room.RoomType.CAMPFIRE;                
            }
        }

        for (int i = 1; i < FLOORS-2; i++)
        {
            for (int j = 0; j < MAP_WIDTH; j++)
            {
                if (mapData[i,j].roomType == Room.RoomType.NOT_ASSIGNED && mapData[i,j].nextRooms.Length>0)
                {
                    SetRoomRandomly(mapData[i,j]);
                }
            }
        }
    }

    private void SetRoomRandomly(Room room)
    {
        bool campfireBelow4 = true;
        bool consecutiveCampfire = true;
        bool consecutiveShop = true;
        bool campfireOn13 = true;

        Room.RoomType candidate = Room.RoomType.NOT_ASSIGNED;
        while (campfireBelow4 || consecutiveCampfire || consecutiveShop || campfireOn13)
        {
            candidate = GetRandomRoomTypeByWeight();
            bool isCampfire = candidate == Room.RoomType.CAMPFIRE;
            bool hasCampfireParent = RoomHasParentType(room, Room.RoomType.CAMPFIRE);
            bool isShop = candidate == Room.RoomType.SHOP;
            bool hasShopParent = RoomHasParentType(room, Room.RoomType.SHOP);

            campfireBelow4 = isCampfire && room.row < 3;
            consecutiveCampfire = isCampfire && hasCampfireParent;
            consecutiveShop = isShop && hasShopParent;
            campfireOn13 = isCampfire && room.row == 12;            
        }
        room.roomType = candidate;
    }

    private bool RoomHasParentType(Room room, Room.RoomType roomType)
    {
        // Get Parents
        List<Room> parents = new();
        // Left Parent
        if (room.column > 0 && room.row > 0)
        {
            Room candidate = mapData[room.row - 1, room.column - 1];
            if (candidate.nextRooms.Contains(room))
            {
                parents.Add(candidate);
            }
        }
        // Below Parent
        if (room.row > 0)
        {
            Room candidate = mapData[room.row - 1, room.column];
            if (candidate.nextRooms.Contains(room))
            {
                parents.Add(candidate);
            }
        }
        // Right Parent
        if (room.column > MAP_WIDTH - 1 && room.row > 0)
        {
            Room candidate = mapData[room.row - 1, room.column + 1];
            if (candidate.nextRooms.Contains(room))
            {
                parents.Add(candidate);
            }
        }

        foreach (Room parent in parents)
        {
            if (parent.roomType == roomType) { return true; }
        }

        return false;
    }


    private Room.RoomType GetRandomRoomTypeByWeight()
    {
        var roll = Random.Shared.NextDouble() * randomRoomTypeTotalWeights;
        foreach (var type in randomRoomTypeWeights)
        {
            if (type.Value > roll) { return type.Key; }
        }

        return Room.RoomType.MONSTER;
    }


    private void SetupRandomRoomWeights()
    {
        randomRoomTypeWeights[Room.RoomType.MONSTER] = MONSTER_ROOM_WEIGHT;
        randomRoomTypeWeights[Room.RoomType.CAMPFIRE] = MONSTER_ROOM_WEIGHT + CAMPFIRE_ROOM_WEIGHT;
        randomRoomTypeWeights[Room.RoomType.SHOP] = MONSTER_ROOM_WEIGHT + CAMPFIRE_ROOM_WEIGHT + SHOP_ROOM_WEIGHT;

        randomRoomTypeTotalWeights = randomRoomTypeWeights[Room.RoomType.SHOP];
    }

    private void SetupBossRoom()
    {
        int middle = MAP_WIDTH / 2;
        Room bossRoom = mapData[FLOORS-1,middle];
        for (int j = 0; j < MAP_WIDTH; j++)
        {
            Room currentRoom = mapData[FLOORS-2,j];
            if (currentRoom.nextRooms.Length>0)
            {
                currentRoom.nextRooms.Append(bossRoom);
            }
        }

        bossRoom.roomType = Room.RoomType.BOSS;
    }

    private int SetupConnection(int i, int j)
    {
        Room nextRoom = null;
        Room currentRoom = mapData[i,j];
        while (nextRoom == null || WouldCrossExistingPath(i, j, nextRoom))
        {
            int roll = Math.Clamp(Random.Shared.Next(j-1, j+1), 0, MAP_WIDTH - 1);
            nextRoom = mapData[i+1, roll];
        }
        currentRoom.nextRooms = (Room[])currentRoom.nextRooms.Append(nextRoom).ToArray();
        return nextRoom.column;
    }

    private bool WouldCrossExistingPath(int i, int j, Room room)
    {
        Room leftNeighbour = null;
        Room rightNeighbour = null;
        if (j>0)
        {
            leftNeighbour = mapData[i,j-1];
        }
        if (j<MAP_WIDTH - 1)
        {
            rightNeighbour = mapData[i,j+1];
        }

        if (rightNeighbour != null && room.column > j)
        {
            foreach (Room nextRoom in rightNeighbour.nextRooms)
            {
                if (nextRoom.column < room.column) { return true; } 
            }
        }

        if (leftNeighbour != null && room.column < j)
        {
            foreach (Room nextRoom in leftNeighbour.nextRooms)
            {
                if (nextRoom.column > room.column) { return true; } 
            }
        }

        return false;
    }

    private int[] GetRandomStartingPoints()
    {
        List<int> yCoord = new();
        int uniquePoints = 0;

        while (uniquePoints < 2)
        {
            uniquePoints = 0;
            yCoord.Clear();

            for (int i = 0; i < PATHS; i++)
            {
                var roll = Random.Shared.Next(MAP_WIDTH - 1);
                if (!yCoord.Contains(roll))
                {
                    uniquePoints++;
                    yCoord.Add(roll);
                }
            }
        }
        
        return yCoord.ToArray();
    }


    private Room[,] GenerateInitGrid()
    {
        Room[,] result = new Room[FLOORS, MAP_WIDTH];

        for (int i = 0; i < FLOORS; i++)
        {
            for (int j = 0; j < MAP_WIDTH; j++)
            {
                Room currentRoom = new();
                Vector2 offset = 
                    new Vector2((float)Random.Shared.NextDouble(), (float)Random.Shared.NextDouble()) * PLACEMENT_RANDOMNESS;
                currentRoom.position = new Vector2(j*X_DIST, i*-Y_DIST) + offset;
                currentRoom.row = i;
                currentRoom.column = j;
                currentRoom.nextRooms = new Room[0];

                // Boss room has a non-random Y
                if (i== FLOORS - 1)
                {
                    currentRoom.position.Y = (i + 1) * -Y_DIST;
                }

                result[i,j] = currentRoom;
            }
        }

        return result;
    }

}
