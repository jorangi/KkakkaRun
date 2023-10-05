using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingJelly : JelliesObject
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == character.gameObject || other.gameObject == character.Pet.Body)
        {
            character.HP += 15.0f;
            ExitToPool();
        }
    }
}
