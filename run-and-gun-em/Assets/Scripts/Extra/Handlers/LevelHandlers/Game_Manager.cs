using System;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject MainCamera;

    private LevelCreator levelCreator;
    private SpawnEntities spawnEntities;

    private int amountOfEnemies;


    private void Awake()
    {
        instance = this;
        levelCreator = GetComponent<LevelCreator>();
        spawnEntities = GetComponent<SpawnEntities>();
    }

    private void Start()
    {
        GameData.Level += 1;
        CreateNewLevel();
    }

    private void CreateNewLevel()
    {
        levelCreator.GenerateLevel();
        spawnEntities.SpawnHandler();
        amountOfEnemies = transform.GetChild(1).childCount;

        Instantiate(MainCamera);
        Instantiate(player);        
    }

    public void EnemyKilled()
    {
        amountOfEnemies -= 1;
        if (amountOfEnemies <= 0)
            LastRoom.instance.LevelFinished();
    }
}
