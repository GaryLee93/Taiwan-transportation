using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletSpawner : MonoBehaviour
{
    objectPooler instance;
    void Start()
    {
        instance=objectPooler.instance;
    }
    void FixedUpdate()
    {
        if(Input.GetButton("Fire1"))
        {
            instance.spawnFromPool("bullet",transform.position,Quaternion.identity);
        }
    }
}
