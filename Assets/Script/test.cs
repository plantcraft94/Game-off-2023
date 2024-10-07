using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class test : MonoBehaviour
{
    Camera cam;
    public float CamShakeStrength;
    public int CamVibrato;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cam.DOShakePosition(0.1667f, CamShakeStrength, CamVibrato, 45f, true);
        }
    }
}
