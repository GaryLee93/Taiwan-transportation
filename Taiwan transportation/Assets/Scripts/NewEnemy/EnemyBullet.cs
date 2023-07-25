using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour, Ipooled
{
    public bool isGrazed;
    private void Update() 
    {
        if(hitBorder()) poolDespawn();     
    }
    bool hitBorder()
    {
        if(Mathf.Abs(transform.position.x)>7f || Mathf.Abs(transform.position.y)>7.5f) return true;
        else return false;
    }
    public void onBulletSpawn(){

    }
    public void poolDespawn(){
        gameObject.SetActive(false);
    }
}
