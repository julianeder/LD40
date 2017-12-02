using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour {

    public float population = 7000;

    public float Population_Grew_per_sec = 1f;

    private float t;

    public float pop_crit = 8000;
    public float pop_max = 15000;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if((t-= Time.deltaTime) < 0)
        {
            t = 1;
            population = population * Population_Grew_per_sec;
        }



	}

    public string GetPopulationString()
    {
        if (population <= 0)
        {
            return "none";
        }
        else if (population < 1000)
        {
            return Convert.ToInt32(population).ToString() + " Mio.";

        }
        else
        {
            return (population / 1000).ToString("F1") + " Mrd.";

        }
    }
}
