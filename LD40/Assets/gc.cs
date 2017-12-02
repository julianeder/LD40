using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gc : MonoBehaviour {

    #region singelton
    public static gc instance;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Two Gcs");
        }
    }

    #endregion


    public Comand selectedComand;


    public GameObject prefabExplorationShip;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E");
            selectedComand = new sendExplorationShipComand();
        }


	}

    public void PlanetCollided(GameObject planet, GameObject other)
    {

        if (other.CompareTag("Ship") && other.GetComponent<Ship>().destination == planet)
        {
            if (other.GetComponent<Ship>().shipType == Ship.ShipType.exploration) {

                planet.GetComponent<Planet>().Explore();

            }
            Destroy(other);

        }

    }

    public void PlanetClicked(GameObject planet)
    {
        selectedComand.PlanetClicked(planet);
    }


    public GameObject InstantiateShip(GameObject ship,Vector3 pos,Quaternion rot)
    {
        return Instantiate(ship, pos, rot);
    }


}


[System.Serializable]
public class Comand
{
    public int comandStatus = 0;

    public virtual void PlanetClicked(GameObject planet) { }

}

[System.Serializable]
public class sendExplorationShipComand : Comand
{
    GameObject startPlanet;
    GameObject destinationPlanet;

    GameObject ship;

    public sendExplorationShipComand()
    {

    }

    

    public override void PlanetClicked(GameObject planet)
    {

        if (comandStatus == 0)
        {
            startPlanet = planet;
            comandStatus = 1;
        }
        else if (comandStatus == 1 && planet == startPlanet)
        {
            startPlanet = planet;
            comandStatus = 1;
        }
        else if(comandStatus == 1 && planet != startPlanet)
        {
            
            destinationPlanet = planet;
            SendShip();
            comandStatus = 0;
        }
        


    }

    private void SendShip()
    {
        ship =  gc.instance.InstantiateShip(gc.instance.prefabExplorationShip, startPlanet.transform.position, startPlanet.transform.rotation);
        ship.GetComponent<Ship>().destination = destinationPlanet;
        ship.GetComponent<Ship>().shipType = Ship.ShipType.exploration;

    }
}
