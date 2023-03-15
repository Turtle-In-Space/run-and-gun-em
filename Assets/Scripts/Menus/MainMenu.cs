using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private LevelLoader levelLoader;


    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }

    /*
     * Startar spelet
     * sätter globala variabler
     */
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
