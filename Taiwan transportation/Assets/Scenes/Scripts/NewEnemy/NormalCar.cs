using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCar : AbsNormalEnemy
{
    enum MoveType{Straight, TurnCircle, StopMove}
    enum ShootType{Circle, Sector}
    MoveType moveType;
    ShootType shootType;
    float move_timer, turn_delay, turn_time, turn_radius, turn_angle;
    Vector2 init_speed, new_speed;
    float slow_time, hold_time;
    EnemyBulletShooter shooter;
    
    public void setStraightMove(Vector2 vel){
        GetComponent<Rigidbody2D>().velocity = vel;
    }
    private void Update(){
        if(hitBorder()){
            Destroy(gameObject);
        }
        if(moveType == MoveType.StopMove){
            if(move_timer <= slow_time){
                GetComponent<Rigidbody2D>().velocity = init_speed*(1 - move_timer/slow_time);
            }
            else if(move_timer <= slow_time+hold_time){
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
            else if(move_timer <= 2*slow_time+hold_time){
                GetComponent<Rigidbody2D>().velocity = init_speed*(move_timer-slow_time-hold_time)/slow_time;
            }
            else{
                GetComponent<Rigidbody2D>().velocity = new_speed;
            }
        }
        move_timer += Time.fixedDeltaTime;
    }
    public void setStopMove(Vector2 initVel, float slowTime, float holdTime, Vector2 newVel){
        moveType = MoveType.StopMove;
        move_timer = 0;
        init_speed = initVel;
        slow_time = slowTime;
        hold_time = holdTime;
        new_speed = newVel;
    }
    public void setShootCircle(Vector2 direction, int count, bool aim, float delay, float interval, int number){
        StartCoroutine(startShootCircle(direction, count, aim, delay, interval, number));
    }
    IEnumerator startShootCircle(Vector2 direction, int count, bool aim, float delay, float interval, int number){
        shooter = GetComponentInChildren<EnemyBulletShooter>();
        yield return new WaitForSeconds(delay);
        YieldInstruction wtime = new WaitForSeconds(interval);

        if(number <0){
            while(true){
                shooter.shoot_circle_bullet(direction, count, aim);
                number --;
                yield return wtime;
            }
        }
        else{
            while(number>0){
                shooter.shoot_circle_bullet(direction, count, aim);
                number --;
                yield return wtime;
            }
        }
    }
    public void setShootSector(Vector2 direction, int count, float angle, bool aim, float delay, float interval, int number){
        StartCoroutine(startShootSector(direction, count, angle, aim, delay, interval, number));
    }
    IEnumerator startShootSector(Vector2 direction, int count, float angle, bool aim, float delay, float interval, int number){
        shooter = GetComponentInChildren<EnemyBulletShooter>();
        yield return new WaitForSeconds(delay);
        YieldInstruction wtime = new WaitForSeconds(interval);

        if(number <0){
            while(true){
                shooter.shoot_sector_bullet(direction, count, angle, aim);
                number --;
                yield return wtime;
            }
        }
        else{
            while(number>0){
                shooter.shoot_sector_bullet(direction, count, angle, aim);
                number --;
                yield return wtime;
            }
        }
    }
    public override void die(){
        summonDrop(Random.Range(3, 5), "score");
        summonDrop(Random.Range(3, 5), "power");
        Destroy(gameObject);
    }
   
    public override bool hitBorder(){
        float XBORDER = 7f, YBORDER = 8f;
        return transform.position.x > XBORDER || transform.position.x < -XBORDER ||
                transform.position.y > YBORDER || transform.position.y < -YBORDER;
    }
}
