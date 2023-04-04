using UnityEngine;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    public static DeathScreen instance;

    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;

    private ScoreSubmission submitScore;

    private void Awake()
    {
        instance = this;
        submitScore = GetComponent<ScoreSubmission>();
    }

    /*
     * Pausar spel och visar "DeathScreen" meny
     */
    public void OnPlayerDead()
    {
        Time.timeScale = 0f;
        GameData.isGamePaused = true;
        HUD.instance.gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);

        scoreText.text = "Score: " + GameData.Score;
        levelText.text = "Level: " + GameData.Level;

        submitScore.SubmitScore(GameData.Score);
    }

    /*
     * Ställer om game info
     * Startar Game scene
     */
    public void Retry()
    {
        GameData.isPlayerDead = false;
        GameData.PlayerHP = GameData.MaxPlayerHP;
        GameData.Level = 0;
        GameData.Score = 0;

        Time.timeScale = 1f;
        GameData.isGamePaused = false;
        levelLoader.ChangeLevel((int)Scene.Game);
    }

    /*
     * Går till main menu
     */
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        GameData.isGamePaused = false;
        levelLoader.ChangeLevel((int)Scene.MainMenu);
    }
}
