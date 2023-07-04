using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooterModel : MonoBehaviour
{
    objectPooler instance;
    [SerializeField] GameObject bullet;
    public float start_time;
    public float end_time;
    public int shoot_type;
    public float shoot_angle_diff;
    public Vector2 shoot_direction;
    public float shoot_interval;
    public int shoot_count;
    public float sector_angle;
    public bool shoot_at_player;
    float s_timer;
    void Start(){
        s_timer = 0;
        instance=objectPooler.instance;
        if(shoot_type == 1){
            StartCoroutine(circle_shoot());
        }
        else if(shoot_type == 2){
            StartCoroutine(sector_shoot());
        }
        
    }

    IEnumerator circle_shoot(){
        s_timer += start_time;
        yield return new WaitForSeconds(start_time);
        while(end_time == -1 || s_timer <= end_time){
            shoot_circle_bullet();
            s_timer += shoot_interval;
            yield return new WaitForSeconds(shoot_interval);
        }
    }

    IEnumerator sector_shoot(){
        s_timer += start_time;
        yield return new WaitForSeconds(start_time);
        while(end_time == -1 || s_timer <= end_time){
            shoot_sector_bullet();
            s_timer += shoot_interval;
            yield return new WaitForSeconds(shoot_interval);
        }
    }

    public void shoot_circle_bullet(){/*
        Vector2 tmp_direction;
        if(shoot_at_player == true){
            tmp_direction = aiming_player();
        }
        else{
            tmp_direction = shoot_direction;
        }
        
        for(int i=0; i<shoot_count; i++){
            ModelMovement newBullet = instance.spawnFromPool(bullet.name,
                transform.position,transform.rotation, null).GetComponent<ModelMovement>();
            
            newBullet.moveList = new List<ModelMovement.fumoType>();
            newBullet.moveList.Add(new ModelMovement.fumoType(1, rotateVector(tmp_direction, Random.Range(-shoot_angle_diff, shoot_angle_diff)), 1));
            tmp_direction = rotateVector(tmp_direction, 360/shoot_count);
        }*/
    }

    public void shoot_sector_bullet(){/*
        if(shoot_count > 1){
            Vector2 tmp_direction;
            if(shoot_at_player == true){
                tmp_direction = aiming_player();
            }
            else{
                tmp_direction = rotateVector(shoot_direction, -sector_angle);
            }
            for(int i=0; i<shoot_count; i++){
                ModelMovement newBullet = instance.spawnFromPool(bullet.name,
                    transform.position,transform.rotation, null).GetComponent<ModelMovement>();

                newBullet.moveList = new List<ModelMovement.fumoType>();
                newBullet.moveList.Add(new ModelMovement.fumoType(1, rotateVector(tmp_direction, Random.Range(-shoot_angle_diff, shoot_angle_diff)), 1));
                tmp_direction = rotateVector(tmp_direction, 2*sector_angle/(shoot_count-1));
            }
        }
        else{
            Vector2 tmp_direction;
            if(shoot_at_player == true){
                tmp_direction = aiming_player();
            }
            else{
                tmp_direction = rotateVector(shoot_direction, Random.Range(-shoot_angle_diff, shoot_angle_diff));
            }

            ModelMovement newBullet = instance.spawnFromPool(bullet.name,
                transform.position,transform.rotation, null).GetComponent<ModelMovement>();
            
            newBullet.moveList = new List<ModelMovement.fumoType>();
            newBullet.moveList.Add(new ModelMovement.fumoType(1, rotateVector(tmp_direction, Random.Range(-shoot_angle_diff, shoot_angle_diff)), 1));
        }*/
    }
    

    public Vector2 aiming_player(){
        return GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
    }
    Vector2 rotateVector(Vector2 vector, float angle){
        float rad = angle /180 *Mathf.PI;
        float x = vector.x, y = vector.y;
        float a = x*Mathf.Cos(rad) - y*Mathf.Sin(rad);
        float b = x*Mathf.Sin(rad) + y*Mathf.Cos(rad);
        return new Vector2(a, b);
    }
}
