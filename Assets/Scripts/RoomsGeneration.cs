using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomPrefabMapping
{
    public RoomType roomType;
    public GameObject prefab;
}

public class RoomsGeneration : MonoBehaviour
{

    public RoomListEventChannel roomList;
    public GameObject roomPrefab;
    public List<RoomPrefabMapping> roomPrefabs;



    // Start is called before the first frame update
    void Start()
    {
        roomList.OnEventRaised += GenerateRooms;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void GenerateRooms(List<BasicRoom> rooms)
    {
        foreach (BasicRoom room in rooms)
        {
            Vector3 roomPosition = new Vector3(room.y * 10, 0, room.x * 10);

            // If you have a room prefab, instantiate it at the room's position:
            GameObject roomTile = roomPrefabs.Find(mapping => mapping.roomType == room.type).prefab;
            GameObject roomObject = Instantiate(roomPrefab, roomPosition, Quaternion.identity);
            roomObject.GetComponent<Room>()._width = room.width;
            roomObject.GetComponent<Room>()._height = room.height;
            roomObject.GetComponent<Room>()._tile = roomTile;


            // If you don't have a room prefab, create an empty GameObject for each room:
            // GameObject roomObject = new GameObject("Room");
            // roomObject.transform.position = roomPosition;

            // Set the room object's parent to this script's GameObject for better organization in the scene hierarchy:
            //roomObject.transform.SetParent(transform);
        }
    }
}
