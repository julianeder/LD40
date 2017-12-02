using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
//[ExecuteInEditMode]
public class OrbitVisualizer : MonoBehaviour
{


    public int stepCount;

    Transform sun;

    LineRenderer lineRenderer;

    private void Start()
    {
        sun = GameObject.FindGameObjectWithTag("Sun").transform;

        lineRenderer = GetComponent<LineRenderer>();
    }


    private void Update()
    {

        lineRenderer.positionCount = stepCount;

        float offsetAngle = Mathf.Atan2(transform.position.z - sun.position.z, transform.position.x - sun.position.x) * Mathf.Rad2Deg;

        for (int i = 0; i < stepCount; i++)
        {
            lineRenderer.SetPosition(i, AngleToPosition(i * (360f / (float)stepCount) + offsetAngle) * Vector3.Distance(sun.position, transform.position));
        }


    }

    private Vector3 AngleToPosition(float angle)
    {
        Vector2 pos = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        return new Vector3(pos.x, 0, pos.y);
    }

}
