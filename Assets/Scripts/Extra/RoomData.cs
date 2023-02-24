using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorDirection
{
    right,
    top,
    left,
    bottom
}

[System.Serializable]
public class DoorData
{
    public Vector2 pos;
    public DoorDirection direction;
}

[System.Serializable]
public class RoomData
{
    [SerializeField] public DoorData[] doorData;
    public GameObject roomPrefab;    
}
