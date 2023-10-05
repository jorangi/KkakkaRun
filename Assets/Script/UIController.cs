using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public RectTransform GameOverScene;
    private uint bestScore;
    public uint BestScore
    {
        get => bestScore;
        set
        {
            bestScore = value;
            BestRecord.text = $"나의 최고 점수 {string.Format("{0:#,###}", Mathf.Max(ScoreRecorder.SRecorder.RecordedScore, value))}점";
        }
    }
    public RectTransform Treasures;
    public TextMeshProUGUI BestRecord;
    public TextMeshProUGUI Score;
    public TextMeshProUGUI Coin;
    public CharacterBase character;
    public Image HPBar;
    public Image AddHPBar;
    public Image HPBack;
    public RectTransform glow;

    private void Awake()
    {
        BestRecord.text = ScoreRecorder.SRecorder.RecordedScore > 0 ? $"나의 최고 점수 {string.Format("{0:#,###}", ScoreRecorder.SRecorder.RecordedScore)}점" : "나의 최고 점수 0점";
    }
    public void SetHPBar()
    {
        HPBack.fillAmount = 0.5f + (character.MaxHP - 100.0f) / 200.0f;
        HPBar.fillAmount = character.HP / character.MaxHP * HPBack.fillAmount;
        AddHPBar.fillAmount = character.AddHP / character.MaxHP;
        AddHPBar.rectTransform.anchoredPosition = new Vector2(1150.0f * HPBar.fillAmount - 3.0f, 0);
        glow.anchoredPosition = AddHPBar.fillAmount > 0 ? 
        new Vector2(AddHPBar.rectTransform.anchoredPosition.x + 1150.0f * AddHPBar.fillAmount - 17.0f, 0) : new Vector2(1150.0f * HPBar.fillAmount - 25.0f, 0);
    }
    public void Jump()
    {
        if(character.HP == 0.0f)
            return;
        if(character.jumpCount == 0)
        {
            character.jumpCount ++;
            if(character.state == "normal" && character.BoostTime > 0.0f) character.RunAnimation("sprintJump");
            else character.RunAnimation(character.state+"_jump");
            character.rigid.AddForce(transform.up * character.JumpPower, ForceMode2D.Impulse);
        }
        else if(character.jumpCount == 1)
        {
            character.RunAnimation(character.state+"_roll");
            character.jumpCount ++;
            character.rigid.velocity = character.rigid.velocity.y < 0 ? character.rigid.velocity : Vector2.zero;
            character.rigid.AddForce(character.JumpPower * 1.25f * transform.up, ForceMode2D.Impulse);
        }
    }
    public void SlideOn()
    {
        if(character.HP == 0.0f)
            return;
        if(character.jumpCount == 0)
        {
            if(character.state == "normal" && character.BoostTime > 0.0f) character.RunAnimation("sprintSlide");
            else character.RunAnimation(character.state+"_slide");
            character.Slide = true;
        }
    }
    public void SlideOff()
    {
        if(character.HP == 0.0f)
            return;
        if(character.jumpCount == 0)
        {
            if(character.BoostTime == 0.0f) character.RunAnimation(character.state+"_run");
            else character.RunAnimation(character.state+"_sprint");
            character.Slide = false;
        }
    }
    public void GameOver()
    {
        GameOverScene.gameObject.SetActive(true);
        GameOverScene.Find("Score").GetComponent<TextMeshProUGUI>().text = $"{string.Format("{0:#,###}", character.Score)} 점";
        ScoreRecorder.SRecorder.RecordedScore = (uint)Mathf.Max(character.Score, ScoreRecorder.SRecorder.RecordedScore);
    }
    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
