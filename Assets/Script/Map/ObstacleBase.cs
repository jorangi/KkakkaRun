using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBase : ObjectBase
{
    public Animator anim;
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
        map.RecycleObstacle(this);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == character.gameObject)
        {
            if(character.BoostTime > 0.0f || character.GiantTime >0.0f)
            {
                anim.Play("DestroyObstacle");
                return;
            }
            else if(character.invincibleTime > 0.0f)
            {
                return;
            }
            else if((character as LemonCookie).SkillOn)
            {
                (character as LemonCookie).SkillOn = false;
                character.invincibleTime = 0.5f;
                character.Speed = 6.5f;
            }
            else
            {
                character.invincibleTime = 0.5f;
                character.ReduceEnergy(10.0f);
                character.Speed = 6.5f;
            }
        }
    }
}