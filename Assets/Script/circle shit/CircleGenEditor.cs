using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CircleGenEditor : EditorWindow
{
    private float radius = 5f;
    private int numberOfPoints = 36;
    private Vector3 center = Vector3.zero;
    private GameObject pointPrefab;
    private Transform parentTransform;
    private Vector3[] points;

    [MenuItem("Tools/Circle Points Generator")]
    public static void ShowWindow()
    {
        GetWindow<CircleGenEditor>("Circle Points Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Generate Circle Points", EditorStyles.boldLabel);

        radius = EditorGUILayout.FloatField("Radius", radius);
        numberOfPoints = EditorGUILayout.IntField("Number of Points", numberOfPoints);
        center = EditorGUILayout.Vector3Field("Center", center);
        pointPrefab = (GameObject)EditorGUILayout.ObjectField("Point Prefab", pointPrefab, typeof(GameObject), false);
        parentTransform = (Transform)EditorGUILayout.ObjectField("Parent Transform", parentTransform, typeof(Transform), true);

        if (GUILayout.Button("Generate Points"))
        {
            GeneratePoints();
        }

        if (points != null)
        {
            GUILayout.Label("Generated Points:");
            foreach (var point in points)
            {
                GUILayout.Label(point.ToString());
            }
        }
    }

    private void GeneratePoints()
    {
        ClearPreviousPoints();

        points = new Vector3[numberOfPoints];
        float angleStep = 360.0f / numberOfPoints;
        for (int i = 0; i < numberOfPoints; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad + Mathf.PI / 2;
            float x = center.x + radius * Mathf.Cos(angle);
            float y = center.y + radius * Mathf.Sin(angle);
            points[i] = new Vector3(x, y, center.z);

            CreatePoint(points[i]);
        }

        SceneView.RepaintAll();
    }

    private void CreatePoint(Vector3 position)
    {
        GameObject newPoint;
        if (pointPrefab != null)
        {
            newPoint = (GameObject)PrefabUtility.InstantiatePrefab(pointPrefab);
        }
        else
        {
            newPoint = new GameObject("Point");
        }

        newPoint.transform.position = position;

        // Calculate the angle between the point and the center
        Vector3 direction = center - position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        newPoint.transform.rotation = Quaternion.Euler(0, 0, angle);

        if (parentTransform != null)
        {
            newPoint.transform.SetParent(parentTransform);
        }
    }

    private void ClearPreviousPoints()
    {
        if (parentTransform != null)
        {
            foreach (Transform child in parentTransform)
            {
                DestroyImmediate(child.gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (points == null) return;

        Gizmos.color = Color.red;
        foreach (var point in points)
        {
            Gizmos.DrawSphere(point, 0.1f);
        }
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        if (points == null) return;

        Handles.color = Color.red;
        foreach (var point in points)
        {
            Handles.SphereHandleCap(0, point, Quaternion.identity, 0.1f, EventType.Repaint);
        }
    }

    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }
}
