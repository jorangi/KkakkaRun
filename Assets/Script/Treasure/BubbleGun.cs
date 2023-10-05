using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGun : TreasureBase
{
    readonly float v = 5.0f;
    readonly float c = 25.0f;
    protected override IEnumerator Run()
    {
        character.AddHP += v;
        Cooldown = c;
        yield return base.Run();
    }
    protected override IEnumerator StayForCooldown()
    {
        while(Cooldown > 0.0f)
        {
            CooldownSprite.fillAmount = Cooldown/c;
            Cooldown -= Time.deltaTime;
            yield return null;
        }
        yield return base.StayForCooldown();
    }
}
