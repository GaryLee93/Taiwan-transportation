using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modeling_shoot : MonoBehaviour
{
    public Transform FirePoint_childPlane;
    public GameObject bullet;
    public float shoot_interval=0.05f;
    private float timer;
    objectPooler instance;
    void Start()
    {
        instance=objectPooler.instance;
    }
    void Update()
    {
        if(timer>=shoot_interval)
        {
            shoot();
            timer-=shoot_interval;
        }
        timer+=Time.deltaTime;
    }
    void shoot()
    {
        if(Input.GetButton("Fire1"))
        {
            instance.spawnFromPool(bullet.name,FirePoint_childPlane.transform.position,
            FirePoint_childPlane.transform.rotation);
        }
    }
    
}

