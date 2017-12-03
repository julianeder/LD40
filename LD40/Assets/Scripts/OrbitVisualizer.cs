using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
//[ExecuteInEditMode]
public class OrbitVisualizer : MonoBehaviour
{


    public int stepCount;

    public Transform Sun;

    LineRenderer lineRenderer;

    private void Start()
    {
        //Sun = GameObject.FindGameObjectWithTag("Sun").transform;

        lineRenderer = GetComponent<LineRenderer>();
    }


    private void Update()
    {

        lineRenderer.positionCount = stepCount;

        float offsetAngle = Mathf.Atan2(transform.position.z - Sun.position.z, transform.position.x - Sun.position.x) * Mathf.Rad2Deg;

        for (int i = 0; i < stepCount; i++)
        {
            lineRenderer.SetPosition(i, AngleToPosition(i * (360f / (float)stepCount) + offsetAngle) * Vector3.Distance(Sun.position, transform.position));
        }


    }

    private Vector3 AngleToPosition(float angle)
    {
        Vector2 pos = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        return new Vector3(pos.x, 0, pos.y);
    }

}
