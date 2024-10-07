using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCooldown : MonoBehaviour
{
    public static float StasisCoolDown;
    public static float SquareBombCoolDown;
    public static float CircleBombCoolDown;

    void Start()
    {
        StasisCoolDown = 0;
        SquareBombCoolDown = 0;
        CircleBombCoolDown = 0;
    }

    void Update()
    {
        StasisCoolDown -= Time.deltaTime;
        SquareBombCoolDown -= Time.deltaTime;
        CircleBombCoolDown -= Time.deltaTime;
    }
}
