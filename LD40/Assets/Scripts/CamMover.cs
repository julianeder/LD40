using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMover : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");

        transform.Translate(dx,0,dy);

	}
}
