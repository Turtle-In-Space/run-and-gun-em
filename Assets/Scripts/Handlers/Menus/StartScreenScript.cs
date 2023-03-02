using UnityEngine;
using TMPro;

public class StartScreenScript : MonoBehaviour
{
    [SerializeField] private LevelLoaderScript levelLoader;
    [SerializeField] private TMP_InputField inputField;

    private UserHandler userHandler;
    private GameObject button;
    private GameObject errorMessage;
    private string playerName;


    private void Awake()
    {
        button = transform.GetChild(1).gameObject;        
        errorMessage = transform.GetChild(2).gameObject;
        userHandler = GetComponent<UserHandler>();
    }

    public void OnNameEntered()
    {
        playerName = inputField.text;

        if (playerName.Length == 0)
        {
            errorMessage.GetComponent<TextMeshProUGUI>().text = "Error: Name too short, must be longer than 0";
            errorMessage.SetActive(true);
            button.SetActive(false);
        }
        else if (playerName.Length > 15)
        {
            errorMessage.GetComponent<TextMeshProUGUI>().text = "Error: Name too long, must be shorter than 15";
            errorMessage.SetActive(true);
            button.SetActive(false);
        }
        else
        {
            errorMessage.SetActive(false);            
            button.SetActive(true);
        }
    }   

    public void LoadMainMenu()
    {
        userHandler.SetPlayerName(playerName);
        levelLoader.ChangeLevel((int)Scene.MainMenu);
    }
}
