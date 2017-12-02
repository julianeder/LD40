using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMover : MonoBehaviour {

    public float zoom_fact;
    Camera cam;

	// Use this for initialization
	void Start () {
        cam = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {

        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");

        float scr = Input.GetAxis("Mouse ScrollWheel");

        transform.Translate(dx,0,dy);
        cam.fieldOfView += scr * zoom_fact;

        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, 10, 150);

	}
}
