using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour{
    [SerializeField]GameObject bullet;
    public int shootType = 0; 
    public float shootInterval = 1;
    public float shoot_angle_diff = 0;
    public Vector2 shoot_direction;
    void Start() {
        
    }
    void Update(){
        if(hitBorder())
            Destroy(gameObject);
    }
    IEnumerator enemy_start(){
        yield return new WaitForSeconds(1f);
    }

    bool hitBorder(){
        const float XBORDER = 6.6f, YBORDER = 8f;
        return this.transform.position.x < -XBORDER || this.transform.position.x > XBORDER || 
                this.transform.position.y > YBORDER || this.transform.position.y < -YBORDER ;
    }
}
