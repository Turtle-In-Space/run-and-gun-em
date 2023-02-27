using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{     
    [SerializeField] private RoomData[] roomData;
    [SerializeField] private GameObject doorPrefab;
    [SerializeField] private GameObject lastRoomArea;

    private List<DoorData> possibleExit = new List<DoorData>();
    private Transform roomParent;    
    private Vector3 prevRoomPosition = Vector3.zero;

    private readonly int roomLayerMask = 1 << 11;
    private readonly int doorLayerMask = 1 << 13;
    private int prevRoomRotation = 0;


    private void Awake()
    {
        roomParent = transform.GetChild(0);
    }

    //Skapar första rummet och sköter skapandet av fler
    public void GenerateLevel()
    {
        int numberOfRooms = Random.Range(3, 5);

        Instantiate(roomData[0].roomPrefab, roomParent);
        possibleExit.Add(roomData[0].doorData[0]);

        for (int i = 0; i < numberOfRooms; i++)
        {
            DoorData exitDoor = possibleExit[Random.Range(0, possibleExit.Count)];
            SpawnRoom(Random.Range (1, roomData.Length), exitDoor);
        }

        SpawnRoom(0, possibleExit[Random.Range(0, possibleExit.Count)]);
    }

    private void SpawnRoom(int roomID, DoorData entryDoor)
    {
        RoomData currentRoom = roomData[roomID];
        int connectDoorID = Random.Range(0, currentRoom.doorData.Length);
        DoorData connectDoor = currentRoom.doorData[connectDoorID];

        //Räknar ut plats och rotation
        int newRotation = (2 - (int)connectDoor.direction + prevRoomRotation + (int)entryDoor.direction) % 4;
        Vector2 pastDoorPos = entryDoor.pos.Rotate(90 * prevRoomRotation);
        Vector2 connectedDoorPos = connectDoor.pos.Rotate(90 * newRotation);
        Vector2 roomPos = prevRoomPosition + (Vector3)pastDoorPos - (Vector3)connectedDoorPos;
        Quaternion roomRot = Quaternion.Euler(0, 0, 90 * newRotation);

        GameObject room = Instantiate(currentRoom.roomPrefab, roomPos, roomRot, roomParent);
        List<Collider2D> roomCollider = IsColliderObstructed(room.GetComponent<Collider2D>(), roomLayerMask);

        if (roomCollider.Count != 0)
        {
            //Sista rummet ryms inte, skapa en ny bana
            if (roomID == 0)
            {
                for (int i = 0; i < roomParent.childCount; i++)
                {
                    Destroy(roomParent.GetChild(i).gameObject);
                }
                GenerateLevel();
            }

            Destroy(room);
            return;
        }        

        //TODO gör dett atill egen function
        //Sätter ut speldörrarna och lägger in möjliga utgångar
        possibleExit.Clear();
        for (int i = 0; i < roomData[roomID].doorData.Length; i++)
        {
            Transform parent = room.transform.GetChild(0).GetChild(i);
            Quaternion rotation = Quaternion.Euler(0, 0, 90 * (newRotation + (1 + (int)roomData[roomID].doorData[i].direction) % 2));
            GameObject Door = Instantiate(doorPrefab, parent.position, Quaternion.identity, parent);
            Door.transform.GetChild(0).rotation = rotation;

            // Ingång --> kan exlpodera och ta bort om en annan dörr är ivägen
            if (i == connectDoorID)
            {
                Door.GetComponentInChildren<DoorHandler>().canExplode = true;
                List<Collider2D> doorCollider = IsColliderObstructed(Door.GetComponentInChildren<BoxCollider2D>(), doorLayerMask);
                if (doorCollider.Count != 0)
                {
                    Destroy(doorCollider[0].gameObject);
                }                
            }
            else
            {
                possibleExit.Add(roomData[roomID].doorData[i]);
            }
        }

        if (roomID == 0)
        {
            Instantiate(lastRoomArea, roomPos, Quaternion.identity);
        }

        prevRoomPosition = roomPos;
        prevRoomRotation = newRotation;
    }

    private List<Collider2D> IsColliderObstructed(Collider2D collider, LayerMask layerMask)
    {
        ContactFilter2D filter = new ContactFilter2D();
        List<Collider2D> results = new List<Collider2D>();
        filter.SetLayerMask(layerMask);

        collider.OverlapCollider(filter, results);
        return results;
    }
}