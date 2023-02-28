using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject MainCamera;

    private LevelCreator levelCreator;
    private SpawnEnemies spawnEnemies;

    private int amountOfEnemies;


    private void Awake()
    {
        instance = this;
        levelCreator = GetComponent<LevelCreator>();
        spawnEnemies = GetComponent<SpawnEnemies>();
    }

    private void Start()
    {
        GameData.Level += 1;
        CreateNewLevel();
    }

    /*
     * Lägger till score
     * Håller reda på antal döda fiender
     */
    public void OnEnemyKilled()
    {
        HUD.instace.AddScore(100);
        amountOfEnemies -= 1;
        if (amountOfEnemies <= 0)
            LastRoom.instance.OnLevelFinished();
    }

    /*
     * Insansierar Level, fiender, spelare och kamera
     */
    private void CreateNewLevel()
    {
        levelCreator.GenerateLevel();
        spawnEnemies.SpawnHandler();
        amountOfEnemies = transform.GetChild(1).childCount;

        Instantiate(MainCamera);
        Instantiate(player);        
    }   
}
