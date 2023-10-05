using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JelliesObject : MonoBehaviour
{
    public Transform pool;
    public CharacterBase character;
    public JellyData data;
    public Map map;
    private void Update()
    {
        transform.position -= new Vector3(character.Speed * Time.deltaTime, 0, 0);
        if(transform.localPosition.x < -12.0f)
        {
            ExitToPool();
        }
    }
    protected void ExitToPool()
    {
        transform.SetParent(pool);
        gameObject.SetActive(false);
        map.RecycleJellybean();
    }
    protected virtual void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject == character.gameObject || other.gameObject == character.Pet.Body && character.Pet.MagnetTime > 0.0f)
        {
            character.GetJelly(data);
            ExitToPool();
        }
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.layer == 7 && (character as LemonCookie).SkillOn)
        {
            transform.position = Vector2.Lerp(transform.position, character.transform.position, Time.deltaTime * (character.Speed + 1.0f));
            return;
        }
        if(other.gameObject.layer == 9 && character.Pet.MagnetTime > 0.0f)
        {
            transform.position = Vector2.Lerp(transform.position, character.Pet.transform.position, Time.deltaTime * (character.Speed + 1.0f));
            return;
        }
    }
}