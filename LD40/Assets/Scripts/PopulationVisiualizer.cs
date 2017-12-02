
using System;
using UnityEngine;
using UnityEngine.UI;


public class PopulationVisiualizer : MonoBehaviour {

    public Population population;
    public Transform mainCam;


    public Text pop_text;
    public RectTransform panel_pop_crit;

    public RectTransform img_green;
    public RectTransform img_red;

    public RectTransform img_act;


    // Use this for initialization
    void Start () {

        img_green.sizeDelta = new Vector2((population.pop_crit / population.pop_max) * panel_pop_crit.rect.width, img_green.sizeDelta.y);

        img_red.sizeDelta = new Vector2((1-(population.pop_crit / population.pop_max)) * panel_pop_crit.rect.width, img_red.sizeDelta.y);

    }

    // Update is called once per frame
    void Update () {

        transform.LookAt(mainCam);
        transform.Rotate(Vector3.up, 180f);

        if (population.population < 1000)
        {
            pop_text.text = Convert.ToInt32(population.population).ToString() + " Mio.";
        }
        else
        {
            pop_text.text = (population.population / 1000).ToString("F1") + " Mrd.";

        }
        img_act.anchoredPosition = new Vector2((population.population / population.pop_max) * panel_pop_crit.rect.width, img_act.anchoredPosition.y);


    }
}
