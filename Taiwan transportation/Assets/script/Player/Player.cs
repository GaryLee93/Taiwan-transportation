using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour{
    [SerializeField] GameObject check_point;
    [SerializeField] GameObject childPlane;
    [SerializeField] GameObject wrenchShooter;
    [SerializeField] int power;
    [SerializeField] int powerMode;
    [SerializeField] Text score_text;
    [SerializeField] Text power_text;
    [SerializeField] Text remain_life_text;
    float move_speed;
    float slow_speed;
    int remain_life;
    int bomb_count;
    int score;
    Rigidbody2D rb;
    
    List<GameObject> shooterList;

    public static Player instance;
    private void Awake(){
        instance = this;
    }

    public static GameObject GetPlayer(){
        return instance.gameObject;
    }
    
    void Start(){
        rb = GetComponent<Rigidbody2D>();

        remain_life = 4;
        power = 0;
        bomb_count = 3;
        score = 0;

        move_speed = 10;
        slow_speed = 3;

        shooterList = new List<GameObject>();
        
        powerMode = -1;
        
        changePowerMod(0);
        refreshScoreText();
        refreshPowerText();
        refreshLifeText();
    }
    void Update(){
        normal_mod();        
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
            move(slow_speed);
        }
        else{
            check_point.SetActive(false);
            move(move_speed);
        }
    }
    void changePowerMod(int newPowerMode){
        if(newPowerMode == powerMode){
            return;
        }
        else{
            powerMode = newPowerMode;
            GameObject tmp_shooter;
            if(powerMode == 0){
                destroyAllShooter();
                for(int i=0; i<2; i++){
                    tmp_shooter = Instantiate(wrenchShooter, transform);
                    tmp_shooter.transform.localPosition = new Vector3(-0.4f + i*0.8f, 1f, 0f);
                    shooterList.Add(tmp_shooter);
                }
            }
            else if(powerMode == 1){
                destroyAllShooter();
                for(int i=0; i<2; i++){
                    tmp_shooter = Instantiate(wrenchShooter, transform);
                    tmp_shooter.transform.localPosition = new Vector3(-0.4f + i*0.8f, 1f, 0f);
                    shooterList.Add(tmp_shooter);
                }

                for(int i=0; i<2; i++){
                    tmp_shooter = Instantiate(childPlane, transform);
                    tmp_shooter.transform.localPosition = new Vector3(-1.5f + i*3f, -1f, 0f);
                    shooterList.Add(tmp_shooter);
                }
            }
            else if(powerMode == 2){
                destroyAllShooter();
                tmp_shooter = Instantiate(wrenchShooter, transform);
                tmp_shooter.transform.localPosition = new Vector3(0, 1.2f, 0f);
                shooterList.Add(tmp_shooter);
                for(int i=0; i<2; i++){
                    tmp_shooter = Instantiate(wrenchShooter, transform);
                    tmp_shooter.transform.localPosition = new Vector3(-0.7f + i*1.4f, 1f, 0f);
                    shooterList.Add(tmp_shooter);
                }
                for(int i=0; i<2; i++){
                    tmp_shooter = Instantiate(childPlane, transform);
                    tmp_shooter.transform.localPosition = new Vector3(-1.5f + i*3f, -1f, 0f);
                    shooterList.Add(tmp_shooter);
                }
            }
            else if(powerMode == 3){
                destroyAllShooter();
                tmp_shooter = Instantiate(wrenchShooter, transform);
                tmp_shooter.transform.localPosition = new Vector3(0, 1.2f, 0f);
                shooterList.Add(tmp_shooter);
                
                for(int i=0; i<2; i++){
                    tmp_shooter = Instantiate(wrenchShooter, transform);
                    tmp_shooter.transform.localPosition = new Vector3(-0.7f + i*1.4f, 1f, 0f);
                    shooterList.Add(tmp_shooter);
                }
                for(int i=0; i<2; i++){
                    tmp_shooter = Instantiate(childPlane, transform);
                    tmp_shooter.transform.localPosition = new Vector3(-1.5f + i*3f, -1f, 0f);
                    shooterList.Add(tmp_shooter);
                }
                for(int i=0; i<2; i++){
                    tmp_shooter = Instantiate(childPlane, transform);
                    tmp_shooter.transform.localPosition = new Vector3(-2f + i*4f, -0.2f, 0f);
                    shooterList.Add(tmp_shooter);
                }
            }
            else if(powerMode == 4){
                destroyAllShooter();
                for(int i=0; i<2; i++){
                    tmp_shooter = Instantiate(wrenchShooter, transform);
                    tmp_shooter.transform.localPosition = new Vector3(-0.3f + i*0.6f, 1f, 0f);
                    shooterList.Add(tmp_shooter);
                }
                for(int i=0; i<2; i++){
                    tmp_shooter = Instantiate(wrenchShooter, transform);
                    tmp_shooter.transform.localPosition = new Vector3(-0.9f + i*1.8f, 0.8f, 0f);
                    shooterList.Add(tmp_shooter);
                }
                for(int i=0; i<2; i++){
                    tmp_shooter = Instantiate(childPlane, transform);
                    tmp_shooter.transform.localPosition = new Vector3(-1.5f + i*3f, -1f, 0f);
                    shooterList.Add(tmp_shooter);
                }
                for(int i=0; i<2; i++){
                    tmp_shooter = Instantiate(childPlane, transform);
                    tmp_shooter.transform.localPosition = new Vector3(-2f + i*4f, -0.2f, 0f);
                    shooterList.Add(tmp_shooter);
                }
            }
        }
    }
    void destroyAllShooter(){
        for(int i=0; i<shooterList.Count; i++){
            Destroy(shooterList[i]);
        }
        shooterList = new List<GameObject>();
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
                if(power > 400){
                    power = 400;
                }
                int newPowerMode = power /100;

                changePowerMod(newPowerMode);
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
        changePowerMod(power /100);
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
