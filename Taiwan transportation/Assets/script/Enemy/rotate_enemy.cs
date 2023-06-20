using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_enemy : MonoBehaviour
{
    public float shooter_rotate_v = 150;
    public int rotate_time=0;
    void Update()
    {
        if(transform.localPosition.y>0)
        {
            rotate_time++;
            shooter_rotate_v *= (-1);
        }
        transform.RotateAround(transform.parent.transform.position,Vector3.forward,-shooter_rotate_v*Time.deltaTime);
    }
}
