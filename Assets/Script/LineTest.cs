using System.Collections.Generic;
using UnityEngine;

public class LineTest : MonoBehaviour
{
    public GameObject rootObject;
    public GameObject targetObject;
    public LineRenderer lineRenderer;

    void Start()
    {
        // Initialize the LineRenderer component
        if (lineRenderer == null)
        {
            lineRenderer.positionCount = 2; // Two positions for the root and target objects
        }

    }
    private void Update()
    {
        DrawLine();
    }
    void DrawLine()
    {
        // Set the positions of the LineRenderer
        lineRenderer.SetPosition(0, rootObject.transform.position); // Root object position
        lineRenderer.SetPosition(1, targetObject.transform.position); // Target object position
    }
}
