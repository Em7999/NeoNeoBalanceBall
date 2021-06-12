using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnThings : MonoBehaviour
{
    float CreatFoodTime = 5f;
    float CreatBulletTime = 5f;
    public float FoodLeastTime = 3;
    public float BulletLeastTime = 4;
    GameObject Food;
    GameObject Bullet;
    public GameObject obj_food;
    public GameObject obj_bigFood;
    public GameObject obj_bullet;
    public GameObject obj_bigBullet;
    protected Transform m_transform;
    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        CreatFoodTime -= Time.deltaTime;
        if (CreatFoodTime <= 0)
        {
            CreatFoodTime = Random.Range(FoodLeastTime, FoodLeastTime+5);
            Food = Instantiate<GameObject>(obj_food);
            Food.transform.position = m_transform.position + new Vector3(-5, Random.Range(4f, -4f), 0);
        }

        CreatBulletTime -= Time.deltaTime;
        if(CreatBulletTime <= 0)
        {
            CreatBulletTime = Random.Range(BulletLeastTime, BulletLeastTime+5);
            Bullet = Instantiate<GameObject>(obj_bullet);
            Bullet.transform.position = m_transform.position + new Vector3(-3, Random.Range(4f, -4f), 0);
        }
    }

    public void CreateBigFood()
    {
        Instantiate(obj_bigFood, m_transform.position + new Vector3(-5, Random.Range(4f, -4f), 0), Quaternion.identity);
    }

    public void CreateBigBullet()
    {
        Instantiate(obj_bigBullet, m_transform.position + new Vector3(-3, Random.Range(4f, -4f), 0), Quaternion.identity);
    }
}
