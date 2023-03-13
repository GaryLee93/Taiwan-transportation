using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    objectPooler instance;
    [SerializeField]GameObject bullet;
    [SerializeField]Rigidbody2D rb;
    public int moveType = 0;
    public int shootType = 0;
    public float movespeed = 2f;
    public Vector2 moveVector = new Vector2(0f, 0f);
    public float shootFreq = 1f;
    float shoot_timer = 0f;
    void Start(){
        instance=objectPooler.instance;
        if(moveType == 1)
            StartCoroutine(move1());
        else if(moveType == 2)
            StartCoroutine(move2());
    }
    void Update(){
        if(hitBorder())
            Destroy(gameObject);
    }

    IEnumerator move1(){
        rb.velocity = moveVector;
        yield return new WaitForSeconds(1f);
    }

    IEnumerator move2(){
        rb.velocity = new Vector2(0f, -1f);
        yield return new WaitForSeconds(2f);

        rb.velocity = new Vector2(0f, 0f);
        yield return new WaitForSeconds(5f);
        
        rb.velocity = new Vector2(0f, -1f);
    }
    void shoot1(){
        if(shoot_timer >= shootFreq){
            instance.spawnFromPool(bullet.name,transform.GetChild(0).transform.position,
            transform.GetChild(0).transform.rotation);
            shoot_timer -= shootFreq;
        }
        shoot_timer += Time.deltaTime;
    }
    bool hitBorder(){
        const float XBORDER = 6.6f, YBORDER = 7.5f;
        return this.transform.position.x < -XBORDER || this.transform.position.x > XBORDER || 
                this.transform.position.y > YBORDER || this.transform.position.y < -YBORDER ;
    }
}
