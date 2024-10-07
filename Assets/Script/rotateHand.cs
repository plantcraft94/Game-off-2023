using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateHand : MonoBehaviour
{
    Camera cam;
    GameObject MouseLocation;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        MouseLocation = GameObject.Find("MouseLocation");
    }
                            
    // Update is called once per frame
    void Update()
    {
        Vector2 direction = new Vector2(
            MouseLocation.transform.position.x - transform.position.x,
            MouseLocation.transform.position.y - transform.position.y
        );

        transform.right = direction;
    }
}
