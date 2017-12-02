using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    public GameObject destination;
    public float speed;

    public ShipType shipType;

    public int population;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(destination != null)
        {
            transform.LookAt(destination.transform, Vector3.up);
            transform.Translate(transform.forward * Time.deltaTime * speed,Space.World);
        }

	}

    public enum ShipType
    {
        exploration,
        colony,
        transport
    }
}
