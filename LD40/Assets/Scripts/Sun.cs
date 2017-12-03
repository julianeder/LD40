using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {

    void OnMouseDown()
    {
        try
        {
            gc.instance.SunClicked(gameObject);
        }
        catch (System.Exception ex) { }

    }

    void OnTriggerEnter(Collider other)
    {
        gc.instance.PlanetCollided(gameObject, other.gameObject);
        GetComponent<AudioSource>().Play();

    }
}
