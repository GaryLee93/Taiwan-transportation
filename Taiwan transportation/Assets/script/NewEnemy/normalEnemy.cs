using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalEnemy : MonoBehaviour{
    
    [SerializeField] GameObject[] collectables;
    public int health;
    public void takeDamage(int damage){
        health -= damage;
        if(health <= 0){
            health = 0;
            die();
        }
    }
    public void die(){
        GameObject cb;
        for(int i=0; i<Random.Range(1, 3); i++){
            cb = Instantiate(collectables[0], transform.position, transform.rotation);
            cb.transform.Translate(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            cb.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, Random.Range(3f, 5f));
        }

        for(int i=0; i<Random.Range(3, 6); i++){
            cb = Instantiate(collectables[1], transform.position, transform.rotation);
            cb.transform.Translate(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            cb.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, Random.Range(3f, 5f));
        }

        Destroy(gameObject);
    }
}
