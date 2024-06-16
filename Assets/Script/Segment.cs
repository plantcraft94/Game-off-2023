using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Segment : MonoBehaviour
{
    Color cyan = new Color(0, 152, 255, 255);
    Color Grey = new Color(71, 71, 71, 255);
    Image img;
    [HideInInspector]
    public bool isHovered = false;

    public string AbilityName;
    private void Start()
    {
        img = GetComponent<Image>();
        img.color = Grey;
    }
    private void Update()
    {
        if (isHovered)
        {
            img.color = cyan;
        }
        else if (!isHovered)
        {
            img.color = Color.gray;
        }
    }
}
