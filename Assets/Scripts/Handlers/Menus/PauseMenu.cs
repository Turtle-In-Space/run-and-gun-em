using UnityEngine;


public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private LevelLoaderScript levelLoader;

    private SubmitScoreScript submitScore;


    private void Awake()
    {
        submitScore = GetComponent<SubmitScoreScript>();        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameData.PlayerHP > 0)
        {
            Pause();
        }
    }

    /*
     * Pausar och av pausar spelet
     * Sänker ljudet då pausat
     */
    public void Pause()
    {
        if (GameData.isGamePaused)
        {
            pauseMenu.SetActive(false);
            AudioManager.instance.SetVolume("MainTheme", 0.05f);
            Time.timeScale = 1f;
            GameData.isGamePaused = false;
            HUD.instace.gameObject.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(true);
            AudioManager.instance.SetVolume("MainTheme", 0.025f);
            Time.timeScale = 0f;
            GameData.isGamePaused = true;
            HUD.instace.gameObject.SetActive(false);
        }
    }

    public void ExitGame()
    {
        submitScore.SubmitScore(GameData.Score);

        Application.Quit();
        Debug.Log("Quit!");
    }

    public void LoadMainMenu()
    {
        submitScore.SubmitScore(GameData.Score);

        Time.timeScale = 1f;
        GameData.isGamePaused = false;
        levelLoader.ChangeLevel((int)Scene.MainMenu);
    }
}
