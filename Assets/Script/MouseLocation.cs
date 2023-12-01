using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLocation : MonoBehaviour
{
    public Vector3 MousePos;
    Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        MousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = MousePos;
    }
}
