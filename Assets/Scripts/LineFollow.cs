using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFollow : MonoBehaviour
{
    //public Vector3[] parent;
    //LineRenderer lr;
    //private void Start()
    //{
    //    lr = this.gameObject.GetComponent<LineRenderer>();
    //}

    //private void Update()
    //{
    //    lr.SetPositions(parent);
    //}

    public Transform Ball;
    private LineRenderer lineRenderer;
    private int pointCount = 0; // How many points are there in the line?

    void Start()
    {
        // Get the LineRenderer component
        lineRenderer = GetComponent<LineRenderer>();

        // Initialize the LineRenderer
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, Ball.position);
    }

    void Update()
    {
        // Check if the sprite has moved
        if (lineRenderer.GetPosition(pointCount) != Ball.position)
        {
            // Increase the number of points in the LineRenderer
            pointCount++;
            lineRenderer.positionCount = pointCount + 1;

            // Set the new position of the line
            lineRenderer.SetPosition(pointCount, Ball.position);
        }
    }

}
