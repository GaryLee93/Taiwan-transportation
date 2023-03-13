using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehavior : MonoBehaviour
{
    private void Update() {
        if(hitBorder())
            Destroy(gameObject);
    }
    private bool hitBorder(){
        return this.transform.position.x <-8.7 || this.transform.position.x > 3 || 
                this.transform.position.y > 7 || this.transform.position.y < -7 ;
    }
}
