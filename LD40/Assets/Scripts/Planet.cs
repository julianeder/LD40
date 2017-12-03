using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    public string PlanetName = "<name>";

    public Transform Sun;
    public float local_rotation_speed = 1f;
    public float local_orbit_speed = 1f;


    public static float global_orbit_speed = 1f;


    public bool isExplored = false;
    public bool isPopulated = false;



    public GameObject unexploredGeonometry;
    public GameObject exploredGeonometry;



    // Use this for initialization
    void Start () {

        Sun = GameObject.FindGameObjectWithTag("Sun").transform;


        if (isExplored)
        {
            exploredGeonometry.SetActive(true);
            unexploredGeonometry.SetActive(false);
        }
        else
        {
            exploredGeonometry.SetActive(false);
            unexploredGeonometry.SetActive(true);
        }

        if(GetComponent<Population>().population > 0)
        {
            isPopulated = true;
        }


	}
	
	// Update is called once per frame
	void Update () {

        transform.RotateAround(Sun.position, Vector3.up, Time.deltaTime * local_rotation_speed * global_orbit_speed);
        transform.Rotate(transform.up, Time.deltaTime * local_rotation_speed);

        try {
            if (isExplored)
            {
                GetComponentInChildren<PopulationVisiualizer>().isActive = true;
            }
            else
            {
                GetComponentInChildren<PopulationVisiualizer>().isActive = false;
            }
        }catch(System.Exception ex) { }

    }


    void OnMouseDown()
    {
        try { 
            gc.instance.PlanetClicked(gameObject);
        }
        catch (System.Exception ex) { }

    }

    void OnMouseEnter()
    {
        try { 
            Population pop = GetComponent<Population>();
            if (isExplored)
            {
                GameCanvas.instance.ShowPlanetStats(PlanetName, Population.GetPopulationString(pop.population), GetComponent<Population>().isHabitable.ToString(), pop.Population_Grew_per_sec.ToString("F3"));
            }
            else
            {
                GameCanvas.instance.ShowPlanetStats("unknown Planet", "-", "-", "-");

            }
        }catch(System.Exception ex) { }

    }

    void OnMouseExit()
    {
        try {
            GameCanvas.instance.HidePlanetStats();
        }catch(System.Exception ex) { }

    }

    void OnTriggerEnter(Collider other)
    {
        gc.instance.PlanetCollided(gameObject,other.gameObject);

    }

    public void Explore()
    {
        isExplored = true;
        exploredGeonometry.SetActive(true);
        unexploredGeonometry.SetActive(false);
    }




}
