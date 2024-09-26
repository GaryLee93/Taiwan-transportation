using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbit_half_cycle : MonoBehaviour
{
    public float shooter_rotate_v = 300;
    public int rotate_time=0;
    public bool constraint = true;
    private void Update() 
    {
        if(transform.localPosition.y>0 && constraint)
        {
            rotate_time++;
            shooter_rotate_v *= (-1);
        }
        transform.RotateAround(transform.parent.transform.position,Vector3.forward,-shooter_rotate_v*Time.deltaTime);
    }
}
