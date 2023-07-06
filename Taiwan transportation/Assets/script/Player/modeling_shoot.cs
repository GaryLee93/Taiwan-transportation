using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modeling_shoot : MonoBehaviour
{
    public GameObject bullet;
    [SerializeField] float shoot_interval=0.05f;
    private float timer;
    void Start()
    {
        timer = 0;
    }
    void FixedUpdate()
    {   
        if(timer>=shoot_interval)
        {
            shoot();
            timer-=shoot_interval;
        }
        timer+=Time.fixedDeltaTime;
    }
    void shoot()
    {
        if(Input.GetButton("Fire1"))
        {
            objectPooler.spawnFromPool(bullet.name,transform.position,
            transform.rotation,null);
        }
    }
    
}

