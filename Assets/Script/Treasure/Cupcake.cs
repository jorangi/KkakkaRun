using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cupcake : TreasureBase
{
    public void Buff(JellyData jelly)
    {
        if(jelly.type == JellyType.basic) character.Score += (uint)(jelly.Score * 0.5f);
    }
    protected override IEnumerator Run()
    {
        character.getJelly.Add(Buff);
        while(RunningTime > 0.0f)
        {
            Effect.transform.Rotate(new(0, 0, Time.deltaTime * 200.0f));
            RunningTime -= Time.deltaTime;
            yield return null;
        }
        Cooldown = 7.0f;
        character.getJelly.Remove(Buff);
        yield return base.Run();
    }
    protected override IEnumerator StayForCooldown()
    {
        while(Cooldown > 0.0f)
        {
            CooldownSprite.fillAmount = Cooldown/7.0f;
            Cooldown -= Time.deltaTime;
            yield return null;
        }
        RunningTime = 3.0f;
        yield return base.StayForCooldown();
    }
}
