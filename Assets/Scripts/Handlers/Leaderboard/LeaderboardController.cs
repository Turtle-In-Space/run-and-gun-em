using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class LeaderboardController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI names;
    [SerializeField] private TextMeshProUGUI scores;
    [SerializeField] private TextMeshProUGUI errorMessage;
    [SerializeField] private LevelLoaderScript levelLoader;

    private readonly string leaderboardKey = "globalScore";

    private void Start()
    {
        StartCoroutine("GetTopHighscoreRoutine");
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameData.isGamePaused = false;
        levelLoader.ChangeLevel((int)Scene.Menu);
    }

    private IEnumerator GetTopHighscoreRoutine()
    {
        bool done = false;

        LootLockerSDKManager.GetScoreList(leaderboardKey, 10, 0, (response)=>
        {
            if (response.success)
            {
                LootLockerLeaderboardMember[] members = response.items;

                string tempPlayerName = "";
                string tempPlayerScore = "";

                //Sätter Rank. Name Score "X. NAMEMCNAMEFACE   XXXX"
                for (int i = 0; i < members.Length; i++)
                {
                    tempPlayerName += members[i].rank + ". ";

                    if(members[i].player.name != "")
                    {
                        tempPlayerName += members[i].player.name;
                    }
                    else
                    {
                        tempPlayerName += members[i].player.public_uid;
                    }

                    tempPlayerScore += members[i].score + "\n";
                    tempPlayerName += "\n";
                }
                names.text = tempPlayerName;
                scores.text = tempPlayerScore;

                done = true;
            }
            else
            {
                errorMessage.text = "Error: " + response.statusCode;
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
