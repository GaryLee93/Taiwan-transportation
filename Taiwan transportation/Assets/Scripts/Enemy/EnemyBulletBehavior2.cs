using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehavior2 : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 b_SpeedVector = new Vector2(0f ,0f);
    public float angle_diff = 0f;   // -180 to 180 degree
    public int b_MoveType = 0;

    void Start(){
        rb=GetComponent<Rigidbody2D>();
        if(angle_diff!=0){
            angle_diff = Random.Range(-angle_diff, angle_diff);
            b_SpeedVector = rotateVector(b_SpeedVector, angle_diff);
        }

        if(b_MoveType == 1){    //linear move w/ b_MoveVector
            rb.velocity = b_SpeedVector;
        } 
        else if(b_MoveType == 2){   //aim at player w/ b_speed
            float b_speed = b_SpeedVector.magnitude;
            GameObject aim_target = GameObject.FindGameObjectWithTag("Player");
            Vector2 direction=(aim_target.transform.position-transform.position);
            direction.Normalize();
            rb.velocity = direction * b_speed;
        }
    }
    public Vector2 rotateVector(Vector2 vector, float angle){
        float rad = angle /180 *Mathf.PI;
        Vector2 newVector = vector; 
        float x = vector.x, y = vector.y;
        newVector.x = x*Mathf.Cos(rad) - y*Mathf.Sin(rad);
        newVector.y = x*Mathf.Sin(rad) + y*Mathf.Cos(rad);
        return newVector;
    }
}


