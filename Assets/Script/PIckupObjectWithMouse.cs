using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


public class PIckupObjectWithMouse : MonoBehaviour
{
    public GameObject MouseLocation;
    public Vector3 MousePos;
    Camera cam;
    public bool pickedUp = false;
    Rigidbody2D rb;
    public bool pickable = false;
    bool scaleable = true;
    public static int count = 0;
    bool moveable = true;
    float currentgravityscale;
    public float speed = 20f;
    Vector2 MoveDirection;
    LineRenderer lineRenderer;
    Transform targetObject;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        targetObject = GameObject.Find("Magnet").transform;
    }

    // Update is called once per frame
    void Update()
    {
        DrawLine();
        print(pickedUp);
        MousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        MouseLocation.transform.position = MousePos;
        Vector3 direction = (MousePos - transform.position).normalized;
        MoveDirection = direction;
        if (pickable)
        {
            if (Input.GetKeyDown(KeyCode.E) && !pickedUp)
            {
                currentgravityscale = rb.gravityScale;
                pickedUp = true;
                lineRenderer.enabled = true;
            }
            else if (Input.GetKeyDown(KeyCode.E) && pickedUp)
            {
                pickedUp = false;
                rb.freezeRotation = false;
                rb.gravityScale = currentgravityscale;
                lineRenderer.enabled = false;
            }

        }
        if (pickedUp)
        {
            rb.freezeRotation = true;
            rb.gravityScale = 0;
            if (moveable)
            {
                rb.velocity = Vector2.zero;
                rb.velocity = new Vector2(MoveDirection.x, MoveDirection.y) * speed;
            }
            if (Input.mouseScrollDelta.y > 0)
            {
                if (scaleable == false)
                {
                    return;
                }
                transform.localScale += new Vector3(0.5f ,0.5f ,0.5f);
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                if (transform.localScale == new Vector3(0.5f, 0.5f, 0.5f))
                {
                    return;
                }
                transform.localScale += new Vector3(-0.5f, -0.5f, -0.5f);
            }
            if (Input.GetMouseButtonDown(0))
            {
                rb.gravityScale += 1;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                rb.gravityScale -= 1;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Feet"))
        {
            moveable = false;
        }
        if (collision.gameObject.CompareTag("Body"))
        {
            if (pickedUp)
            {
                collision.transform.parent.gameObject.GetComponent<Rigidbody2D>().mass = rb.mass * 1000;
            }
        }
        if (collision.gameObject.CompareTag("Picker"))
        {
            pickable = true;
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Feet"))
        {
            moveable = false;
        }
        if (collision.gameObject.CompareTag("Body"))
        {
            collision.transform.parent.gameObject.GetComponent<Rigidbody2D>().mass = rb.mass * 1000;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Feet"))
        {
            moveable = true;
        }
        if (collision.gameObject.CompareTag("Body"))
        {
            collision.transform.parent.gameObject.GetComponent<Rigidbody2D>().mass = 1f;
        }
        if (collision.gameObject.CompareTag("Picker"))
        {
            pickable = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            count++;
            if (count == 2)
            {
                scaleable = false;
            }    
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            count--;
            if (count == 0)
            {
                scaleable = true;
            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            moveable = true;
        }
    }
    void DrawLine()
    {
        // Set the positions of the LineRenderer
        lineRenderer.SetPosition(0, transform.position); // Root object position
        lineRenderer.SetPosition(1, targetObject.transform.position); // Target object position
    }
}
