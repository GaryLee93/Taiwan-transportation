using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBorderCheck : MonoBehaviour
{
    private void Update() {
        if(hitBorder())
            Destroy(gameObject);
    }
    private bool hitBorder(){
        const float XBORDER = 6.7f, YBORDER = 7.5f;
        return this.transform.position.x <-XBORDER || this.transform.position.x > XBORDER || 
                this.transform.position.y > YBORDER || this.transform.position.y < -YBORDER ;
    }
}
