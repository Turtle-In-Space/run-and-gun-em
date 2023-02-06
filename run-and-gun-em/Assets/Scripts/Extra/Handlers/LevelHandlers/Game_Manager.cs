using System;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject MainCamera;
    [SerializeField] private GameObject Crosshair;

    private LevelCreator levelCreator;
    private SpawnEntities spawnEntities;


    private void Awake()
    {
        levelCreator = GetComponent<LevelCreator>();
        spawnEntities = GetComponent<SpawnEntities>();
    }

    private void Start()
    {
        GameData.Level += 1;
        CreateNewLevel();
    }    

    public void CreateNewLevel()
    {
        levelCreator.GenerateLevel();
        spawnEntities.SpawnHandler();

        Instantiate(MainCamera);
        Instantiate(Crosshair);
        Instantiate(player);        
    }
}
