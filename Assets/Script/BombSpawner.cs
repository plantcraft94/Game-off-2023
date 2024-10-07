using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public static float SquareBombCount = 0;
    public static float CircleBombCount = 0;
    public GameObject SquareBomb;
    public GameObject CircleBomb;
    public GameObject placeholderSquareBomb;
    public GameObject placeholderCircleBomb;
    Player Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        if (Player.isSquareBomb == false && Player.isCircleBomb == false)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Player.useSquareBomb == true && SquareBombCount < 1)
            {
                SquareBombCount += 1;
                Instantiate(SquareBomb, transform.position, Quaternion.identity);
                Player.useSquareBomb = false;
            }
            if (Player.useCircleBomb == true && CircleBombCount < 1)
            {
                CircleBombCount += 1;
                Instantiate(CircleBomb, transform.position, Quaternion.identity);
                Player.useCircleBomb = false;
            }

        }
    }
    public void Flip()
    {
        if (Player.flipped == true)
        {
            transform.rotation = Quaternion.Euler(0, 150, 0);
        }
        else if (Player.flipped == false)
        {
            transform.rotation = Quaternion.Euler(0, 30, 0);
        }
    }
}
