using UnityEngine;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    public static DeathScreen instace;

    [SerializeField] private LevelLoaderScript levelLoader;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;

    private SubmitScoreScript submitScore;

    private void Awake()
    {
        instace = this;
        submitScore = GetComponent<SubmitScoreScript>();
    }

    /*
     * Pausar spel och visar "DeathScreen" meny
     */
    public void OnPlayerDead()
    {
        Time.timeScale = 0f;
        GameData.isGamePaused = true;
        HUD.instace.gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);

        scoreText.text = "Score: " + GameData.Score;
        levelText.text = "Level: " + GameData.Level;

        submitScore.SubmitScore(GameData.Score);

    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        GameData.isGamePaused = false;
        levelLoader.ChangeLevel((int)Scene.MainMenu);
    }
}
