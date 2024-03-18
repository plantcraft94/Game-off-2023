using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLocation : MonoBehaviour
{
    public Vector2 MousePos;
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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Pickable"))
        {
            collision.GetComponent<Runeable>().isRuneable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pickable"))
        {
            collision.GetComponent<Runeable>().isRuneable = false;
        }
    }
}
