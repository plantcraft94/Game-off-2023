using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMenu : MonoBehaviour
{
    GameObject AbilityMenu;
    // Start is called before the first frame update
    void Start()
    {
        AbilityMenu = GameObject.Find("AbilityMenu");
        AbilityMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AbilityMenu.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            AbilityMenu.SetActive(false);
        }
    }
}
