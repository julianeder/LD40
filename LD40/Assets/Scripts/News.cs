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

    float t;

    public string nextNews = "";

    // Use this for initialization
    void Start () {
        t = Random.Range(2f, 8f);
	}
	
	// Update is called once per frame
	void Update () {
		
        if((t-= Time.deltaTime) < 0)
        {
            t = Random.Range(2f, 8f);
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
