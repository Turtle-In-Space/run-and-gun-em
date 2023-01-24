using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Manager : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }    
}
