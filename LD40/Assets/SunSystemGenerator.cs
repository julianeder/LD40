using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunSystemGenerator : MonoBehaviour {

    public int seed;
    public int planetCount = 9;
    public float probapilityHabitable = 0.5f;

    public GameObject prefab_planet;

    public List<Color> waterColor = new List<Color>();
    public List<Color> mountainColor = new List<Color>();
    public List<string> planetNames = new List<string>();

    private List<GameObject> planets = new List<GameObject>();


    // Use this for initialization
    void Start () {
        Random.InitState(seed);

        GenerateSystem();

	}

    private void GenerateSystem()
    {
        float radius = 0;
        float size = 0.5f;
        
        for (int i = 0; i < planetCount; i++)
        {
            GameObject planet = Instantiate(prefab_planet, transform);
            radius += Random.Range(3f * (i+1) * 0.3f, 15f * (i+1) * 0.3f);
            planet.transform.Translate(radius, 0, 0);
            planet.transform.RotateAround(GameObject.FindGameObjectWithTag("Sun").transform.position, Vector3.up, Random.Range(0f, 360f));


            do
            {
                size += Random.Range(-0.5f, 1f);
            } while (size <= 0.5f);

            planet.transform.localScale = Vector3.one * size;

            planet.GetComponent<Planet>().local_orbit_speed = (1 / size) * 3f;

            planet.GetComponent<Planet>().local_rotation_speed = (1 / size) * 3f;

            if(Random.value < probapilityHabitable)
            {
                planet.GetComponent<Population>().isHabitable = true;
            }
            else
            {
                planet.GetComponent<Population>().isHabitable = false;
            }

            planet.GetComponent<Population>().Population_Grew_per_sec = Random.Range(0.999f, 1.005f);

            planet.transform.Find("explored").Find("expl_mountain").GetComponent<MeshRenderer>().material.color = mountainColor[Random.Range(0,mountainColor.Count)];
            planet.transform.Find("explored").Find("expl_water").GetComponent<MeshRenderer>().material.color = waterColor[Random.Range(0, mountainColor.Count)];

            planet.GetComponent<Planet>().PlanetName = planetNames[Random.Range(0, planetNames.Count)] +"-"+ Random.Range(1,100).ToString();

            planets.Add(planet);

        }
        int index = 0;
        do
        {
            index = Random.Range(0, planetCount);
        } while (!planets[index].GetComponent<Population>().isHabitable);

        planets[index].GetComponent<Planet>().isExplored = true;
        planets[index].GetComponent<Planet>().isPopulated = true;
        planets[index].GetComponent<Population>().population = 7000;


    }


}
