using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalScooter : AbsNormalEnemy
{
    enum MoveType{ TurnCircle, StopMove}
    MoveType moveType;
    float move_timer, turn_delay, turn_time, turn_radius, turn_angle;
    Vector2 init_speed, new_speed;
    float slow_time, hold_time;
    float shoot_timer;
    void Update(){
        if(hitBorder()){
            Destroy(gameObject);
        }
    }
    void FixedUpdate(){
        if(moveType == MoveType.TurnCircle){
            if(move_timer >= turn_delay && move_timer <= turn_delay +turn_time){
                Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                float circleTime = 2f*Mathf.PI *turn_radius /vel.magnitude;
                float angle = 360f*Time.fixedDeltaTime/circleTime;
                if(turn_angle < 0)
                    angle *= -1;
                Vector2 newVel = ourTool.rotate_vector(vel, angle);
                GetComponent<Rigidbody2D>().velocity = newVel;
            }
        }
        else if(moveType == MoveType.StopMove){
            if(move_timer <= slow_time){
                GetComponent<Rigidbody2D>().velocity = init_speed*(1 - move_timer/slow_time);
            }
            else if(move_timer <= slow_time+hold_time){
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
            else{
                GetComponent<Rigidbody2D>().velocity = new_speed;
            }
        }
        move_timer += Time.fixedDeltaTime;
    }
    public void setTurnCircle(Vector2 initVel, float radius, float delayTime, float angle){
        moveType = MoveType.TurnCircle;
        move_timer = 0;
        GetComponent<Rigidbody2D>().velocity = initVel;
        turn_delay = delayTime;
        turn_radius = radius;
        turn_angle = angle;
        turn_time = (2f*Mathf.PI *radius /initVel.magnitude) * Mathf.Abs(angle)/360f;
    }
    public void setStopMove(Vector2 initVel, float slowTime, float holdTime, Vector2 newVel){
        moveType = MoveType.StopMove;
        move_timer = 0;
        init_speed = initVel;
        slow_time = slowTime;
        hold_time = holdTime;
        new_speed = newVel;
    }
    public override void die(){
        summonDrop(5, "score");
        summonDrop(3, "power");
        Destroy(gameObject);
    }
   
    public override bool hitBorder(){
        float XBORDER = 6.1f, YBORDER = 7.1f;
        return transform.position.x > XBORDER || transform.position.x < -XBORDER ||
                transform.position.y > YBORDER || transform.position.y < -YBORDER;
    }    
}
