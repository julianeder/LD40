using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class News : MonoBehaviour {

    #region singelton
    public static News instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Two News");
        }
    }

    #endregion

    public List<string> RandomNews = new List<string>();

    public Text txt_news;

    public float time_till_next_line;

    public string nextNews = "";

    // Use this for initialization
    void Start () {
        time_till_next_line = Random.Range(2f, 8f);
	}
	
	// Update is called once per frame
	void Update () {
		
        if((time_till_next_line-= Time.deltaTime) < 0)
        {
            time_till_next_line = Random.Range(2f, 8f);
            if (nextNews == "")
            {
                ShowRandomNews();
            }
            else
            {
                ShowNextNews();
            }
        }


    }

    private void ShowNextNews()
    {
        txt_news.text = nextNews;
        nextNews = "";
    }

    private void ShowRandomNews()
    {
        txt_news.text = RandomNews[Random.Range(0,RandomNews.Count)];
    }
}
