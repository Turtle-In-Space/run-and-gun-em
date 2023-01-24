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
    private readonly int doorLayerMask = 1 << 13;
    private List<DoorData> possibleExit = new List<DoorData>();


    private void Start()
    {
        GenerateLevel();
        Instantiate(HUD);
        Instantiate(player, Vector3.zero, Quaternion.identity);
        Instantiate(MainCamera);
    }

    //Skapar första rummet och sköter skapandet av fler
    private void GenerateLevel()
    {
        int numberOfRooms = Random.Range(6, 9);

        Instantiate(roomData[0].roomPrefab, transform);
        possibleExit.Add(roomData[0].doorData[0]);

        for (int i = 0; i < numberOfRooms; i++)
        {
            DoorData exitDoor = possibleExit[Random.Range(0, possibleExit.Count)];
            SpawnRoom(Random.Range (1, roomData.Length), exitDoor);
        }

        SpawnRoom(0, possibleExit[Random.Range(0, possibleExit.Count)]);
    }

    private void SpawnRoom(int roomID, DoorData pastDoor)
    {
        RoomData currentRoom = roomData[roomID];
        int connectedDoorID = Random.Range(0, currentRoom.doorData.Length);
        DoorData connectedDoor = currentRoom.doorData[connectedDoorID];

        //Räknar ut plats och rotation
        int newRot = (2 - (int)connectedDoor.direction + prevRoomRotation + (int)pastDoor.direction) % 4;
        Vector2 pastDoorPos = pastDoor.pos.Rotate(90 * prevRoomRotation);
        Vector2 connectedDoorPos = connectedDoor.pos.Rotate(90 * newRot);
        Vector2 roomPos = prevRoomPos + (Vector3)pastDoorPos - (Vector3)connectedDoorPos;
        Quaternion roomRot = Quaternion.Euler(0, 0, 90 * newRot);

        GameObject room = Instantiate(currentRoom.roomPrefab, roomPos, roomRot, transform);

        if (IsColliderObstructed(room.GetComponent<Collider2D>(), roomLayerMask).Count != 0)
        {
            Destroy(room);
            return;
        }        

        //Sätter ut spel dörrarna och lägger in möjliga utgångar
        possibleExit.Clear();
        for (int i = 0; i < roomData[roomID].doorData.Length; i++)
        {
            Transform parent = room.transform.GetChild(0).GetChild(i);
            Quaternion rotation = Quaternion.Euler(0, 0, 90 * (newRot + (1 + (int)roomData[roomID].doorData[i].direction) % 2));
            GameObject Door = Instantiate(door, parent.position, rotation, parent);

            /*Sätt ut dörrar
             * om ingång -> explodera
             * finns annan dörr -> Ta bort          
             */

            // Ingång
            if (i == connectedDoorID)
            {
                Door.GetComponent<DoorHandler>().canExplode = true;
                List<Collider2D> collider = IsColliderObstructed(Door.GetComponent<BoxCollider2D>(), doorLayerMask);

                if(collider.Count != 0)
                {
                    Destroy(collider[0].gameObject);
                }                
            }
            else
            {
                possibleExit.Add(roomData[roomID].doorData[i]);
            }
        }

        prevRoomPos = room.transform.position;
        prevRoomRotation = newRot;
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