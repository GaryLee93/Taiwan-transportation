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
        if(Mathf.Abs(transform.position.x)>9 || Mathf.Abs(transform.position.y)>10) return true;
        else return false;
    }
    public void onBulletSpawn(){

    }
    public void poolDespawn(){
        gameObject.SetActive(false);
    }
}
