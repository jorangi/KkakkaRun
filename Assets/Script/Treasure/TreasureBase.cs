using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class TreasureBase : MonoBehaviour
{
    protected CharacterBase character;
    private UIController uiCon;
    public Transform slot;
    public uint grade;
    public Image icon;
    public Image Effect;
    public Image CooldownSprite;
    protected float RunningTime;
    public float Cooldown;
    public void Init(UIController uiCon, Transform slot)
    {
        this.slot = slot;
        icon = slot.GetChild(0).GetComponent<Image>();
        CooldownSprite = slot.GetChild(1).GetComponent<Image>();
        Effect = slot.GetChild(2).GetComponent<Image>();
        this.uiCon = uiCon;
        character = uiCon.character;
        StartCoroutine(StayForCooldown());
    }
    protected virtual IEnumerator Run()
    {
        Effect.enabled = false;
        CooldownSprite.enabled = true;
        StartCoroutine(StayForCooldown());
        yield break;
    }
    protected virtual IEnumerator StayForCooldown()
    {
        Effect.enabled = true;
        CooldownSprite.enabled = false;
        StartCoroutine(Run());
        yield break;
    }
}
