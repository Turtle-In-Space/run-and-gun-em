using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene
{
    StartScreen,
    MainMenu,
    Game,
    Leaderboard
}

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;
    private readonly float transitionTime = 1f;


    public void ChangeLevel(int levelIndex)
    {
        StartCoroutine(LoadLevel(levelIndex));
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
