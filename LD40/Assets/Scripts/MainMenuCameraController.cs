using UnityEngine;
using System.Collections;

public class MainMenuCameraController : MonoBehaviour {


    public GameObject[] positions;
    public GameObject[] canvas;
    public float smoothFactor = 2;
    public float fadeOutFactor = 2;

    public int nextPos = 0;

    void Start()
    {
        //hide all canvas except main canvas
        for(int i = 1; i < canvas.Length; i++)
        {
            canvas[i].GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, positions[nextPos].transform.position, Time.deltaTime * smoothFactor);
        transform.rotation = Quaternion.Slerp(transform.rotation, positions[nextPos].transform.rotation, Time.deltaTime * smoothFactor);

        
    }

    public void NextPosition(int index)
    {
        //fadeout current canvas
        StartCoroutine(FadeOut());

        nextPos = index;

        //show next canvas
        StartCoroutine(FadeIn());




    }

    IEnumerator FadeOut()
    {
        CanvasGroup cg = canvas[nextPos].GetComponent<CanvasGroup>();
        float time = 0;
        while (cg.alpha > 0)
        {
            cg.alpha = Mathf.Lerp(1f, 0f, time / fadeOutFactor);
            time += Time.deltaTime;
            yield return null;
        }

    }

    IEnumerator FadeIn()
    {
        CanvasGroup cg = canvas[nextPos].GetComponent<CanvasGroup>();
        float time = 0;
        while (cg.alpha < 1)
        {
            cg.alpha = Mathf.Lerp(0f, 1f, time / fadeOutFactor);
            time += Time.deltaTime;
            yield return null;
        }

    }



}
