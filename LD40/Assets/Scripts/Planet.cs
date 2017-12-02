using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

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

	}


    void OnMouseDown()
    {
        gc.instance.PlanetClicked(gameObject);
    }

    void OnMouseEnter()
    {
        GameCanvas.instance.ShowPlanetStats(GetComponent<Population>().GetPopulationString() , true,GetComponent<Population>().Population_Grew_per_sec);
    }

    void OnMouseExit()
    {
        GameCanvas.instance.HidePlanetStats();
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
