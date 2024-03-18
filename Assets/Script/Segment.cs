using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    Color cyan = new Color(0, 152, 255, 255);
    Color Grey = new Color(71, 71, 71, 255);
    SpriteRenderer sr;
    bool isHovered = false;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = Grey;
    }
    private void Update()
    {
        if (isHovered)
        {
            sr.color = cyan;
        }
        else if (!isHovered)
        {
            sr.color = Color.gray;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if the gameobject tagged "Picker" is in the trigger area
        if (collision.CompareTag("Picker"))
        {
            isHovered = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Picker"))
        {
            isHovered = false;
        }
    }
}
