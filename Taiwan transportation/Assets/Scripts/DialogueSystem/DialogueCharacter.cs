using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueCharacter : MonoBehaviour
{
    public enum Direction{
        Left, Right
    }
    [SerializeField] Direction enterDirection;
    private bool isEntered = false;
    private bool isSpeaking = false;
    private Vector3 initPos;

    private void Awake(){
        initPos = transform.position;
        Color tmpcolor = GetComponent<Image>().color;
        tmpcolor.a = 0;
        GetComponent<Image>().color = tmpcolor;
        Speak(false);
    }
    public void Speak(bool speak){
        if(speak){
            isSpeaking = true;
            Color tmpcolor = new Color(1, 1, 1, 0);
            tmpcolor.a = GetComponent<Image>().color.a;
            GetComponent<Image>().color = tmpcolor;
        }
        else{
            isSpeaking = false;
            Color tmpcolor = new Color(0.3f, 0.3f, 0.3f, 0);
            tmpcolor.a = GetComponent<Image>().color.a;
            GetComponent<Image>().color = tmpcolor;
        }
    }
    public void Enter(){
        if(!isEntered){
            isEntered = true;
            StartCoroutine(enterField());
        }
        else
            Debug.LogWarning("This character("+ gameObject.name+ ")has already entered!");
    }
    IEnumerator enterField(){
        float timer = 0f, enterTime = 0.3f;
        while(timer < enterTime){
            timer += Time.deltaTime;
            if(timer >= enterTime){
                timer = enterTime;
            }

            if(enterDirection == Direction.Left){
                transform.position = initPos + new Vector3(-5f*(1f -timer/enterTime), 0, 0);
            }
            else if(enterDirection == Direction.Right){
                transform.position = initPos + new Vector3(5f*(1f -timer/enterTime), 0, 0);
            }

            Color tmpcolor = GetComponent<Image>().color;
            tmpcolor.a = timer/enterTime;
            GetComponent<Image>().color = tmpcolor;

            yield return null;
        }
    }
    public void Leave(){
        if(isEntered){
            isEntered = false;
            StartCoroutine(leaveField());   
        }
        else
            Debug.LogWarning("This character("+ gameObject.name+ ") is not on the field!");
    }
    IEnumerator leaveField(){
        float timer = 0f, leaveTime = 0.3f;
        while(timer < leaveTime){
            timer += Time.deltaTime;
            if(timer >= leaveTime){
                timer = leaveTime;
            }
            if(enterDirection == Direction.Left){
                transform.position = initPos + new Vector3(-5f*(timer/leaveTime), 0, 0);
            }
            else if(enterDirection == Direction.Right){
                transform.position = initPos + new Vector3(5f*(timer/leaveTime), 0, 0);
            }

            Color tmpcolor = GetComponent<Image>().color;
            tmpcolor.a = 1- timer/leaveTime;
            GetComponent<Image>().color = tmpcolor;
            yield return null;
        }
    }
    public bool IsEntered(){
        return isEntered;
    }
}
