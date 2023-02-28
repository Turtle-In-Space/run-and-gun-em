using System.Collections;
using UnityEngine;
using LootLocker.Requests;

public class SubmitScore : MonoBehaviour
{
    private readonly string leaderboardKey = "globalScore";


    /*
     * Skickar spelar score till online leaderboard
     */
    public IEnumerator SubmitScoreRutine(int score)
    {
        bool done = false;

        LootLockerSDKManager.SubmitScore(GameData.PlayerID, score, leaderboardKey, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Score submit success");
                done = true;
            }
            else
            {
                Debug.Log("Score submit failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }    
}
