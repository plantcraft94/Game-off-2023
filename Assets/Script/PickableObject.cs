using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    // Variables to hold the information of the first and second walls
    private GameObject firstWall, secondWall;

    // Variable to control scaling
    private bool isScaling = true;

    // The scaling factor
    public float scaleSpeed = 0.01f;

    private void Update()
    {
        if (isScaling)
        {
            // Scale the object every frame
            transform.localScale += new Vector3(scaleSpeed, scaleSpeed, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            if (firstWall == null)
            {
                // Assign the first wall
                firstWall = collision.gameObject;
            }
            else if (secondWall == null)
            {
                // Assign the second wall and check if it's opposite to the first wall
                secondWall = collision.gameObject;
                Vector2 toFirstWall = (Vector2)firstWall.transform.position - (Vector2)transform.position;
                Vector2 toSecondWall = (Vector2)secondWall.transform.position - (Vector2)transform.position;
                print(toFirstWall);
                print(toSecondWall);
                print(Vector2.Dot(new Vector2(0, toFirstWall.y), new Vector2(0, toSecondWall.y)));
                print(Vector2.Dot(new Vector2(toFirstWall.x, 0), new Vector2(toSecondWall.x, 0)));
                if (toFirstWall.x == toSecondWall.x || toFirstWall.y == toSecondWall.y)
                {
                    if (Vector2.Dot(new Vector2(0,toFirstWall.y),new Vector2(0,toSecondWall.y)) < 0|| Vector2.Dot(new Vector2(toFirstWall.x, 0), new Vector2(toSecondWall.x, 0)) < 0)
                    {
                        // If the dot product is negative, the walls are opposite
                        isScaling = false;
                    }
                }
                else
                {
                    print("not opposite");
                }
            }
            else
            {
                // Reset the wall references for the new scaling process
                firstWall = collision.gameObject;
                secondWall = null;
                isScaling = false;
            }
        }
    }
}
