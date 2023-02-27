using UnityEngine;

public class Menu_Manager : MonoBehaviour
{
    [SerializeField] private LevelLoaderScript levelLoader;


    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }

    public void LoadGameLevel()
    {
        GameData.isPlayerDead = false;
        GameData.PlayerHP = GameData.MaxPlayerHP;
        GameData.Level = 0;
        GameData.Score = 0;

        levelLoader.ChangeLevel((int)Scene.Game);
    }

    public void LoadLeaderboardScene()
    {
        levelLoader.ChangeLevel((int)Scene.Leaderboard);
    }
}
