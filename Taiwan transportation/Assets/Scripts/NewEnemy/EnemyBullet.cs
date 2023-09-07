using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour, Ipooled
{
    public bool isGrazed;
    [SerializeField] float borderX, borderY;
    void Update(){
        if(hitBorder()) 
            poolDespawn();
    }
    bool hitBorder(){
        if(Mathf.Abs(transform.position.x) > borderX || Mathf.Abs(transform.position.y) > borderY) 
            return true;
        else 
            return false;
    }
    public void onBulletSpawn(){

    }
    public void poolDespawn(){
        gameObject.SetActive(false);
    }
    public void transformIntoScore()
    {
        Instantiate(StageObj.Collectables["grayScore"],transform.position,new Quaternion());
        gameObject.SetActive(false);
    }
}
