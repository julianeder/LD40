﻿
using System;
using UnityEngine;
using UnityEngine.UI;


public class PopulationVisiualizer : MonoBehaviour {

    public Population population;
    public Transform mainCam;

    public bool isActive = true;

    public Text pop_text;
    public RectTransform panel_pop_crit;

    public RectTransform img_green;
    public RectTransform img_red;

    public RectTransform img_act;


    // Use this for initialization
    void Start () {

        mainCam = Camera.main.transform;

        img_green.sizeDelta = new Vector2((population.pop_crit / population.pop_max) * panel_pop_crit.rect.width, img_green.sizeDelta.y);

        img_red.sizeDelta = new Vector2((1-(population.pop_crit / population.pop_max)) * panel_pop_crit.rect.width, img_red.sizeDelta.y);

    }

    // Update is called once per frame
    void Update () {

        if (isActive)
        {

            transform.LookAt(mainCam);
            transform.Rotate(Vector3.up, 180f);

            if (population.isHabitable)
            {
                panel_pop_crit.gameObject.SetActive(true);

                pop_text.text = Population.GetPopulationString(population.population);
                img_act.anchoredPosition = new Vector2((population.population / population.pop_max) * panel_pop_crit.rect.width, img_act.anchoredPosition.y);
            }
            else
            {
                panel_pop_crit.gameObject.SetActive(false);
                pop_text.text = "not Habitable";

            }
        }
        else
        {
            panel_pop_crit.gameObject.SetActive(false);
            pop_text.text = "";
        }

    }
}
