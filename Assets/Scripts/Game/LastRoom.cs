using UnityEngine;

public class LastRoom : MonoBehaviour
{
    public static LastRoom instance;

    private LevelLoader levelLoaderScript;
    private GameObject FKey;

    private bool isLevelFinished = false;


    private void Awake()
    {
        instance = this;
        FKey = transform.parent.GetChild(1).gameObject;
    }

    private void Start()
    {
        levelLoaderScript = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
    }

    /*
     * Visar F-key
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FKey.SetActive(true);
        }
    }

    /*
     * Byter bana om F trycks
     */
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.F) && isLevelFinished)
        {
            levelLoaderScript.ChangeLevel((int)Scene.Game);
        }
    }

    /*
     * Gömmer F-key
     */
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.parent.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void OnLevelFinished()
    {
        isLevelFinished = true;
        GetComponentInChildren<SpriteRenderer>().color = new Color32(66, 101, 63, 110);
    }           
}
