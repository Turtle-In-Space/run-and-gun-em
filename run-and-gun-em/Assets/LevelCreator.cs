using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{ 
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject MainCamera;
    [SerializeField] private GameObject startRoom;
    [SerializeField] private GameObject door;

    [SerializeField]private RoomData[] roomDatas;

    private void Start()
    {
        Instantiate(startRoom, transform);
        /*Instantiate(player);
        Instantiate(MainCamera);
        Instantiate(HUD);*/
    }


    public void GenerateLevel(int numberOfRooms)
    {

    }

    private void SpawnRoom(Vector2 doorPosition, int direction)
    {
    }
}










/*
           GameObject room = Instantiate(rooms[Random.Range(0, rooms.Length)], prevRoom.transform.position, Quaternion.identity, transform);//Random.Range(0, rooms.Length)]
           Transform openings = room.transform.GetChild(0);
           Transform opening = openings.GetChild(Random.Range(0, openings.childCount));//Random.Range(0, openings.childCount));

           Vector3 direction = (opening.position - room.transform.position).normalized;              
           float center2Opening = Vector2.Distance(opening.position, room.transform.position);
           float distance = center2Opening + prevCenter2Opening;
           float angle = Vector2.SignedAngle(prevDirection, direction);

           room.transform.position += prevDirection * distance;
           room.transform.rotation = Quaternion.Euler(0, 0, angle);

           prevRoom = room;
           prevOpening = openings.GetChild(Random.Range(0, openings.childCount));
           prevCenter2Opening = Vector2.Distance(prevOpening.position, prevRoom.transform.position);
           prevDirection = (prevOpening.position - prevRoom.transform.position).normalized;
           Destroy(opening.gameObject);
           */