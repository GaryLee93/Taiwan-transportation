using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour{
    
    [Header("要拉的東西")]
    [SerializeField] GameObject check_point;
    [SerializeField] GameObject backChildPlane;
    [SerializeField] GameObject frontChildPlane;
    [SerializeField] GameObject loudSpark;
    [SerializeField] Sprite middleSprite;
    [SerializeField] Sprite leftSprite;
    [SerializeField] Sprite rightSprite;

    [Header("各種屬性")]
    [SerializeField] PlayerData playerData;
    float current_speed;
    Rigidbody2D rb;
    bool isBombing;
    bool isRespawning;
    float respawnTimer;
    bool canShoot;
    GameObject bombObj = null;
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

        current_speed = playerData.normal_speed;
        shooterList = new List<GameObject>();

        transform.position = new Vector3(0, -5, 0);
        canShoot = true;
        
        changePowerMod();
        refreshScoreText();
        refreshPowerText();
        refreshLifeText();
        refreshBombText();
    }
    void Update(){
        player_move();

        if(Input.GetKey(KeyCode.X) && !isBombing){
            if(!isRespawning || isRespawning && respawnTimer >= 1.5f){
                loudSparkBomb();
            }
        }
        if(isBombing && bombObj == null)
            isBombing = false;
        
        if(isRespawning){
            if(respawnTimer >= 0.5f && respawnTimer < 1.5f){
                transform.Translate(0, 2.5f *Time.deltaTime, 0);
            }
            else if(respawnTimer >= 1.5f && respawnTimer < 5f){
                rb.simulated = true;
            }
            else if(respawnTimer >= 5f){
                isRespawning = false;
            }
            respawnTimer += Time.deltaTime;
        }
        
        if(playerData.power/100!=playerData.powerMode){
            playerData.powerMode = playerData.power/100;
            changePowerMod();
        }
    }
    
    void player_move(){
        if(Input.GetKey(KeyCode.LeftShift)){
            current_speed = playerData.slow_speed;
            changeVelocity();
        }
        else{
            if(isBombing){
                current_speed = playerData.slow_speed;
                changeVelocity();
            }
            else{
                current_speed = playerData.normal_speed;
                changeVelocity();
            }
        }
    }
    void changeVelocity(){
        float hori=Input.GetAxisRaw("Horizontal");
        float ver=Input.GetAxisRaw("Vertical");
        if(hori!=0 && ver!=0){
            hori/=Mathf.Sqrt(2);
            ver/=Mathf.Sqrt(2);
        }
        rb.velocity = new Vector2(hori *current_speed, ver *current_speed);

        if(hori ==0)
            GetComponent<SpriteRenderer>().sprite = middleSprite;
        else if(hori <0)
            GetComponent<SpriteRenderer>().sprite = leftSprite;
        else
            GetComponent<SpriteRenderer>().sprite = rightSprite;
    }
    void changePowerMod(){
        GameObject tmp_shooter;
        if(playerData.powerMode == 0){
            destroyAllShooter();
            for(int i=0; i<2; i++){
                tmp_shooter = Instantiate(frontChildPlane, transform);
                tmp_shooter.GetComponent<childPlane>().setPosition
                    (new Vector2(-0.3f +i*0.6f, 1f), new Vector2(-0.2f +i*0.4f, 1f), 0f);
                shooterList.Add(tmp_shooter);

                tmp_shooter = Instantiate(backChildPlane, transform);
                tmp_shooter.GetComponent<childPlane>().setPosition
                    (new Vector2(-0.8f +i*1.6f, -0.4f), new Vector2(-0.4f +i*0.8f, -0.4f), 0.15f);
                tmp_shooter.GetComponent<childPlane>().setRotate(-60 +120*i);
                shooterList.Add(tmp_shooter);
            }
        }
        else if(playerData.powerMode == 1){
            destroyAllShooter();
            for(int i=0; i<2; i++){
                tmp_shooter = Instantiate(frontChildPlane, transform);
                tmp_shooter.GetComponent<childPlane>().setPosition
                    (new Vector2(-0.3f +i*0.6f, 1f), new Vector2(-0.2f +i*0.4f, 1f), 0f);
                shooterList.Add(tmp_shooter);

                tmp_shooter = Instantiate(backChildPlane, transform);
                tmp_shooter.GetComponent<childPlane>().setPosition
                    (new Vector2(-0.8f +i*1.6f, -0.4f), new Vector2(-0.4f +i*0.8f, -0.4f), 0.15f);
                tmp_shooter.GetComponent<childPlane>().setRotate(-60 +120*i);
                shooterList.Add(tmp_shooter);
            }
            tmp_shooter = Instantiate(backChildPlane, transform);
            tmp_shooter.GetComponent<childPlane>().setPosition
                (new Vector2(0f, -0.9f), new Vector2(0f, -0.75f), 0.15f);
            tmp_shooter.GetComponent<childPlane>().setRotate(60f);
            shooterList.Add(tmp_shooter);
        }
        else if(playerData.powerMode == 2){
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
                    (new Vector2(-0.8f +i*1.6f, -0.4f), new Vector2(-0.4f +i*0.8f, -0.4f), 0.15f);
                tmp_shooter.GetComponent<childPlane>().setRotate(-60 +120*i);
                shooterList.Add(tmp_shooter);
            }
            tmp_shooter = Instantiate(backChildPlane, transform);
            tmp_shooter.GetComponent<childPlane>().setPosition
                (new Vector2(0f, -0.9f), new Vector2(0f, -0.75f), 0.15f);
            tmp_shooter.GetComponent<childPlane>().setRotate(60f);
            shooterList.Add(tmp_shooter);
        }
        else if(playerData.powerMode == 3){
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
                    (new Vector2(-0.8f +i*1.6f, -0.4f), new Vector2(-0.5f +i*1f, -0.4f), 0.15f);
                tmp_shooter.GetComponent<childPlane>().setRotate(-60 +120*i);
                shooterList.Add(tmp_shooter);

                tmp_shooter = Instantiate(backChildPlane, transform);
                tmp_shooter.GetComponent<childPlane>().setPosition
                    (new Vector2(-0.35f +i*0.7f, -0.8f), new Vector2(-0.2f +i*0.4f, -0.6f), 0.15f);
                tmp_shooter.GetComponent<childPlane>().setRotate(-60 +120*i);
                shooterList.Add(tmp_shooter);
            }
        }
        else if(playerData.powerMode == 4){
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
                    (new Vector2(-0.8f +i*1.6f, -0.4f), new Vector2(-0.5f +i*1f, -0.4f), 0.15f);
                tmp_shooter.GetComponent<childPlane>().setRotate(-60 +120*i);
                shooterList.Add(tmp_shooter);

                tmp_shooter = Instantiate(backChildPlane, transform);
                tmp_shooter.GetComponent<childPlane>().setPosition
                    (new Vector2(-0.35f +i*0.7f, -0.8f), new Vector2(-0.2f +i*0.4f, -0.6f), 0.15f);
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
            if(!isBombing && !isRespawning)
                be_hit();
        }
        if(other.tag == "collectable"){
            Collectables cb = other.gameObject.GetComponent<Collectables>();
            
            if(cb.Type == Collectables.ColType.OneUP){
                playerData.remain_life++;
                refreshLifeText();
                Destroy(other.gameObject);
            }
            else if(cb.Type == Collectables.ColType.Bomb){
                playerData.bomb_count++;
                refreshBombText();
                Destroy(other.gameObject);
            }
            else if(cb.Type == Collectables.ColType.Power){
                playerData.power +=2;
                if(playerData.power > 400){
                    playerData.power = 400;
                }
                int newPowerMode = playerData.power /100;

                if(newPowerMode != playerData.powerMode){
                    playerData.powerMode = newPowerMode;
                    changePowerMod();
                }
                refreshPowerText();
                Destroy(other.gameObject);
            }
            else if(cb.Type == Collectables.ColType.BigPower){
                playerData.power +=50;
                if(playerData.power > 400){
                    playerData.power = 400;
                }
                int newPowerMode = playerData.power /100;

                if(newPowerMode != playerData.powerMode){
                    playerData.powerMode = newPowerMode;
                    changePowerMod();
                }
                refreshPowerText();
                Destroy(other.gameObject);
            }
            else if(cb.Type == Collectables.ColType.Score){
                playerData.score++;
                refreshScoreText();
                Destroy(other.gameObject);
            }
        }
        
    }
    void be_hit(){
        Debug.Log("被彈");
        this.playerData.power -= 100;
        if(this.playerData.power < 0){
            this.playerData.power = 0;
        }
        playerData.powerMode = this.playerData.power /100;
        changePowerMod();
        refreshPowerText();

        playerData.bomb_count = 3;
        refreshBombText();

        playerData.remain_life --;
        GameObject tmp;
        for(int i=0; i<25; i++){
            tmp = Instantiate(StageObj.Collectables["power"], transform.position, transform.rotation);
            tmp.GetComponent<Rigidbody2D>().velocity = Random.Range(6f, 6.5f) * 
                new Vector2(Random.Range(-0.3f, 0.3f), 1).normalized;
            if(transform.position.x > 3.5f){
                tmp.GetComponent<Rigidbody2D>().velocity += new Vector2(-3f, 0f);
            }
            else if(transform.position.x < -3.5f){
                tmp.GetComponent<Rigidbody2D>().velocity += new Vector2(3f, 0f);
            }
        }
        if(playerData.remain_life < 0){
            playerData.remain_life = 0;
            Debug.Log("滿身瘡痍");
            StartCoroutine(respawn());
        }
        else{
            StartCoroutine(respawn());
        }
        refreshLifeText();
    }
    IEnumerator respawn(){
        rb.simulated = false;
        isRespawning = true;
        respawnTimer = 0;
        yield return new WaitForSeconds(0.5f);
        transform.position = new Vector3(0, -7.5f, 0);
        
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("enemy_bullet");
        foreach(GameObject b in bullets){
            EnemyBullet eb = b.GetComponent<EnemyBullet>();
            if(eb == null)
                Destroy(b);
            else
                eb.poolDespawn();
        }
    }

    void loudSparkBomb(){
        if(playerData.bomb_count >0){
            playerData.bomb_count --;
            refreshBombText();
            GameObject tmpobj = Instantiate(loudSpark, transform);
            tmpobj.transform.localPosition = new Vector3(0, 7, 0);
            bombObj = tmpobj;
            isBombing = true;
        }
    }
    void refreshScoreText(){
        StageObj.StageTexts["score"].GetComponent<Text>().text = "Score: " + playerData.score;
    }

    void refreshLifeText()  {
        StageObj.StageTexts["remainlife"].GetComponent<Text>().text = "Life: " + playerData.remain_life;
    }

    void refreshPowerText(){
        StageObj.StageTexts["power"].GetComponent<Text>().text = "Power: " + playerData.power;
    }
    void refreshBombText(){
        StageObj.StageTexts["bomb"].GetComponent<Text>().text = "Bomb: " + playerData.bomb_count;
    }
}
