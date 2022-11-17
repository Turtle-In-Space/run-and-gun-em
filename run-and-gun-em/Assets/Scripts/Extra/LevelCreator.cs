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

    private readonly Vector2[] directionList = { Vector2.right, Vector2.up, Vector2.left, Vector2.down };

    
    private void Start()
    {
        GenerateLevel(3);
        /*Instantiate(player);
        Instantiate(MainCamera);
        Instantiate(HUD);*/
    }


    public void GenerateLevel(int numberOfRooms)
    {
        Instantiate(roomData[0].roomPrefab, transform);
        DoorData door = roomData[0].doorData[0];

        for (int i = 0; i < numberOfRooms; i++)
        {
            door = SpawnRoom(Random.Range(1, roomData.Length), door);
        }
    }

    private DoorData SpawnRoom(int roomNum, DoorData pastDoor)
    {
        GameObject room = Instantiate(roomData[roomNum].roomPrefab, pastDoor.pos, Quaternion.identity, transform);
        int doorNum = Random.Range(0, roomData[roomNum].doorData.Length);
        DoorData door = roomData[roomNum].doorData[doorNum];


        int desiredDirection = (((int)pastDoor.direction) + 2) & 3;
        int directionDiffrence = (desiredDirection - (int)door.direction) & 3;
        float distance = door.pos.magnitude + pastDoor.pos.magnitude;

        room.transform.SetPositionAndRotation(-directionList[desiredDirection] * distance, Quaternion.Euler(0, 0, directionDiffrence * 90));

        DoorData[] newDoors = new DoorData[roomData[roomNum].doorData.Length];
        Debug.Log(roomData[roomNum].doorData.Length);
        for (int i = 0; i < roomData[roomNum].doorData.Length; i++)
        {
            Debug.Log(i);
            if (i != doorNum)
            {
                newDoors[i] = roomData[roomNum].doorData[i];
            }
        }

        return newDoors[Random.Range(0, roomData[roomNum].doorData.Length)];
    }
}
