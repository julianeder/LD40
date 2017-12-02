using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour {


    #region singelton
    public static GameCanvas instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Two Game Canvases");
        }
    }

    #endregion


    [Space(10)]
    public GameObject panelPlanetstats;
    public Text TextPlanetName;
    public Text TextPlanetstatsPopulation;
    public Text TextPlanetstatsHabitable;
    public Text TextPlanetstatsGrowth;

    [Space(30)]
    public Text TextGlobalPopulation;
    public Text TextTime;

    private float t_sec;
    private float t_min;

    // Use this for initialization
    void Start () {
        HidePlanetStats();
	}
	
	// Update is called once per frame
	void Update () {
        TextGlobalPopulation.text =  Population.GetPopulationString(Population.GetGlobalPopulation()).ToString();
        t_sec += Time.deltaTime;
        if (t_sec > 60)
        {
            t_sec = 0;
            t_min++;
        }

        TextTime.text = t_min.ToString("00") + ":" + t_sec.ToString("00");
    }



    public void ShowPlanetStats(string planetName,string population,string isHabitable,string growth)
    {
        panelPlanetstats.SetActive(true);

        TextPlanetName.text = planetName;
        TextPlanetstatsPopulation.text = "Population: " + population;

        TextPlanetstatsHabitable.text = "Is habitable: "+ isHabitable;
        TextPlanetstatsGrowth.text = "Population Growth: " + growth;
    }
    public void HidePlanetStats()
    {
        panelPlanetstats.SetActive(false);

    }
}
