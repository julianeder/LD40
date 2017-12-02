using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    public Transform Sun;
    public float local_rotation_speed = 1f;

    public static float global_rotation_speed = 10f;



	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        transform.RotateAround(Sun.position, Vector3.up, Time.deltaTime * local_rotation_speed * global_rotation_speed);

        




	}
}
