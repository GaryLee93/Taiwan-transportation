using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour, Ipooled{
    enum BType{
        Straight, Homing,
    }
    [SerializeField] BType bulletType;
    [SerializeField] float bulletSpeed;
    [SerializeField] int bulletDamage;
    private void Update(){
        if(bulletType == BType.Homing){
            GameObject target = find_closest_enemy();
            if(target == null){
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);
            }
            else{
                Vector2 direction = target.transform.position - transform.position;
                GetComponent<Rigidbody2D>().velocity = bulletSpeed * direction.normalized;
            }
        }

        if(hitBorder())
            poolDespawn();
    }
    public void onBulletSpawn(){
        if(bulletType == BType.Straight){
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,bulletSpeed);
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "enemy"){
            other.gameObject.GetComponent<AbsNormalEnemy>().takeDamage(bulletDamage);
            poolDespawn();
        }
    }
    public void poolDespawn(){
        gameObject.SetActive(false);
    }
    GameObject find_closest_enemy()
    {
        GameObject[] targets=GameObject.FindGameObjectsWithTag("enemy");
        GameObject closest_target=null;
        float distance=Mathf.Infinity;
        Vector2 position=transform.position;
        foreach(GameObject target in targets)
        {
           Vector2 diff=(Vector2)target.transform.position-position;
           float tem_dis=diff.sqrMagnitude;
           if(tem_dis<distance)
           {
                closest_target=target;
                distance=tem_dis;
           }
        }
        return closest_target;
    }
    private bool hitBorder(){
        const float XBORDER = 6.7f, YBORDER = 7.5f;
        return this.transform.position.x <-XBORDER || this.transform.position.x > XBORDER || 
                this.transform.position.y > YBORDER || this.transform.position.y < -YBORDER ;
    }
}
