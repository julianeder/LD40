using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour {

    string privateCode = "3q8EHqToi0ebYBZYnc8dqQSc_yB11O-UWigW-zgcPhnA";
    string publicCode = "5a23137b6b2b651624a9e8a9";
    string webURL = "http://dreamlo.com/lb/";

    public int score = 0;
    public string username = "julian";

    public bool finishedDownload = false;

    public Highscore[] highscoresList;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);


       // DownloadHighscores();
    }

    public void AddNewHighscore()
    {
        StartCoroutine(UploadNewHighscore(username, score));
    }

    IEnumerator UploadNewHighscore(string username, int score)
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
            print("Upload Successful");
        else {
            print("Error uploading: " + www.error);
        }

        DownloadHighscores();

    }

    public void DownloadHighscores()
    {
        StartCoroutine("DownloadHighscoresFromDatabase");
    }

    IEnumerator DownloadHighscoresFromDatabase()
    {

        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
            FormatHighscores(www.text);
        else {
            print("Error Downloading: " + www.error);
        }


    }

    void FormatHighscores(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoresList = new Highscore[entries.Length];

        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            highscoresList[i] = new Highscore(username, score);
            print(highscoresList[i].username + ": " + highscoresList[i].score);

        }

        finishedDownload = true;
    }

}

[System.Serializable]
public struct Highscore
{
    public string username;
    public int score;

    public Highscore(string _username, int _score)
    {
        username = _username;
        score = _score;
    }

}
