using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SkillMenu : MonoBehaviour
{
    Transform Child;
    [SerializeField] Volume GlobalVolume;
    private DepthOfField dof;
    float timeelapse;
    public Player Player;
    private void Start()
    {
        Child = transform.GetChild(0);
    }
    private void Update()
    {
        timeelapse += Time.fixedDeltaTime;
        if (gameObject.activeInHierarchy == true)
        {
            if(GlobalVolume.profile.TryGet(out dof))
            {
                dof.focusDistance.value = Mathf.Lerp(2f, 0.8f, timeelapse / 0.5f);
            }
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            
            print(GetSkill());

            if (GlobalVolume.profile.TryGet(out dof))
            {
                dof.focusDistance.value = Mathf.Lerp(0.8f, 2f, timeelapse / 0.5f);
            }

            transform.gameObject.SetActive(false);
            Time.timeScale = 1f;
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
}
