using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    public Transform Sun;
    public float local_rotation_speed = 1f;

    public static float global_rotation_speed = 5f;



	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        transform.RotateAround(Sun.position, Vector3.up, Time.deltaTime * local_rotation_speed * global_rotation_speed);


	}


    void OnMouseDown()
    {
        gc.instance.PlanetClicked(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        gc.instance.PlanetCollided(gameObject,other.gameObject);

    }

    public void Explore()
    {
        throw new NotImplementedException();
    }
}
