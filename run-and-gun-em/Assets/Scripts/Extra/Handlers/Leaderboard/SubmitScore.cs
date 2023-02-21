using System.Collections;
using UnityEngine;
using LootLocker.Requests;

public class SubmitScore : MonoBehaviour
{
    private readonly string leaderboardKey = "globalScore";
    private IEnumerator corutine;

    public void StartScoreRutine(int score)
    {
        corutine = SubmitScoreRutine(score);
        StartCoroutine(corutine);
    }

    private IEnumerator SubmitScoreRutine(int score)
    {
        bool done = false;
        string playerID = GameData.PlayerID;

        LootLockerSDKManager.SubmitScore(playerID, score, leaderboardKey, (response) =>
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
