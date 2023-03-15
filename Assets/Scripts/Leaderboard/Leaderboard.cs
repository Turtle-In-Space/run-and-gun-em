using System.Collections;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI names;
    [SerializeField] private TextMeshProUGUI scores;
    [SerializeField] private TextMeshProUGUI errorMessage;
    [SerializeField] private LevelLoader levelLoader;

    private readonly string leaderboardKey = "globalScore";


    private void Start()
    {
        StartCoroutine("GetTopHighscoreRoutine");
    }

    /*
     * Byter till Menu scen
     */
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameData.isGamePaused = false;
        levelLoader.ChangeLevel((int)Scene.MainMenu);
    }

    /*
     * Tar 10 högsta scores från online lista
     * formaterar till "[RANK]. NAMEMCNAMEFACE    [SCORE]
     * Skriver ut scores
     */
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

                foreach (LootLockerLeaderboardMember member in members)
                {
                    tempPlayerName += member.rank + ". ";

                    if (member.player.name != "")
                    {
                        tempPlayerName += member.player.name;
                    }
                    else
                    {
                        tempPlayerName += member.player.public_uid;
                    }

                    tempPlayerScore += member.score + "\n";
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
