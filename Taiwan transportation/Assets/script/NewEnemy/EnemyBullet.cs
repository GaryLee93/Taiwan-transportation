using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour, Ipooled
{
    public bool isGrazed;
    public void onBulletSpawn(){

    }
    public void poolDespawn(){
        gameObject.SetActive(false);
    }
}
