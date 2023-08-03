using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsNormalEnemy : MonoBehaviour
{
    [SerializeField] int health;
    
    public void takeDamage(int damage){
        health -= damage;
        if(health <= 0){
            health = 0;
            die();
        }
    }
    public void setHealth(int hp){
        this.health = hp;
    }
    public void summonDrop(int count, string type){
        GameObject cb;
        for(int i=0; i<count; i++){
            cb = Instantiate(StageObj.Collectables[type], transform.position, transform.rotation);
            cb.transform.Translate(Random.Range(-0.3f, 0.3f), 0, 0);
            cb.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(2f, 5f));
        }
    }
    public abstract void die();
    public abstract bool hitBorder();
    
}
