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
    [SerializeField] float turnSpeed;
    AudioSource hitEnemy;
    private void Start() {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,bulletSpeed);
        hitEnemy = Player.GetPlayer().GetComponent<AudioSource>();
    }
    private void Update(){
        if(hitBorder())
            poolDespawn();
    }
    private void FixedUpdate(){
        if(bulletType == BType.Homing){
            GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
            GameObject target = find_closest_enemy();
            if(target != null){
                Vector2 direction = target.transform.position - transform.position;
                float angularV = Vector3.Cross(GetComponent<Rigidbody2D>().velocity.normalized, direction.normalized).z;
                angularV *= turnSpeed;
                GetComponent<Rigidbody2D>().angularVelocity = angularV;
            }
            else
                GetComponent<Rigidbody2D>().angularVelocity = 0;
        }
    }
    public void onBulletSpawn(){
        if(bulletType == BType.Straight)
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,bulletSpeed);
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "enemy"){
            other.gameObject.GetComponent<AbsNormalEnemy>().takeDamage(bulletDamage);
            if(this.bulletType == BType.Straight && hitEnemy!=null)
                hitEnemy.Play();
            poolDespawn();
        }
        if(other.gameObject.tag == "boss")
        {
            abstractBoss hitBoss = other.GetComponent<abstractBoss>();
            if(hitBoss==null)
            {
                Debug.Log("shit");
                return;
            }
            hitBoss.takeDamage(bulletDamage);
            if(this.bulletType == BType.Straight && hitEnemy!=null)
                hitEnemy.Play();
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
        if(closest_target==null)
        {
            targets = GameObject.FindGameObjectsWithTag("boss");
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
        }
        return closest_target;
    }
    private bool hitBorder(){
        const float XBORDER = 6.7f, YBORDER = 7.5f;
        return this.transform.position.x <-XBORDER || this.transform.position.x > XBORDER || 
                this.transform.position.y > YBORDER || this.transform.position.y < -YBORDER ;
    }
}
