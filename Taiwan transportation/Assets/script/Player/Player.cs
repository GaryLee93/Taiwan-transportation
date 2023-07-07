using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour{
    
    [Header("要拉的東西")]
    [SerializeField] GameObject check_point;
    [SerializeField] GameObject backChildPlane;
    [SerializeField] GameObject frontChildPlane;
    [SerializeField] Sprite middleSprite;
    [SerializeField] Sprite leftSprite;
    [SerializeField] Sprite rightSprite;

    [Header("各種屬性")]
    [SerializeField] int power = 0;
    [SerializeField] int powerMode = 0;
    [SerializeField] float move_speed = 10f;
    [SerializeField] float slow_speed = 3f;
    [SerializeField] int remain_life = 3;
    [SerializeField] int bomb_count = 3;
    [SerializeField] int score = 0;
    Rigidbody2D rb;
    int lastPM;
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

        lastPM = 0;
        shooterList = new List<GameObject>();
        
        changePowerMod();
        refreshScoreText();
        refreshPowerText();
        refreshLifeText();
    }
    void Update(){
        player_move();

        if(lastPM!=powerMode){
            changePowerMod();
        }

        lastPM = powerMode;

    }
    void player_move(){
        if(Input.GetKey(KeyCode.RightShift)||Input.GetKey(KeyCode.LeftShift)){
            check_point.SetActive(true);
            float hori=Input.GetAxisRaw("Horizontal");
            float ver=Input.GetAxisRaw("Vertical");
            if(hori!=0 && ver!=0){
                hori/=Mathf.Sqrt(2);
                ver/=Mathf.Sqrt(2);
            }
            rb.velocity = new Vector2(hori *slow_speed, ver *slow_speed);

            if(hori >0){
                GetComponent<SpriteRenderer>().sprite = rightSprite;
            }
            else if(hori <0){
                GetComponent<SpriteRenderer>().sprite = leftSprite;
            }
            else{
                GetComponent<SpriteRenderer>().sprite = middleSprite;
            }
        }
        else{
            check_point.SetActive(false);
            float hori=Input.GetAxisRaw("Horizontal");
            float ver=Input.GetAxisRaw("Vertical");
            if(hori!=0 && ver!=0){
                hori/=Mathf.Sqrt(2);
                ver/=Mathf.Sqrt(2);
            }
            rb.velocity = new Vector2(hori *move_speed, ver *move_speed);

            if(hori >0){
                GetComponent<SpriteRenderer>().sprite = rightSprite;
            }
            else if(hori <0){
                GetComponent<SpriteRenderer>().sprite = leftSprite;
            }
            else{
                GetComponent<SpriteRenderer>().sprite = middleSprite;
            }
        }
    }
    void changePowerMod(){
        GameObject tmp_shooter;
        if(powerMode == 0){
            destroyAllShooter();
            for(int i=0; i<2; i++){
                tmp_shooter = Instantiate(frontChildPlane, transform);
                tmp_shooter.GetComponent<childPlane>().setPosition
                    (new Vector2(-0.3f +i*0.6f, 1f), new Vector2(-0.2f +i*0.4f, 1f), 0f);
                shooterList.Add(tmp_shooter);
            }
        }
        else if(powerMode == 1){
            destroyAllShooter();
            for(int i=0; i<2; i++){
                tmp_shooter = Instantiate(frontChildPlane, transform);
                tmp_shooter.GetComponent<childPlane>().setPosition
                    (new Vector2(-0.3f +i*0.6f, 1f), new Vector2(-0.2f +i*0.4f, 1f), 0f);
                shooterList.Add(tmp_shooter);

                tmp_shooter = Instantiate(backChildPlane, transform);
                tmp_shooter.GetComponent<childPlane>().setPosition
                    (new Vector2(-1f +i*2f, -0.3f), new Vector2(-0.5f +i*1f, -0.5f), 0.15f);
                tmp_shooter.GetComponent<childPlane>().setRotate(-60 +120*i);
                shooterList.Add(tmp_shooter);
            }
        }
        else if(powerMode == 2){
            destroyAllShooter();
            tmp_shooter = Instantiate(frontChildPlane, transform);
                tmp_shooter.GetComponent<childPlane>().setPosition
                    (new Vector2(0, 1), new Vector2(0, 1), 0);
                shooterList.Add(tmp_shooter);

            for(int i=0; i<2; i++){
                tmp_shooter = Instantiate(frontChildPlane, transform);
                tmp_shooter.GetComponent<childPlane>().setPosition
                    (new Vector2(-0.5f +i*1f, 0.7f), new Vector2(-0.3f +i*0.6f, 0.7f), 0f);
                shooterList.Add(tmp_shooter);

                tmp_shooter = Instantiate(backChildPlane, transform);
                tmp_shooter.GetComponent<childPlane>().setPosition
                    (new Vector2(-1f +i*2f, -0.3f), new Vector2(-0.5f +i*1f, -0.5f), 0.15f);
                tmp_shooter.GetComponent<childPlane>().setRotate(-60 +120*i);
                shooterList.Add(tmp_shooter);
            }
        }
        else if(powerMode == 3){
            destroyAllShooter();
            tmp_shooter = Instantiate(frontChildPlane, transform);
                tmp_shooter.GetComponent<childPlane>().setPosition
                    (new Vector2(0, 1), new Vector2(0, 1), 0);
                shooterList.Add(tmp_shooter);

            for(int i=0; i<2; i++){
                tmp_shooter = Instantiate(frontChildPlane, transform);
                tmp_shooter.GetComponent<childPlane>().setPosition
                    (new Vector2(-0.5f +i*1f, 0.7f), new Vector2(-0.3f +i*0.6f, 0.7f), 0f);
                shooterList.Add(tmp_shooter);

                tmp_shooter = Instantiate(backChildPlane, transform);
                tmp_shooter.GetComponent<childPlane>().setPosition
                    (new Vector2(-1f +i*2f, -0.3f), new Vector2(-0.5f +i*1f, -0.35f), 0.15f);
                tmp_shooter.GetComponent<childPlane>().setRotate(-60 +120*i);
                shooterList.Add(tmp_shooter);

                tmp_shooter = Instantiate(backChildPlane, transform);
                tmp_shooter.GetComponent<childPlane>().setPosition
                    (new Vector2(-0.4f +i*0.8f, -0.9f), new Vector2(-0.2f +i*0.4f, -0.6f), 0.15f);
                tmp_shooter.GetComponent<childPlane>().setRotate(-60 +120*i);
                shooterList.Add(tmp_shooter);
            }
        }
        else if(powerMode == 4){
            destroyAllShooter();
            for(int i=0; i<2; i++){
                tmp_shooter = Instantiate(frontChildPlane, transform);
                tmp_shooter.GetComponent<childPlane>().setPosition
                    (new Vector2(-0.2f +i*0.4f, 1f), new Vector2(-0.1f +i*0.2f, 1f), 0f);
                shooterList.Add(tmp_shooter);

                tmp_shooter = Instantiate(frontChildPlane, transform);
                tmp_shooter.GetComponent<childPlane>().setPosition
                    (new Vector2(-0.6f +i*1.2f, 0.7f), new Vector2(-0.3f +i*0.6f, 0.7f), 0f);
                shooterList.Add(tmp_shooter);

                tmp_shooter = Instantiate(backChildPlane, transform);
                tmp_shooter.GetComponent<childPlane>().setPosition
                    (new Vector2(-1f +i*2f, -0.3f), new Vector2(-0.5f +i*1f, -0.35f), 0.15f);
                tmp_shooter.GetComponent<childPlane>().setRotate(-60 +120*i);
                shooterList.Add(tmp_shooter);

                tmp_shooter = Instantiate(backChildPlane, transform);
                tmp_shooter.GetComponent<childPlane>().setPosition
                    (new Vector2(-0.4f +i*0.8f, -0.9f), new Vector2(-0.2f +i*0.4f, -0.6f), 0.15f);
                tmp_shooter.GetComponent<childPlane>().setRotate(-60 +120*i);
                shooterList.Add(tmp_shooter);
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

                if(newPowerMode != powerMode){
                    powerMode = newPowerMode;
                    changePowerMod();
                }
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
        powerMode = this.power /100;
        changePowerMod();
        refreshPowerText();
    }

    void refreshScoreText(){
        
        StageObj.StageTexts["score"].GetComponent<Text>().text = "Score: " + score;
    }

    void refreshLifeText(){
        StageObj.StageTexts["remainlife"].GetComponent<Text>().text = "Life: " + remain_life;
    }

    void refreshPowerText(){
        StageObj.StageTexts["power"].GetComponent<Text>().text = "Power: " + power;
    }
}
