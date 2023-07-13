using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCar : AbsNormalEnemy
{
    float move_timer;
    private void Update(){
        if(hitBorder()){
            Destroy(gameObject);
        }
    }
    public override void die(){
        summonDrop(10, "score");
        summonDrop(7, "power");
        Destroy(gameObject);
    }
   
    public override bool hitBorder(){
        float XBORDER = 7f, YBORDER = 8f;
        return transform.position.x > XBORDER || transform.position.x < -XBORDER ||
                transform.position.y > YBORDER || transform.position.y < -YBORDER;
    }
}
