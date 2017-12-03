using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class MainMenue : MonoBehaviour {

    public string username = "";

    public InputField input_username;

    public void StartGame()
    {
        if (username != "")
        {
            SceneManager.LoadScene(1);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void UsernameUpdate()
    {
        username = input_username.text;
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
    }
}
