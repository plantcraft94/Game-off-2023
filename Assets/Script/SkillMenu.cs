using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SkillMenu : MonoBehaviour
{
    Transform Child;
    public Player Player;
    public MouseLocation MouseLocation;
    private void Start()
    {
        Child = transform.GetChild(0);
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {        
            MouseLocation.WarpMouse();
            StartCoroutine(Wait());
            Player.isEnableSkills = true;
            Player.ChangeAbility(GetSkill());
        }
    }
    public string GetSkill()
    {
        foreach(Transform segment in Child)
        {
            Segment getsegment = segment.GetComponent<Segment>();
            if (getsegment != null && getsegment.isHovered)
            {
                return segment.GetComponent<Segment>().AbilityName;
            }

        }
        return null;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(1/60f);        
        Time.timeScale = 1f;
        transform.gameObject.SetActive(false);
        print("warp");
    }
}
