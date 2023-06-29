using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelMovement : MonoBehaviour
{
    public class fumoType{
        public int type;
        public Vector2 direction;
        public float time;
        public fumoType(int type, Vector2 direction, float time){
            this.type = type;
            this.direction = direction;
            this.time = time;
        }
    }
    public List<fumoType> moveList;
    public List<fumoType> accelList;
    Rigidbody2D rb;
    int move_index = 0;
    int accel_index = 0;
    
    void Start(){
        if(moveList == null){
            moveList = new List<fumoType>();
        }
        if(accelList == null){
            accelList = new List<fumoType>();
        }
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(start_accelerate());
        StartCoroutine(start_move());
        
    }
    IEnumerator start_move(){
        if(move_index < moveList.Count){
            int type = moveList[move_index].type;
            Vector2 direction = moveList[move_index].direction;
            float time = moveList[move_index].time;

            if(type == 0){
                yield return new WaitForSeconds(time);
            }
            else if(type == 1){
                rb.velocity = direction;
                yield return new WaitForSeconds(time);
            }
            else if(type == 2){
                Debug.Log("3sec");
                yield return new WaitForSeconds(time);
            }
            else if(type == 3){
                Debug.Log("5sec");
                yield return new WaitForSeconds(time);
            }
            
            
            if(type == -1)
                move_index = 0;
            else
                move_index++;
            
            StartCoroutine(start_move());
        }
    }

    IEnumerator start_accelerate(){
        if(accel_index < accelList.Count){
            int type = accelList[accel_index].type;
            Vector2 direction = accelList[accel_index].direction;
            float time = accelList[accel_index].time;

            if(type == 0){
                yield return new WaitForSeconds(time);
            }
            else if(type == 1){
                while(time > 0){
                    rb.velocity += direction;
                    time -= 0.1f;
                    yield return new WaitForSeconds(0.1f);
                }
                
            }
            else if(type == 2){
                Debug.Log("3sec");
                yield return new WaitForSeconds(time);
            }
            else if(type == 3){
                Debug.Log("5sec");
                yield return new WaitForSeconds(time);
            }

            if(type == -1)
                accel_index = 0;
            else
                accel_index++;

            StartCoroutine(start_accelerate());
        }
    }
    
}
