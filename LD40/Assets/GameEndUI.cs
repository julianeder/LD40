using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndUI : MonoBehaviour {

    public Text txt_rank;
    public Text txt_usernames;
    public Text txt_scores;

    public Text txt_rank_you;
    public Text txt_usernames_you;
    public Text txt_scores_you;

    private Leaderboard leaderboard;
    bool repeat = false;

    void Start()
    {
        leaderboard = GameObject.Find("Leaderboard(Clone)").GetComponent<Leaderboard>();
        leaderboard.AddNewHighscore();

    }

    void Update()
    {
        if (leaderboard.finishedDownload && !repeat)
        {
            SetScores(leaderboard.highscoresList);
            repeat = true;

        }
    }

    public void SetScores(Highscore[] highscoresList)
    {
        int rank = 1;
        foreach (Highscore hs in highscoresList)
        {
            txt_rank.text += rank.ToString() + "\n";
            txt_usernames.text += hs.username + "\n";
            txt_scores.text += hs.score + "\n";

            if(hs.username == leaderboard.username)
            {
                txt_rank_you.text += rank.ToString() + "\n";
                txt_usernames_you.text += hs.username + "\n";
                txt_scores_you.text += hs.score + "\n";
            }

            rank++;
            
        }
    }
}
