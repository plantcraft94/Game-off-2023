using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ItemHolder : MonoBehaviour
{
    Camera cam;
    public Transform Hand;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );

        Hand.right = direction;
        float zRotation = Hand.rotation.eulerAngles.z;
        if (zRotation > 90 && zRotation < 270)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
        

    }
}
