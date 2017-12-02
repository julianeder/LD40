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


    public GameObject panelPlanetstats;
    public Text TextPlanetName;
    public Text TextPlanetstatsPopulation;
    public Text TextPlanetstatsHabitable;
    public Text TextPlanetstatsGrowth;


    // Use this for initialization
    void Start () {
        HidePlanetStats();
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    public void ShowPlanetStats(string population,bool isHabitable,float growth)
    {
        panelPlanetstats.SetActive(true);

        TextPlanetName.text = "Planet";
        TextPlanetstatsPopulation.text = "Population: " + population;

        TextPlanetstatsHabitable.text = "Is habitable: "+ isHabitable.ToString();
        TextPlanetstatsGrowth.text = "Population Growth: " + growth.ToString();
    }
    public void HidePlanetStats()
    {
        panelPlanetstats.SetActive(false);

    }
}
