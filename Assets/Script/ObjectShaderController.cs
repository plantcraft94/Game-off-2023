using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShaderController : MonoBehaviour
{
    Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.useMagnet)
        {
            if(Stasis.isStasis)
            {
                material.SetInt("_IsUseMagnet", 0);
                return;
            }
            material.SetInt("_IsUseMagnet", 1);
        }
        else if (Player.useLock)
        {
            material.SetInt("_IsUseMagnet", 0);
        }           
    }
}
