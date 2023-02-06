using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEntities : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    private Transform roomParent;
    private Transform enemiesParent;
    

    private void Awake()
    {
        roomParent = transform.GetChild(0);
        enemiesParent = transform.GetChild(1);
    }

    public void SpawnHandler()
    {
        for (int i = 1; i < roomParent.childCount - 1; i++)
        {
            Bounds roomBounds = roomParent.GetChild(i).GetComponent<Collider2D>().bounds;
            float largestSide = roomBounds.size.x > roomBounds.size.y ? roomBounds.size.x : roomBounds.size.y;
            int amountOfEnemies = (int)Random.Range(1, Mathf.Round(largestSide / 10));

            for (int j = 0; j < amountOfEnemies; j++)
            {
                SpawnEnemy(roomBounds);
            }
        }
    }

    private void SpawnEnemy(Bounds roomBounds)
    {             
        float randPosX = Random.Range(roomBounds.min.x, roomBounds.max.x);
        float randPosY = Random.Range(roomBounds.min.y, roomBounds.max.y);
        Vector3 randPos = new Vector3(randPosX, randPosY, 0);

        if(IsValidPosition(randPos))
        {
            Instantiate(enemy, randPos, Quaternion.identity, enemiesParent);
        }
        else
        {
            SpawnEnemy(roomBounds);
        }
    }

    private bool IsValidPosition(Vector3 randPos)
    {
        bool inRoom = false;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(randPos, new Vector2(3, 2), 0);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Wall"))
            {
                return false;
            }
            else if (colliders[i].CompareTag("Room"))
            {
                inRoom = true;
            }
        }
        return inRoom;
    }
}