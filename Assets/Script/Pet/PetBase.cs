using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetBase : MonoBehaviour
{
    public GameObject Body;
    private readonly float SPD = 5.0f; 
    public CharacterBase character;
    public UIController uiCon;
    public Animator anim;
    private float magnetTime = 0.0f;
    public float MagnetTime
    {
        get => magnetTime;
        set
        {
            value = Mathf.Clamp(value, 0.0f, float.MaxValue);
            if(value == 0.0f)
            {
                anim.Play("Idle");
            }
            else
            {
                anim.Play("Magnet");
            }
            magnetTime = value;
        }
    }
    private void Update()
    {
        if(MagnetTime > 0.0f)
        {
            MagnetTime -= Time.deltaTime;
            transform.position = Vector2.Lerp(transform.position, Vector2.zero, Time.deltaTime * SPD);
        }
        else if(character.Slide)
        {
            transform.position = Vector2.Lerp(transform.position, character.transform.position - new Vector3(1.8f, 0.0f), Time.deltaTime * SPD);
        }
        else
        {
            transform.position = Vector2.Lerp(transform.position, character.transform.position - new Vector3(1.8f, -2.0f), Time.deltaTime * SPD);
        }
    }
}