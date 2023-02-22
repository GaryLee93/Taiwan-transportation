using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting_childPlane : MonoBehaviour
{
    public Transform FirePoint_childPlane;
    public GameObject bullet;
    public float shootFre=0.05f;
    private float timer;
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
        Debug.Log("shoot");
        Instantiate(bullet,FirePoint_childPlane.position,FirePoint_childPlane.rotation);
    }
    
}

