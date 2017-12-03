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

    public AudioClip ExplorationSoundEffect;
    public AudioClip ColonisationSoundEffect;


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

        if (planet.CompareTag("Sun") && other.CompareTag("Ship") && other.GetComponent<Ship>().destination == planet)
        {
            Population.ships.Remove(other.GetComponent<Ship>()); //kill all the people
            Destroy(other); // Destroy the Ship
            News.instance.nextNews = "1.000.000 people ppainfully died by Sunburn";
            News.instance.time_till_next_line = 0.1f;

        }
        else {

            if (other.CompareTag("Ship") && other.GetComponent<Ship>().destination == planet)
            {
                if (other.GetComponent<Ship>().shipType == Ship.ShipType.exploration)
                {

                    planet.GetComponent<Planet>().Explore();
                    News.instance.nextNews = "Discovered Planet " + planet.GetComponent<Planet>().PlanetName;
                    News.instance.time_till_next_line = 0.1f;

                    GetComponent<AudioSource>().clip = ExplorationSoundEffect;
                    GetComponent<AudioSource>().Play();


                }
                else if (other.GetComponent<Ship>().shipType == Ship.ShipType.colony)
                {
                    planet.GetComponent<Planet>().isPopulated = true;
                    planet.GetComponent<Population>().population += other.GetComponent<Ship>().population;

                    GetComponent<AudioSource>().clip = ColonisationSoundEffect;
                    GetComponent<AudioSource>().Play();

                    Population.ships.Remove(other.GetComponent<Ship>());

                    News.instance.nextNews = "Colonized Planet " + planet.GetComponent<Planet>().PlanetName;
                    News.instance.time_till_next_line = 0.1f;

                }
                else if (other.GetComponent<Ship>().shipType == Ship.ShipType.transport)
                {
                    planet.GetComponent<Population>().population += other.GetComponent<Ship>().population;
                    Population.ships.Remove(other.GetComponent<Ship>());

                }



                Destroy(other);
            }
        }

    }

    public void PlanetClicked(GameObject planet)
    {
        selectedComand.PlanetClicked(planet);
    }

    public void SunClicked(GameObject sun)
    {
        selectedComand.SunClicked(sun);
    }

    public void Lose(GameObject planet)
    {
        GameObject lb = Instantiate(prefabLeaderboard);
        lb.GetComponent<Leaderboard>().score = (int)Population.GetGlobalPopulation();
        SceneManager.LoadScene(2);
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
    public virtual void SunClicked(GameObject planet) { }

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
            if (planet.GetComponent<Planet>().isPopulated && planet.GetComponent<Population>().population > 2000)
            {
                startPlanet = planet;
                comandStatus = 1;
            }
            else if (planet.GetComponent<Planet>().isPopulated && planet.GetComponent<Population>().population < 2000)
            {
                //Debug.LogError("min. 2000 population Required");
                News.instance.nextNews = "min. 2 Mrd. Populaton is required to send a Colonisation Ship";
                News.instance.time_till_next_line = 0.1f;
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
                if (!planet.GetComponent<Planet>().isExplored)
                {
                    News.instance.nextNews = "Can not colonize unexplored Planet";
                    News.instance.time_till_next_line = 0.1f;
                }
                else
                {
                    News.instance.nextNews = "Can not colonize unhabitable Planet";
                    News.instance.time_till_next_line = 0.1f;
                }


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
        Population.ships.Add(ship.GetComponent<Ship>());

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
                //Debug.LogError("min. 2000 Populaton is required");
                News.instance.nextNews = "min. 2 Mrd. Populaton is required to send a Transport Ship";
                News.instance.time_till_next_line = 0.1f;
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
            //Debug.LogError("not colonized planet");
            News.instance.nextNews = "Not colonized planet";
            News.instance.time_till_next_line = 0.1f;

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
        Population.ships.Add(ship.GetComponent<Ship>());


    }

    public override void SunClicked(GameObject sun)
    {

        if (comandStatus == 0)
        {

        }
        else if (comandStatus == 1)
        {
            News.instance.nextNews = "Killing People by Sending them into the Sun is Dangerous";
            News.instance.time_till_next_line = 0.1f;


            destinationPlanet = sun;
            SendShip();
            comandStatus = 0;

        }



    }

}
