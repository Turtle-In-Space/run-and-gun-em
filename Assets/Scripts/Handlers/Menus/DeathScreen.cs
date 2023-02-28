using UnityEngine;
using System.Collections;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    public static DeathScreen instace;

    [SerializeField] private SubmitScore submitScore;
    [SerializeField] private LevelLoaderScript levelLoader;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;

    private void Awake()
    {
        instace = this;
    }

    public void Dead()
    {
        Time.timeScale = 0f;
        GameData.isGamePaused = true;
        HUD.instace.gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);

        scoreText.text = "Score: " + GameData.Score;
        levelText.text = "Level: " + GameData.Level;

        IEnumerator coroutine = submitScore.SubmitScoreRutine(GameData.Score);
        submitScore.StartCoroutine(coroutine);

    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameData.isGamePaused = false;
        levelLoader.ChangeLevel((int)Scene.Menu);
    }
}
