using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostJelly : JelliesObject
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == character.gameObject || other.gameObject == character.Pet.Body && character.Pet.MagnetTime > 0.0f)
        {
            character.BoostTime = 5.0f;
            ExitToPool();
        }
    }
}