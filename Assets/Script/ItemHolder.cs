using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ItemHolder : MonoBehaviour
{
    public enum Flip
    {
        x,
        y
    }
    public Flip flip;
    public Transform Hand;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        print(Flip.x);
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float zRotation = Hand.rotation.eulerAngles.z;
        if (zRotation > 90 && zRotation < 270)
        {
            if(flip == Flip.x)
                sr.flipX = true;
            else if (flip == Flip.y)
                sr.flipY = true;
        }
        else
        {
            if (flip == Flip.x)
                sr.flipX = false;
            else if (flip == Flip.y)
                sr.flipY = false;
        }
        

    }
}
