using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform firepoint;
    public float shot_interval;
    private float timer=0;
    private void Start()
    {
        Instantiate(bullet,firepoint.position,firepoint.rotation);
    }
    private void Update()
    {
        if(timer>=shot_interval)
        {
            Instantiate(bullet,firepoint.position,firepoint.rotation);
            timer-=shot_interval;
        }
        timer+=Time.deltaTime;
    }
}
