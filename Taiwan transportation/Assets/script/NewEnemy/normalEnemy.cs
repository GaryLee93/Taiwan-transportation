using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalEnemy : MonoBehaviour{
    enum EnemyType{
        Scooter, Car
    }
    [SerializeField] EnemyType enemyType;
    [SerializeField] int health;
    protected float XBORDER, YBORDER;
    private void Start() {
        if(enemyType == EnemyType.Scooter){
            XBORDER = 6.1f;
            YBORDER = 7.1f;
        }
    }
    private void Update() {
        if(hitBorder()){
            gameObject.SetActive(false);
        }
    }
    public void takeDamage(int damage){
        health -= damage;
        if(health <= 0){
            health = 0;
            die();
        }
    }
    void die(){
        GameObject cb;
        for(int i=0; i<Random.Range(1, 5); i++){
            cb = Instantiate(StageObj.Collectables["score"], transform.position, transform.rotation);
            cb.transform.Translate(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            cb.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, Random.Range(3f, 5f));
        }

        for(int i=0; i<Random.Range(1, 5); i++){
            cb = Instantiate(StageObj.Collectables["score"], transform.position, transform.rotation);
            cb.transform.Translate(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            cb.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, Random.Range(3f, 5f));
        }
        
        Destroy(gameObject);
    }

    bool hitBorder(){
        return transform.position.x > XBORDER || transform.position.x < -XBORDER ||
                transform.position.y > YBORDER || transform.position.y < -YBORDER;
    }
}
