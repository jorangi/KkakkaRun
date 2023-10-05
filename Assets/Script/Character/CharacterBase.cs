using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public string state = "normal";
    public PetBase Pet;
    private float hp;
    public float MaxHP;
    private float addHP;
    public float invincibleTime = -1.0f;
    public float AddHP
    {
        get => addHP;
        set
        {
            value = Mathf.Clamp(value, 0.0f, float.MaxValue);
            addHP = value;
        }
    }
    public float HP
    {
        get => hp;
        set
        {
            value = Mathf.Clamp(value, 0.0f, MaxHP);
            if(value == 0.0f)
            {
                anim.SetBool("Dead", true);
                RunAnimation("normal_down1");
                Speed = 0.0f;
                uiCon.Invoke("GameOver", 3.0f);
            }
            hp = value;
        }
    }
    private SpriteRenderer spr;
    public Animator anim;
    public Rigidbody2D rigid;
    public Collider2D col;
    public Transform Foot;
    public UIController uiCon;
    public int jumpCount = 0;
    public bool Slide = false;
    private float oneCounter = 0.0f;
    private float EnergyReduce = 1.0f;
    public float JumpPower;
    private uint score = 0;
    private uint coin = 0;
    private float giantTime;
    public float GiantTime
    {
        get => giantTime;
        set
        {
            value = Mathf.Clamp(value, 0.0f, float.MaxValue);
            if(value > 0.0f)
            {
                transform.localScale = new Vector3(5.0f, 5.0f);
            }
            else
            {
                transform.localScale = new Vector3(1.5f, 1.5f);
            }
            giantTime = value;
        }
    }
    private float boostTime;
    public float BoostTime
    {
        get => boostTime;
        set
        {
            value = Mathf.Clamp(value, 0.0f, float.MaxValue);
            if(value > 0.0f)
            {
                if(boostTime == 0.0f)
                {
                    speedBackup = Speed;
                    RunAnimation(state+"_sprint");
                }
                Speed = 30.0f;
            }
            else
                Speed = speedBackup;
            boostTime = value;
        }
    }
    
    public uint Coin
    {
        get => coin;
        set
        {
            coin = value;
            uiCon.Coin.text = string.Format("{0:#,###}", value);
        }
    }
    public uint Score
    {
        get => score;
        set
        {
            score = value;
            uiCon.Score.text = string.Format("{0:#,###}", value);
            if(value > uiCon.BestScore)
                uiCon.BestScore = value;
        }
    }
    private float speedBackup;
    private float speed = 6.5f;
    public float Speed
    {
        get => speed;
        set
        {
            if(value < 15.0f) value = Mathf.Clamp(value, 0.0f, 15.0f);
            speed = value;
        }
    }
    private float acc = 0.0f;
    public float Acc
    {
        get => acc;
        set
        {
            acc = value;
        }
    }
    public bool IsLanding = false;
    public LayerMask floorMask;
    RaycastHit2D hit;
    public delegate void MethodEff(JellyData jelly);
    public List<MethodEff> getJelly = new();

    private void Awake() 
    {
        invincibleTime = -1.0f;
        spr = GetComponent<SpriteRenderer>();
        JumpPower = 12;
        anim = GetComponent<Animator>();
        uiCon = FindObjectOfType<UIController>();
        MaxHP = 100.0f;
        HP = 100.0f;
        AddHP = 0.0f;
        Acc = Time.deltaTime;
        TreasureSet(uiCon.Treasures.GetChild(1), "bubblegun");
        TreasureSet(uiCon.Treasures.GetChild(2), "skate");
        TreasureSet(uiCon.Treasures.GetChild(3), "cupcake");
    }
    public void TreasureSet(Transform slot, string id)
    {
        if(slot.TryGetComponent<TreasureBase>(out TreasureBase tr))
            Destroy(tr);
        switch(id)
        {
            case "bubblegun":
            slot.AddComponent<BubbleGun>().Init(uiCon, slot);
            break;
            case "skate":
            slot.AddComponent<Skate>().Init(uiCon, slot);
            break;
            case "cupcake":
            slot.AddComponent<Cupcake>().Init(uiCon, slot);
            break;
        }
    }
    protected virtual void Update() 
    {
        if(transform.position.y < -10.0f)
        {
            rigid.gravityScale = 0.0f;
            rigid.velocity = Vector2.zero;
            AddHP = 0.0f;
            HP = 0.0f;
        }
        spr.color = new Color(-invincibleTime, -invincibleTime, -invincibleTime);
        invincibleTime -= Time.deltaTime;
        hit = Physics2D.Raycast(Foot.position, Vector2.down, float.MinValue, floorMask);
        if(GiantTime > 0.0f) GiantTime -= Time.deltaTime;
        if(BoostTime > 0.0f && HP > 0.0f) BoostTime -= Time.deltaTime;
        
        if(IsLanding && (rigid.velocity.y > 0.1f || rigid.velocity.y < -0.1f))
        {
            IsLanding = false;
            col.isTrigger = true;
        }
        oneCounter += Time.deltaTime;
        if(oneCounter >= 1.0f && HP > 0.0f)
        {
            ReduceEnergy(EnergyReduce);
            oneCounter -= 1.0f;
        }
        uiCon.SetHPBar();
    }
    public void ReduceEnergy(float value)
    {
        float dmg = value;
        dmg -= Mathf.Clamp(AddHP, 0, dmg);
        AddHP -= value;
        HP -= dmg;
    }
    public void RunAnimation(string id)
    {
        anim.Play(id);
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(rigid.velocity.y > -0.1f && rigid.velocity.y < 0.1f && !IsLanding && HP > 0.0f)
        {
            IsLanding = true;
            jumpCount = 0;
        } 
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(hit.collider != null)
        {
            col.isTrigger = false;
        }
    }
    public virtual void GetJelly(JellyData jelly)
    {
        Score += jelly.Score;
        foreach(MethodEff m in getJelly) m(jelly);
    }
}