using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    enum BType{
        Straight,
        Homing,
        BombBullet
    }
    [SerializeField] BType bulletType;
    private int bulletDamage;
    private void Start() {
        if(bulletType == BType.Straight){
            bulletDamage = 5;
        }
        else if(bulletType == BType.Homing){
            bulletDamage = 1;
        }
        else if(bulletType == BType.BombBullet){
            bulletDamage = 100;
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "enemy"){
            normalEnemy hitEnemy = other.gameObject.GetComponent<normalEnemy>();
            if(bulletType != BType.BombBullet){
                hitEnemy.takeDamage(bulletDamage);
                gameObject.SetActive(false);
            }
        }
    }
}
