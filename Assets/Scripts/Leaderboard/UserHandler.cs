using UnityEngine;
using System.Collections;
using LootLocker.Requests;
using TMPro;

public class UserHandler : MonoBehaviour
{
    [SerializeField] private GameObject errorMessage;


    private void Start()
    {
        IEnumerator coroutine = LoginRoutine();
        StartCoroutine(coroutine);     
    }

    /*
     * Sätter spelare namn i leaderboard
     */
    public void SetPlayerName(string name)
    {
        LootLockerSDKManager.SetPlayerName(name, (response) =>
        {
            if (!response.success)
            {
                Debug.Log(response.Error);
            }
            else
                Debug.Log("Name set");
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
                errorMessage.SetActive(true);
                Debug.Log("Start session failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }    
}
