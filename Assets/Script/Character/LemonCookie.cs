using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LemonCookie : CharacterBase
{
    public SpriteRenderer SkillGauage, SkillGauageBack;
    public GameObject abilityMagnet;
    private readonly float SetCoolDown = 8.0f;
    private float coolDown;
    private float CoolDown
    {
        get => coolDown;
        set
        {
            value = Mathf.Clamp(value, 0.0f, SetCoolDown);
            coolDown = value;
            if(value == 0.0f)
            {
                SkillOn = true;
            }
            else
            {
                SkillGauage.transform.localScale = new Vector3((SetCoolDown - value)/SetCoolDown, 1);
            }
        }
    }
    private bool skillOn;
    public bool SkillOn
    {
        get => skillOn;
        set
        {
            SkillGauageBack.gameObject.SetActive(!value);
            abilityMagnet.gameObject.SetActive(value);
            skillOn = value;
            state = value ? "special" : "normal";
            if(!value)
                CoolDown = SetCoolDown;
        }
    }
    private void Start()
    {
        CoolDown = 100.0f;
        SkillOn = false;
    }
    protected override void Update()
    {
        base.Update();
        if(CoolDown > 0.0f && HP > 0.0f)
        {
            CoolDown -= Time.deltaTime;
        }
    }
}