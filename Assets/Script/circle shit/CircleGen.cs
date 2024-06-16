using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGen : MonoBehaviour
{
    public float radius = 1f;
    public int numberOfPoints = 10;

    public Vector3[] GeneratePoints()
    {
        Vector3[] points = new Vector3[numberOfPoints];
        float angleStep = 360f / numberOfPoints;

        for (int i = 0; i < numberOfPoints; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            points[i] = new Vector3(x, y, 0);
        }

        return points;
    }

    private void OnDrawGizmos()
    {
        Vector3[] points = GeneratePoints();
        Gizmos.color = Color.red;

        foreach (Vector3 point in points)
        {
            Gizmos.DrawSphere(transform.position + point, 0.1f);
        }
    }
}
