using UnityEngine;
using System.Collections;
using LootLocker.Requests;
using TMPro;

public class UserHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;


    private void Start()
    {
        StartCoroutine("LoginRoutine");     
    }

    /*
     * Sätter spelare namn i online leaderboard
     */
    public void SetPlayerName()
    {
        LootLockerSDKManager.SetPlayerName(inputField.text, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Player name set success");
            }
            else
            {
                Debug.Log("Player name set failed" + response.Error);
            }
        });
    }

    /*
     * Loggar in spelaren till leaderboard
     */
    private IEnumerator LoginRoutine()
    {
        bool done = false;

        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                GameData.PlayerID = response.player_id.ToString();                
                done = true;
            }
            else
            {
                Debug.Log("Start session failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }    
}
