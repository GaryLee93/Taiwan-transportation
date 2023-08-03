using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class childPlane : MonoBehaviour
{
    [SerializeField] Vector2 initPos;
    [SerializeField] Vector2 endPos;
    [SerializeField] float rotateSpeed;
    enum cpType{
        Front, Back
    }
    [SerializeField] cpType cptype;
    Vector2 diffVector;
    float moveTime;
    float curTime;
    
    private void Start(){
        curTime = 0f;
        diffVector = endPos - initPos;
    }
    void Update(){
        if(moveTime == 0f){
            if(Input.GetKey(KeyCode.LeftShift))
                transform.localPosition = endPos;
            else transform.localPosition = initPos;
        }
        else{
            if(Input.GetKey(KeyCode.LeftShift)){
                curTime += Time.deltaTime;
                if(curTime >= moveTime)
                    curTime = moveTime;
            }
            else{
                curTime -= Time.deltaTime;
                if(curTime <= 0f) 
                    curTime = 0f;
            }
            transform.localPosition = initPos + (curTime/moveTime)*diffVector;
        }

        if(cptype == cpType.Back)
            transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }

    public void setPosition(Vector2 init, Vector2 end, float time){
        initPos = init;
        endPos = end;
        moveTime = time;
        transform.localPosition = init;
    }

    public void setRotate(float spin){
        rotateSpeed = spin;
    }
}
