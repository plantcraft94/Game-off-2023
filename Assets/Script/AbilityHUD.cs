using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityHUD : MonoBehaviour
{
    public Sprite Magnet, Stasis, StasisCD, SBomb, SBombCD, CBomb, CBombCD, Transparent;
    Image Recharge;
    Image Icon;
    // Start is called before the first frame update
    void Start()
    {
        Recharge = transform.GetChild(0).gameObject.GetComponent<Image>();
        Icon = transform.GetChild(1).gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.isMagnet)
        {
            Recharge.sprite = Magnet;
            Icon.sprite = Magnet;
            Icon.fillAmount = 1;
        }
        if (Player.isStasis)
        {
            Recharge.sprite = StasisCD;
            Icon.sprite = Stasis;
            if (SkillCooldown.StasisCoolDown >= 0)
            {
                Icon.fillAmount = 1f - (SkillCooldown.StasisCoolDown / 15f);
            }
            else
            {
                Icon.fillAmount = 1f;
            }
        }
        if (Player.isSquareBomb)
        {
            Recharge.sprite = SBombCD;
            Icon.sprite = SBomb;
            if (SkillCooldown.SquareBombCoolDown >= 0)
            {
                Icon.fillAmount = 1f - (SkillCooldown.SquareBombCoolDown / 10f);
            }
            else
            {
                Icon.fillAmount = 1f;
            }
        }
        if (Player.isCircleBomb)
        {
            Recharge.sprite = CBombCD;
            Icon.sprite = CBomb;
            if (SkillCooldown.CircleBombCoolDown >= 0)
            {
                Icon.fillAmount = 1f - (SkillCooldown.CircleBombCoolDown / 10f);
            }
            else
            {
                Icon.fillAmount = 1f;
            }
        }
        if (!Player.isMagnet && !Player.isStasis && !Player.isSquareBomb && !Player.isCircleBomb)
        {
            Icon.sprite = Transparent;
            Recharge.sprite = Transparent;
        }
    }
}
