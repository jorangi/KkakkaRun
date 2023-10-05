using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Map : MonoBehaviour
{
    public CharacterBase character;
    public GameObject Jellybean;
    public GameObject MagnetJelly, GiantJelly, BoostJelly, HealingJelly;
    public Transform Jellies;
    public Transform ObjectPool;
    public Transform ObstaclePool;
    public GameObject ObstaclePrefab;
    public GameObject FloorPrefab;
    public Transform Objects;
    public Transform Floors;
    public Transform FloorPool;
    public List<JellyData> jellyDatas;
    private void Awake()
    {
        CreateFloor();
        CreateJellies();
        SetFloors();
        SetJellyBeans();
    }
    private void CreateJellies()
    {
        for(int i = 0; i < 1000; i++)
        {
            GameObject jel = Instantiate(Jellybean, ObjectPool);
            jel.name = "Jellybean";
            JelliesObject Jel = jel.GetComponent<JelliesObject>();
            Jel.pool = ObjectPool;
            Jel.character = character;
            Jel.map = this;
            jel.SetActive(false);
        }

        GameObject magnetJelly = Instantiate(MagnetJelly, ObjectPool);
        magnetJelly.name = "Magnet";
        MagnetJelly Mag = magnetJelly.GetComponent<MagnetJelly>();
        Mag.pool = ObjectPool;
        Mag.character = character;
        Mag.map = this;
        magnetJelly.SetActive(false);
        
        GameObject giantJelly = Instantiate(GiantJelly, ObjectPool);
        giantJelly.name = "Giant";
        GiantJelly giant = giantJelly.GetComponent<GiantJelly>();
        giant.pool = ObjectPool;
        giant.character = character;
        giant.map = this;
        giantJelly.SetActive(false);
        
        GameObject boostJelly = Instantiate(BoostJelly, ObjectPool);
        boostJelly.name = "Boost";
        BoostJelly boost = boostJelly.GetComponent<BoostJelly>();
        boost.pool = ObjectPool;
        boost.character = character;
        boost.map = this;
        boostJelly.SetActive(false);
        
        GameObject healingJelly = Instantiate(HealingJelly, ObjectPool);
        healingJelly.name = "Healing";
        HealingJelly healing = healingJelly.GetComponent<HealingJelly>();
        healing.pool = ObjectPool;
        healing.character = character;
        healing.map = this;
        healingJelly.SetActive(false);

        magnetJelly.transform.SetSiblingIndex(Random.Range(0, ObjectPool.childCount));
        giantJelly.transform.SetSiblingIndex(Random.Range(0, ObjectPool.childCount));
        boostJelly.transform.SetSiblingIndex(Random.Range(0, ObjectPool.childCount));
        healingJelly.transform.SetSiblingIndex(Random.Range(0, ObjectPool.childCount));
    }
    private void CreateFloor()
    {
        for(int i = 0; i < 40; i++)
        {
            GameObject floor = Instantiate(FloorPrefab, FloorPool);
            floor.name = "Floor";
            FloorBase _floor = floor.GetComponent<FloorBase>();
            _floor.character = character;
            _floor.map = this;
            floor.SetActive(false);
        }
            GameObject obstacle = Instantiate(ObstaclePrefab, ObstaclePool);
            obstacle.name = "Obstacle";
            ObstacleBase _obstacle = obstacle.GetComponent<ObstacleBase>();
            _obstacle.character = character;
            _obstacle.map = this;
            obstacle.SetActive(false);
    }
    private void SetJellyBeans()
    {
        for(int i = 0; i < ObjectPool.childCount; i++)
        {
            Transform jel = ObjectPool.GetChild(0);
            jel.SetParent(Jellies);
            jel.gameObject.SetActive(true);
            jel.transform.localPosition = new Vector2(i, -2.0f);
        }
    }
    private void SetFloors()
    {
        for(int i = 0; i < FloorPool.childCount; i++)
        {
            Transform floor = FloorPool.GetChild(0);
            floor.SetParent(Floors);
            floor.gameObject.SetActive(true);
            floor.transform.localPosition = new Vector2(-6 + i * 1.24f, -2.75f);
            floor.transform.localScale = Vector3.one;
        }
            Transform obstacle = ObstaclePool.GetChild(0);
            obstacle.SetParent(Objects);
            obstacle.gameObject.SetActive(true);
            obstacle.transform.localPosition = new Vector2(-6 + 30, -2.2f);
            obstacle.transform.localScale = new Vector3(1.5f, 1.5f);
    }
    private void SetJellyBean(JelliesObject jel, JellyData jellyData)
    {
        JellyType type = jel.data.type;
        if(type == JellyType.magnet || type == JellyType.giant || type == JellyType.boost || type == JellyType.healing)
            return;
        jel.data = jellyData;
        jel.GetComponent<SpriteRenderer>().sprite = jellyData.Sprite;
    }
    public void RecycleJellybean()
    {
        JelliesObject jel = ObjectPool.transform.GetChild(0).GetComponent<JelliesObject>();
        jel.transform.SetParent(Jellies);
        jel.gameObject.SetActive(true);
        jel.transform.localPosition = new Vector2(jel.transform.localPosition.x + Jellies.childCount, Random.Range(-2.0f, 2.0f));
        JellyType type = jel.data.type;
        if(type == JellyType.magnet || type == JellyType.giant || type == JellyType.boost || type == JellyType.healing) return;
        int rand = Random.Range(0, 101);
        if(rand >= 90)
        {
            SetJellyBean(jel, jellyDatas[2]);
        }
        else if(rand >= 70)
        {
            SetJellyBean(jel, jellyDatas[1]);
        }
        else
        {
            SetJellyBean(jel, jellyDatas[0]); 
        }
    }
    public void RecycleFloor(FloorBase floor)
    {
        floor.transform.localPosition = new Vector2(floor.transform.localPosition.x + 3 + Floors.childCount * 1.24f, -2.75f);
    }
    public void RecycleObstacle(ObstacleBase obstacle)
    {
        obstacle.anim.Play("Default");
        obstacle.transform.localPosition = new Vector2(obstacle.transform.localPosition.x + 30, -2.2f);
    }
}