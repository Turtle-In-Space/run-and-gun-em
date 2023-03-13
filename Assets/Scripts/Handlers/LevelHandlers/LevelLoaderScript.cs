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

public class LevelLoaderScript : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime = 0.5f;


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
