using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public Transform FirePoint_1;
    public Transform FirePoint_2;
    public GameObject bullet;
    public float shootFre=2.0f;
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
        Instantiate(bullet,FirePoint_1.position,FirePoint_1.rotation);
        Instantiate(bullet,FirePoint_2.position,FirePoint_2.rotation);
    }
}
