using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastRoom : MonoBehaviour
{
    public static LastRoom instance;

    private LevelLoaderScript levelLoaderScript;
    private bool isLevelFinished = false;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        levelLoaderScript = GameObject.Find("LevelLoader").GetComponent<LevelLoaderScript>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.F) && isLevelFinished)
        {
            levelLoaderScript.ChangeLevel((int)Scene.Game);
        }
    }

    public void LevelFinished()
    {
        isLevelFinished = true;      
        GetComponentInChildren<SpriteRenderer>().color = new Color32(66, 101, 63, 110);
    }
}
