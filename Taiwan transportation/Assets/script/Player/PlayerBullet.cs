using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour, Ipooled
{
    enum BType{
        Straight,
        Homing,
        BombBullet
    }
    [SerializeField] BType bulletType;
    [SerializeField] float bulletSpeed;
    int bulletDamage;
    Rigidbody2D rb;
    private void Start(){
        rb = GetComponent<Rigidbody2D>();
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
    public void onBulletSpawn(){
        if(bulletType == BType.Straight){
            rb.velocity = new Vector2(0,bulletSpeed);
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
    public void setParent(GameObject p){
        return;
    }
    
    private void Update() {
        if(hitBorder())
            gameObject.SetActive(false);
    }
    private bool hitBorder(){
        const float XBORDER = 6.7f, YBORDER = 7.5f;
        return this.transform.position.x <-XBORDER || this.transform.position.x > XBORDER || 
                this.transform.position.y > YBORDER || this.transform.position.y < -YBORDER ;
    }
}
