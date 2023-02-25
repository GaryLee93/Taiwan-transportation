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
    objectPooler instance;
    private void Start()
    {
        instance = objectPooler.instance;
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
        instance.spawnFromPool("bullet",FirePoint_1.transform.position,FirePoint_1.transform.rotation);
        instance.spawnFromPool("bullet",FirePoint_2.transform.position,FirePoint_2.transform.rotation);
    }
}
