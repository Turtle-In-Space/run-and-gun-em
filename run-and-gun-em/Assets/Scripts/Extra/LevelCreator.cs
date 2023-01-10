using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelCreator : MonoBehaviour
{ 
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject MainCamera;
    [SerializeField] private GameObject door;
    [SerializeField] private RoomData[] roomData;

    private int prevRoomRotation = 0;
    private Vector3 prevRoomPos = Vector3.zero;
    private readonly int roomLayerMask = 1 << 11;
    private List<DoorData> newDoors = new List<DoorData>();


    private void Start()
    {
        GenerateLevel();
        Instantiate(HUD);
        Instantiate(player, Vector3.zero, Quaternion.identity);
        Instantiate(MainCamera);
    }

    private void GenerateLevel()
    {
        int numberOfRooms = Random.Range(6, 9);

        Instantiate(roomData[0].roomPrefab, transform);
        newDoors.Add(roomData[0].doorData[0]);

        for (int i = 0; i < numberOfRooms; i++)
        {
            DoorData newDoor = newDoors[Random.Range(0, newDoors.Count)];
            SpawnRoom(Random.Range (1, roomData.Length), newDoor);
        }

        SpawnRoom(0, newDoors[Random.Range(0, newDoors.Count)]);

    }    

    private void SpawnRoom(int roomID, DoorData pastDoor)
    {
        RoomData currentRoom = roomData[roomID];
        int newDoorID = Random.Range(0, currentRoom.doorData.Length);
        DoorData connectedDoor = currentRoom.doorData[newDoorID];

        int newRot = (2 - (int)connectedDoor.direction + prevRoomRotation + (int)pastDoor.direction) % 4;
        Vector2 prevDoorPos = pastDoor.pos.Rotate(90 * prevRoomRotation);
        Vector2 newDoorPos = connectedDoor.pos.Rotate(90 * newRot);
        Vector2 roomPos = prevRoomPos + (Vector3)prevDoorPos - (Vector3)newDoorPos;
        Quaternion roomRot = Quaternion.Euler(0, 0, 90 * newRot);

        GameObject room = Instantiate(currentRoom.roomPrefab, roomPos, roomRot, transform);

        if (IsRoomObstructed(room.GetComponent<Collider2D>()))
        {
            Destroy(room);
            return;
        }
        
        prevRoomPos = room.transform.position;
        prevRoomRotation = newRot;
       
        AddDoors(roomID, newDoorID);
    }

    private bool IsRoomObstructed(Collider2D newestRoom)
    {
        ContactFilter2D filter = new ContactFilter2D();
        List<Collider2D> results = new List<Collider2D>();
        filter.SetLayerMask(roomLayerMask);

        newestRoom.OverlapCollider(filter, results);
        if (results.Count != 0)
        {            
            return true;
        }

        return false;
    }

    private void AddDoors(int roomID, int newDoorID)
    {
        newDoors.Clear();
        for (int i = 0; i < roomData[roomID].doorData.Length; i++)
        {
            if (i != newDoorID)
            {
                newDoors.Add(roomData[roomID].doorData[i]);
            }
        }
    }
}