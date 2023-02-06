using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastRoom : MonoBehaviour
{
    private LevelLoaderScript levelLoaderScript;

    private void Start()
    {
        levelLoaderScript = GameObject.Find("LevelLoader").GetComponent<LevelLoaderScript>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.F))
        {
            levelLoaderScript.ChangeLevel((int)Scene.Game);
        }
    }
}
