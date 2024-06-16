using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pogo : MonoBehaviour
{
    Rigidbody2D PlayerRb;
    private void Start()
    {
        PlayerRb = GameObject.FindGameObjectWithTag(Tags.T_Player).GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.T_Pickable))
        {
            print("poggoed");
            PlayerRb.velocity = new Vector2(PlayerRb.velocity.y, 0f);
            PlayerRb.velocity = new Vector2(PlayerRb.velocity.y, 20f);
        }
    }
}
