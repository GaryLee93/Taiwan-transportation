using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    objectPooler instance;
    [SerializeField]GameObject bullet;
    [SerializeField]Rigidbody2D rb;
    [SerializeField]Sprite[] sprite;
    public int moveType = 0;
    public int accelType = 0;
    public int shootType = 0;
    public float shoot_angle_diff = 0;
    public Vector2 accelVector = new Vector2(0f, 0f);
    public Vector2 speedVector = new Vector2(0f, 0f);
    private float accelWaitTime = 1f;
    void Start(){
        instance=objectPooler.instance;

        if(moveType == 1)
            StartCoroutine(move1());    //linear move align speedVector
        else if(moveType == 2)
            StartCoroutine(move2());    //move then stop then move
        

        if(accelType == 1)
            StartCoroutine(accel1());   //wait then accelerate align accelVector
        else if(accelType == 2)
            StartCoroutine(accel2());   //wait then accelerate random horizontally 


        if(shootType==1)
            StartCoroutine(shoot1());   //shoot at b_SpeedVector (default:down) slowly
        else if(shootType==2)
            StartCoroutine(aiming_shoot());   //shoot at player slowly
        else if(shootType==3)
            StartCoroutine(circle_shoot(6));
    }
    void Update(){
        if(hitBorder())
            Destroy(gameObject);
        
        if(moveType == 3){
            transform.RotateAround(new Vector2(0f, 10f), Vector3.forward, -60 * Time.deltaTime);
            transform.Rotate(0, 0, 60*Time.deltaTime);
        }
    }

    IEnumerator move1(){
        rb.velocity = speedVector;
        yield return new WaitForSeconds(1f);
        float randnum = Random.Range(-0.1f, 0.1f);
    }
    IEnumerator move2(){
        rb.velocity = new Vector2(0f, -1f);
        yield return new WaitForSeconds(2f);

        rb.velocity = new Vector2(0f, 0f);
        yield return new WaitForSeconds(5f);
        
        rb.velocity = new Vector2(0f, -1f);
    }
    IEnumerator accel1(){
        yield return new WaitForSeconds(accelWaitTime);
        while(gameObject!=null){
            rb.velocity += accelVector;
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator accel2(){
        yield return new WaitForSeconds(accelWaitTime);
        float randnum = Random.Range(-0.1f, 0.1f);
        while(gameObject!=null){
            rb.velocity += new Vector2(randnum, 0f);
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator shoot1(){
        yield return new WaitForSeconds(0.5f);
        while(gameObject!=null){
            for(int i=0; i<3; i++){
                EnemyBulletBehavior2 newBullet = instance.spawnFromPool(bullet.name,transform.GetChild(0).transform.position,
                    transform.GetChild(0).transform.rotation, null).GetComponent<EnemyBulletBehavior2>();
                newBullet.b_MoveType = 1;
                newBullet.b_SpeedVector = new Vector2(0f, -3f);
                newBullet.angle_diff = shoot_angle_diff;
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(1.7f);
        }
    }
    IEnumerator aiming_shoot(){
        yield return new WaitForSeconds(0.5f);
        while(gameObject!=null){
            EnemyBulletBehavior2 newBullet = instance.spawnFromPool(bullet.name,transform.GetChild(0).transform.position,
                transform.GetChild(0).transform.rotation, null).GetComponent<EnemyBulletBehavior2>();
            newBullet.b_MoveType = 2;
            newBullet.b_SpeedVector = new Vector2(0f, -3f);
            newBullet.angle_diff = shoot_angle_diff;
            yield return new WaitForSeconds(2.3f);
        }
    }
    IEnumerator circle_shoot(int count){
        yield return new WaitForSeconds(0.5f);
        while(gameObject!=null){
            Vector2 direction = new Vector2(0f, -1f);
            for(int i=0; i<count; i++){
                EnemyBulletBehavior2 newBullet = instance.spawnFromPool(bullet.name,transform.GetChild(0).transform.position,
                transform.GetChild(0).transform.rotation, null).GetComponent<EnemyBulletBehavior2>();
                newBullet.b_MoveType = 1;
                newBullet.b_SpeedVector = direction;
                newBullet.angle_diff = shoot_angle_diff;
                direction = newBullet.GetComponent<EnemyBulletBehavior2>().rotateVector(direction, 360/count);
            }
            
            yield return new WaitForSeconds(2.3f);
        }
    }

    bool hitBorder(){
        const float XBORDER = 6.6f, YBORDER = 8f;
        return this.transform.position.x < -XBORDER || this.transform.position.x > XBORDER || 
                this.transform.position.y > YBORDER || this.transform.position.y < -YBORDER ;
    }
}
