using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject prefabColonisationShip;
    public GameObject prefabTransportShip;

    public GameObject prefabLeaderboard;


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

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("C");
            selectedComand = new sendColonisationShipComand();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("T");
            selectedComand = new sendTransportationShipComand();
        }


    }

    public void btn_Click(int id)
    {
        if (id == 0)
        {
            Debug.Log("E");
            selectedComand = new sendExplorationShipComand();
        }

        if (id == 1)
        {
            Debug.Log("C");
            selectedComand = new sendColonisationShipComand();
        }

        if (id == 2)
        {
            Debug.Log("T");
            selectedComand = new sendTransportationShipComand();
        }
    }

    public void PlanetCollided(GameObject planet, GameObject other)
    {

        if (other.CompareTag("Ship") && other.GetComponent<Ship>().destination == planet)
        {
            if (other.GetComponent<Ship>().shipType == Ship.ShipType.exploration) {

                planet.GetComponent<Planet>().Explore();
                News.instance.nextNews = "Discovered Planet " + planet.GetComponent<Planet>().PlanetName;

            }
            else if (other.GetComponent<Ship>().shipType == Ship.ShipType.colony)
            {
                planet.GetComponent<Planet>().isPopulated = true;
                planet.GetComponent<Population>().population += other.GetComponent<Ship>().population;
                News.instance.nextNews = "Colonized Planet " + planet.GetComponent<Planet>().PlanetName;

            }
            else if (other.GetComponent<Ship>().shipType == Ship.ShipType.transport)
            {
                planet.GetComponent<Population>().population += other.GetComponent<Ship>().population;
            }



            Destroy(other);

        }

    }

    public void PlanetClicked(GameObject planet)
    {
        selectedComand.PlanetClicked(planet);
    }

    public void Lose(GameObject planet)
    {
        GameObject lb = Instantiate(prefabLeaderboard);
        lb.GetComponent<Leaderboard>().score = (int)Population.GetGlobalPopulation();
        SceneManager.LoadScene(1);
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
            if (planet.GetComponent<Planet>().isExplored)
            {
                startPlanet = planet;
                comandStatus = 1;
            }
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


[System.Serializable]
public class sendColonisationShipComand : Comand
{
    GameObject startPlanet;
    GameObject destinationPlanet;

    GameObject ship;

    public sendColonisationShipComand()
    {

    }



    public override void PlanetClicked(GameObject planet)
    {

        if (comandStatus == 0)
        {
            if (planet.GetComponent<Planet>().isPopulated && planet.GetComponent<Population>().population > 1000)
            {
                startPlanet = planet;
                comandStatus = 1;
            }
            else if (planet.GetComponent<Planet>().isPopulated && planet.GetComponent<Population>().population < 1000)
            {
                Debug.LogError("min. 1000 population Required");
            }
        }
        else if (comandStatus == 1 && planet == startPlanet)
        {
            startPlanet = planet;
            comandStatus = 1;
        }
        else if (comandStatus == 1 && planet != startPlanet )
        {
            if (planet.GetComponent<Planet>().isExplored && planet.GetComponent<Population>().isHabitable)
            {
                destinationPlanet = planet;
                SendShip();
                comandStatus = 0;
            }
            else
            {
                comandStatus = 1;
                Debug.LogError("Unhabitable or Unexplored Planet");


            }
        }



    }

    private void SendShip()
    {
        ship = gc.instance.InstantiateShip(gc.instance.prefabColonisationShip, startPlanet.transform.position, startPlanet.transform.rotation);
        ship.GetComponent<Ship>().destination = destinationPlanet;
        ship.GetComponent<Ship>().shipType = Ship.ShipType.colony;
        ship.GetComponent<Ship>().population = 10;
        startPlanet.GetComponent<Population>().population -= 10;

    }
}


[System.Serializable]
public class sendTransportationShipComand : Comand
{
    GameObject startPlanet;
    GameObject destinationPlanet;

    GameObject ship;

    public sendTransportationShipComand()
    {

    }



    public override void PlanetClicked(GameObject planet)
    {

        if (comandStatus == 0)
        {
            if (planet.GetComponent<Planet>().isPopulated && planet.GetComponent<Population>().population > 2000)
            {
                startPlanet = planet;
                comandStatus = 1;
            }
            else
            {
                Debug.LogError("min. 2000 Populaton is required");
            }
        }
        else if (comandStatus == 1 && planet == startPlanet)
        {
            startPlanet = planet;
            comandStatus = 1;
        }
        else if (comandStatus == 1 && planet != startPlanet && planet.GetComponent<Planet>().isPopulated == false)
        {            
            comandStatus = 0;
            Debug.LogError("not colonized planet");
        }
        else if (comandStatus == 1 && planet != startPlanet && planet.GetComponent<Planet>().isPopulated == true)
        {

            destinationPlanet = planet;
            SendShip();
            comandStatus = 0;

        }



    }

    private void SendShip()
    {
        ship = gc.instance.InstantiateShip(gc.instance.prefabTransportShip, startPlanet.transform.position, startPlanet.transform.rotation);
        ship.GetComponent<Ship>().destination = destinationPlanet;
        ship.GetComponent<Ship>().shipType = Ship.ShipType.transport;
        ship.GetComponent<Ship>().population = 1000;
        startPlanet.GetComponent<Population>().population -= 1000;

    }
}
