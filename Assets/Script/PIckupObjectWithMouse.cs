using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PIckupObjectWithMouse : MonoBehaviour
{
    public GameObject MouseLocation;
    public Vector2 MousePos;
    Camera cam;
    bool pickedUp = false;
    Rigidbody2D rb;
    bool pickable = false;
    bool scaleable = false;
    public static int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        print(scaleable);
        MousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        MouseLocation.transform.position = MousePos;
        if (pickable)
        {
            if (Input.GetKeyDown(KeyCode.E) && !pickedUp)
            {
                pickedUp = true;
            }
            else if (Input.GetKeyDown(KeyCode.E) && pickedUp)
            {
                pickedUp = false;
            }

        }
        if (pickedUp)
        {
            rb.velocity = Vector2.zero;
            rb.MovePosition(MousePos);
            if (Input.mouseScrollDelta.y > 0)
            {
                if (scaleable == false)
                {
                    return;
                }
                transform.localScale += new Vector3(0.5f ,0.5f ,0.5f);
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                if (transform.localScale == new Vector3(0.5f, 0.5f, 0.5f))
                {
                    return;
                }
                transform.localScale += new Vector3(-0.5f, -0.5f, -0.5f);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Picker"))
        {
            print("Pickable");
            pickable = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Picker"))
        {
            print("Not Pickable");
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
    }

}
