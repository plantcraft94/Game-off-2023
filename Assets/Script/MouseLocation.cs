using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UIElements;

public class MouseLocation : MonoBehaviour
{
    public Vector2 MousePos;
    Camera cam;
    Rigidbody2D rb;
    public Vector2 PreSkillPos;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }
    private void Update()
    {
        MousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        rb.MovePosition(MousePos);
        GetMouseLocation();
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
    void GetMouseLocation()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PreSkillPos = Input.mousePosition;

        }
    }
    public void WarpMouse()
    {
        Mouse.current.WarpCursorPosition(PreSkillPos);
    }
}
