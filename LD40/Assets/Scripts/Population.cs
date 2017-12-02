using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour {

    public static List<Population> instances = new List<Population>();

    public bool isHabitable = false;

    public float population = 7000;

    public float Population_Grew_per_sec = 1f;

    private float t;

    public float pop_crit = 8000;
    public float pop_max = 15000;

    private bool overcrit = false;

    // Use this for initialization
    void Start () {
        instances.Add(this);
	}
	
	// Update is called once per frame
	void Update () {

        if (isHabitable)
        {
            if ((t -= Time.deltaTime) < 0)
            {
                t = 1;
                population = population * Population_Grew_per_sec;
            }


            if (population >= pop_max)
            {
                gc.instance.Lose(gameObject);
            }

            if(population >= pop_crit && !overcrit)
            {
                overcrit = true;
                News.instance.nextNews = "Critical Population on " + GetComponent<Planet>().PlanetName;
            }
            if(population < pop_crit)
            {
                overcrit = false;
            }

        }


	}

    public static string GetPopulationString(float pop)
    {
        if (pop <= 0)
        {
            return "none";
        }
        else if (pop < 1000)
        {
            return Convert.ToInt32(pop).ToString() + " Mio.";

        }
        else
        {
            return (pop / 1000).ToString("F1") + " Mrd.";

        }
    }

    public static float GetGlobalPopulation()
    {
        float p = 0;
        foreach (Population item in instances)
        {
            p += item.population;
        }
        return (p);
    }
}
