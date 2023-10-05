using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBase : ObjectBase
{
    public CharacterBase character;
    public Map map;
    private void Update()
    {
        transform.position -= new Vector3(character.Speed * Time.deltaTime, 0, 0);
        if(transform.localPosition.x < -12.0f)
        {
            ExitToPool();
        }
    }
    private void ExitToPool()
    {
        map.RecycleFloor(this);
    }
}
    
