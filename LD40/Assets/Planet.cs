using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    public Transform Sun;
    public float local_rotation_speed = 1f;

    public static float global_rotation_speed = 10f;


    private LineRenderer lr;
    public float lr_res = 0.2f;
    public int lr_max_points = 300;
    private Vector3 last_lr_point;

	// Use this for initialization
	void Start () {
        lr = GetComponentInChildren<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        transform.RotateAround(Sun.position, Vector3.up, Time.deltaTime * local_rotation_speed * global_rotation_speed);

        
        if((transform.position - last_lr_point).magnitude > lr_res)
        {
            last_lr_point = transform.position;
            if (lr.positionCount < lr_max_points)
            {
                lr.positionCount = lr.positionCount + 1;
                lr.SetPosition(lr.positionCount - 1, transform.position);

            }
        }



	}
}
