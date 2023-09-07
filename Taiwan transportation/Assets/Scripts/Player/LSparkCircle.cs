using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSparkCircle : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float XscaleSpeed;
    [SerializeField] float YscaleSpeed;
    [SerializeField] int damage;
    void Start(){
        transform.localScale = new Vector3(1f, 0.3f, 1);
    }
    void Update(){
        transform.localScale += new Vector3(XscaleSpeed *Time.deltaTime, YscaleSpeed *Time.deltaTime, 1);
        transform.Translate(0, moveSpeed *Time.deltaTime, 0);
        if(hitBorder())
            Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "enemy"){
            other.gameObject.GetComponent<AbsNormalEnemy>().takeDamage(damage);
        }
        else if(other.gameObject.tag == "boss"){
            other.gameObject.GetComponent<abstractBoss>().takeDamage(damage);
        }
        else if(other.gameObject.tag == "enemy_bullet"){
            other.gameObject.GetComponent<Ipooled>().poolDespawn();
        }
    }
    private bool hitBorder(){
        const float XBORDER = 6.7f, YBORDER = 8f;
        return this.transform.position.x <-XBORDER || this.transform.position.x > XBORDER || 
                this.transform.position.y > YBORDER || this.transform.position.y < -YBORDER ;
    }
}
