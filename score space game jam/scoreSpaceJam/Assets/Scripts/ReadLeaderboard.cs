using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadLeaderboard : MonoBehaviour
{
    //Objects
    public TextMeshProUGUI name1, name2, name3, name4, name5, name6, name7, name8, name9, name10;
    public TextMeshProUGUI score1, score2, score3, score4, score5, score6, score7, score8, score9, score10;
    public leaderboard leaderboard;

    private void Update()
    { 
        //Update Texts
        name1.text = leaderboard.names[0];
        name2.text = leaderboard.names[1];
        name3.text = leaderboard.names[2];
        name4.text = leaderboard.names[3];
        name5.text = leaderboard.names[4];
        name6.text = leaderboard.names[5];
        name7.text = leaderboard.names[6];
        name8.text = leaderboard.names[7];
        name9.text = leaderboard.names[8];
        name10.text = leaderboard.names[9];

        score1.text = leaderboard.scores[0].ToString("D10");
        score2.text = leaderboard.scores[1].ToString("D10");
        score3.text = leaderboard.scores[2].ToString("D10");
        score4.text = leaderboard.scores[3].ToString("D10");
        score5.text = leaderboard.scores[4].ToString("D10");
        score6.text = leaderboard.scores[5].ToString("D10");
        score7.text = leaderboard.scores[6].ToString("D10");
        score8.text = leaderboard.scores[7].ToString("D10");
        score9.text = leaderboard.scores[8].ToString("D10");
        score10.text = leaderboard.scores[9].ToString("D10");
    }
}
