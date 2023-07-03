using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour{
    public float speed;
    public float slow_speed;
    public GameObject check_point;
    public GameObject childPlane_0,childPlane_1,childPlane_2,childPlane_3;
    int power;
    int remain_life;
    int bomb_count;
    int score;
    Rigidbody2D rb;
    [SerializeField] Text score_text;
    [SerializeField] Text power_text;
    [SerializeField] Text remain_life_text;

    public static Player instance;
    private void Awake(){
        instance = this;
    }

    public static GameObject getPlayer(){
        return instance.gameObject;
    }
    
    void Start(){
        rb = GetComponent<Rigidbody2D>();

        remain_life = 4;
        power = 0;
        bomb_count = 3;
        score = 0;

        speed = 10;
        slow_speed = 3;

        refreshScoreText();
        refreshPowerText();
        refreshLifeText();
    }
    void Update(){
        normal_mod();
        //grazeDetect.transform.position = gameObject.transform.position;
    }

    void move(float tem_speed){
        float hori=Input.GetAxisRaw("Horizontal");
        float ver=Input.GetAxisRaw("Vertical");
        if(hori!=0 && ver!=0){
            hori/=Mathf.Sqrt(2);
            ver/=Mathf.Sqrt(2);
        }
        rb.velocity = new Vector2(hori*tem_speed, ver*tem_speed);
    }
    void normal_mod(){
        if(Input.GetKey(KeyCode.RightShift)||Input.GetKey(KeyCode.LeftShift)){
            check_point.SetActive(true);
            childPlane_0.transform.localPosition= new Vector3(0.75f,-0.75f,0f);
            childPlane_1.transform.localPosition= new Vector3(-0.75f,-0.75f,0);
            childPlane_2.transform.localPosition= new Vector3(1.25f,-0.25f,0);
            childPlane_3.transform.localPosition= new Vector3(-1.25f,-0.25f,0);
            move(slow_speed);
        }
        else{
            check_point.SetActive(false);
            childPlane_0.transform.localPosition= new Vector3(1f,-1f,0f);
            childPlane_1.transform.localPosition= new Vector3(-1f,-1f,0);
            childPlane_2.transform.localPosition= new Vector3(1.75f,-0.25f,0);
            childPlane_3.transform.localPosition= new Vector3(-1.75f,-0.25f,0);
            move(speed);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other){

        if(other.gameObject.tag=="enemy" || other.gameObject.tag=="enemy_bullet"){
            be_hit();
        }
        if(other.tag == "collectable"){
            Collectables cb = other.gameObject.GetComponent<Collectables>();
            
            if(cb.Type == Collectables.ColType.OneUP){
                remain_life++;
                refreshLifeText();
                Destroy(other.gameObject);
            }
            else if(cb.Type == Collectables.ColType.Bomb){
                bomb_count++;
                Destroy(other.gameObject);
            }
            else if(cb.Type == Collectables.ColType.Power){
                power ++;
                refreshPowerText();
                Destroy(other.gameObject);
            }
            else if(cb.Type == Collectables.ColType.Score){
                score++;
                refreshScoreText();
                Destroy(other.gameObject);
            }
        }
        
    }
    void be_hit(){
        Debug.Log("被彈");
        remain_life --;
        if(remain_life < 0){
            remain_life = 0;
            Debug.Log("滿身瘡痍");
        }
        refreshLifeText();
        

        this.power -= 100;
        if(this.power < 0){
            this.power = 0;
        }
        refreshPowerText();
    }

    void refreshScoreText(){
        score_text.text = "Score: " + score;
    }

    void refreshLifeText(){
        remain_life_text.text = "Life: " + remain_life;
    }

    void refreshPowerText(){
        power_text.text = "Power: " + power;
    }
}
