using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        submitScore.StartScoreRutine(GameData.Score);
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
