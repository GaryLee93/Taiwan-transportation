using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modeling_shoot : MonoBehaviour
{
    public Transform FirePoint_childPlane;
    public GameObject bullet;
    public float shootFre=0.05f;
    private float timer;
    objectPooler instance;
    void Start()
    {
        instance=objectPooler.instance;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(Input.GetButton("Fire1")&&timer>=shootFre)
        {
            timer-=shootFre;
            shoot();
        }
    }
    void shoot()
    {
        instance.spawnFromPool("bullet",FirePoint_childPlane.transform.position,
        FirePoint_childPlane.transform.rotation);
    }
    
}

