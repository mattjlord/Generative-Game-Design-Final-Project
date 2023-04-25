using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRoom
{
    public RoomType type;
    public int x, y, width, height;

    public BasicRoom(RoomType type, int x, int y, int width, int height)
    {
        this.type = type;
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
    }

    public override string ToString()
    {
        return type.ToString() + " x: " + x.ToString() + " y: " + y.ToString() + " width: " + width.ToString() + " height: " + height.ToString();
    }
}



public enum RoomType
{
    LivingRoom,
    Library,
    Bedroom,
    Kitchen,
    Entry,
    DiningRoom,
    Bathroom
}



public class GrammarBasedRoomGenerator : MonoBehaviour
{
    public int maxRooms = 10;
    public RoomListEventChannel roomList;

    private Dictionary<RoomType, List<RoomType>> grammarRules = new Dictionary<RoomType, List<RoomType>>
    {
        { RoomType.Entry, new List<RoomType> { RoomType.Library, RoomType.LivingRoom } },
        { RoomType.LivingRoom, new List<RoomType> { RoomType.Library, RoomType.Bedroom, RoomType.Kitchen, RoomType.DiningRoom, RoomType.Bathroom } },
        { RoomType.Library, new List<RoomType> { RoomType.LivingRoom, RoomType.Bedroom, RoomType.Kitchen, RoomType.DiningRoom, RoomType.Bathroom } },
        { RoomType.Bedroom, new List<RoomType> { RoomType.LivingRoom, RoomType.Library, RoomType.Kitchen, RoomType.DiningRoom, RoomType.Bathroom } },
        { RoomType.Kitchen, new List<RoomType> { RoomType.LivingRoom, RoomType.Library, RoomType.Bedroom, RoomType.DiningRoom, RoomType.Bathroom } },
        { RoomType.DiningRoom, new List<RoomType> { RoomType.LivingRoom, RoomType.Library, RoomType.Bedroom, RoomType.Kitchen, RoomType.Bathroom } },
        { RoomType.Bathroom, new List<RoomType> { RoomType.LivingRoom, RoomType.Library, RoomType.Bedroom, RoomType.Kitchen, RoomType.DiningRoom } }
    };


    private List<BasicRoom> rooms = new List<BasicRoom>();

    void Start()
    {
        GenerateRooms();
        foreach (BasicRoom room in rooms)
        {
            Debug.Log(room.ToString());
        }
    }

    void GenerateRooms()
    {
        // Generate the first room as Entry
        RoomType firstRoomType = RoomType.Entry;
        int width = Random.Range(1, 5);
        int height = Random.Range(1, 5);
        int x = 0;
        int y = 0;
        BasicRoom firstRoom = new BasicRoom(firstRoomType, x, y, width, height);
        rooms.Add(firstRoom);

        int attempts;
        const int maxAttempts = 1000;

        for (int i = 1; i < maxRooms;)
        {
            attempts = 0;
            bool roomAdded = false;

            while (!roomAdded && attempts < maxAttempts)
            {
                attempts++;

                BasicRoom previousRoom = rooms[i - 1];
                RoomType newRoomType = GetRandomNeighborRoomType(previousRoom.type);

                int newWidth = Random.Range(1, 5);
                int newHeight = Random.Range(1, 5);
                int newX, newY;

                // Generate the new room on a random side of the previous room
                int side = Random.Range(0, 4); // 0: top, 1: right, 2: bottom, 3: left
                switch (side)
                {
                    case 0: // top
                        newX = previousRoom.x;
                        newY = previousRoom.y + previousRoom.height;
                        break;
                    case 1: // right
                        newX = previousRoom.x + previousRoom.width;
                        newY = previousRoom.y;
                        break;
                    case 2: // bottom
                        newX = previousRoom.x;
                        newY = previousRoom.y - newHeight;
                        break;
                    case 3: // left
                        newX = previousRoom.x - newWidth;
                        newY = previousRoom.y;
                        break;
                    default:
                        newX = 0;
                        newY = 0;
                        break;
                }

                BasicRoom newRoom = new BasicRoom(newRoomType, newX, newY, newWidth, newHeight);

                if (!DoesRoomOverlap(newRoom))
                {

                    rooms.Add(newRoom);
                    
                    roomAdded = true;

                    i++;
                }
            }
        }
        roomList.RaiseEvent(rooms);

    }

        RoomType GetRandomNeighborRoomType(RoomType currentRoomType)
        {
            List<RoomType> neighborTypes = grammarRules[currentRoomType];
            int randomIndex = Random.Range(0, neighborTypes.Count);
            return neighborTypes[randomIndex];
        }

        bool DoesRoomOverlap(BasicRoom room)
        {
            foreach (var otherRoom in rooms)
            {
                if (room.x < otherRoom.x + otherRoom.width &&
                    room.x + room.width > otherRoom.x &&
                    room.y < otherRoom.y + otherRoom.height &&
                    room.y + room.height > otherRoom.y)
                {
                    return true;
                }
            }
            return false;
        }
    }

