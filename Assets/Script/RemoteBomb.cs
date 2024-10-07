using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteBomb : MonoBehaviour
{
    GameObject spawner;
    Transform spawnerLocation;
    Rigidbody2D rb;
    public GameObject ExplodedBomb;
    public string bombType;
    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.Find("BombSpawner");
        spawnerLocation = spawner.GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(spawnerLocation.right * 10f,ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.isCircleBomb == false && Player.isSquareBomb == false)
        {
            return;
        }
        if (Player.isCircleBomb == true)
        {
            if (bombType == "circle")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    CallExplode();
                    SkillCooldown.CircleBombCoolDown = 10f;
                    Player.useCircleBomb = false;
                }

            }
        }
        if (Player.isSquareBomb == true)
        {
            if (bombType == "square")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    CallExplode();
                    SkillCooldown.SquareBombCoolDown = 10f;
                    Player.useSquareBomb = false;
                }
            }
        }
    }
    public void CallExplode()
    {
        Instantiate(ExplodedBomb, transform.position, Quaternion.identity);
        Destroy(gameObject);
        if (bombType == "circle")
        {
            BombSpawner.CircleBombCount -= 1; // <>
        }
        if (bombType == "square")
        {
            BombSpawner.SquareBombCount -= 1; // <>
        }
    }
}
