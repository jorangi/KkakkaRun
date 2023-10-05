using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour
{
    public uint RecordedScore = 0;
    private static ScoreRecorder scoreRecorder = null;
    public static ScoreRecorder SRecorder
    {
        get
        {
            if(scoreRecorder == null)
            {
                return null;
            }
            return scoreRecorder;
        }
    }
    private CharacterBase character;
    private void Awake()
    {
        if(scoreRecorder == null)
        {
            scoreRecorder = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        character = FindObjectOfType<CharacterBase>();
    }
}
