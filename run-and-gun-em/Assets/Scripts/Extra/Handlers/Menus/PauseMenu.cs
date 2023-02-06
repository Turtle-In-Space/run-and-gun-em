using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private LevelLoaderScript levelLoader;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameData.PlayerHP > 0)
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (GameData.isGamePaused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            GameData.isGamePaused = false;
        }
        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            GameData.isGamePaused = true;
        }
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
