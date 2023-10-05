using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skate : TreasureBase
{
    protected override IEnumerator Run()
    {
        float t = 0.0f;
        while(true)
        {
            t += Time.deltaTime;
            if(character.HP > 0.0f && t >= 1.0f) 
            {
                character.Speed += 0.2f;
                t -= 1.0f;
            }
            Effect.transform.Rotate(new(0, 0, Time.deltaTime * 200.0f));
            yield return null;
        }
    }
}
