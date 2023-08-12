using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;
public class Stage1 : MonoBehaviour
{
    [SerializeField] float summonTime;
    [SerializeField] TMP_Text time_text;
    [SerializeField] GameObject taxi;
    [SerializeField] GameObject MissBang;
    [SerializeField] GameObject Ending;
    [SerializeField] GameObject EndingSpin;
    [SerializeField] Background1 background;
    [SerializeField] DialogueSystem dsOne;
    [SerializeField] DialogueSystem dsTwo;
    [SerializeField] GameObject endButton;
    [SerializeField] AudioSource bangMusic;
    [SerializeField] AudioSource midMusic;
    PauseMenu pauseMenu;
    AudioSource nowPlaying;
    Player player;
    public float stageTimer;
    
    void Start(){
        
        Ending.GetComponent<VideoPlayer>().Prepare();
        Ending.GetComponent<VideoPlayer>().Stop();
        EndingSpin.GetComponent<VideoPlayer>().Prepare();
        endButton.SetActive(false);
        nowPlaying = midMusic;
        nowPlaying.Play();

        pauseMenu = PauseMenu.instance;
        player = Player.instance;
        stageTimer = 0;
        StartCoroutine(firstWave());
        background.start_taxi();
        background.display_title();
        StartCoroutine(walkchangefield());
        pauseMenu.pause += pause;
        pauseMenu.resume += resume;
    }
    void Update(){
        stageTimer += Time.deltaTime;
        if(stageTimer < 10){
            time_text.text = "00" + (int)stageTimer;
        }
        else if(stageTimer < 100){
            time_text.text = "0" + (int)stageTimer;
        }
        else{
            time_text.text = "" + (int)stageTimer;
        }

        if(Ending.GetComponent<SpriteRenderer>().enabled && (Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.Alpha0)))
        {
            Ending.GetComponent<SpriteRenderer>().enabled = false;
            Ending.GetComponent<AudioSource>().Pause();
            EndingSpin.GetComponent<SpriteRenderer>().enabled = true;
            EndingSpin.GetComponent<VideoPlayer>().Play();
        }
    }
    void resume()
    {
        if(nowPlaying !=null && !PauseMenu.gameIsPaused) nowPlaying.Play();
    }
    void pause()
    {
        if(nowPlaying != null && PauseMenu.gameIsPaused) nowPlaying.Pause();
    }
    IEnumerator firstWave(){
        yield return new WaitForSeconds(summonTime);
        GameObject enemy;
        YieldInstruction delayTime, delayOneSec;
        delayOneSec = new WaitForSeconds(1);

        delayTime = new WaitForSeconds(0.5f);
        for(int i=4; i>=0; i--){
            enemy = Instantiate(StageObj.Enemies["red_scooter"], new Vector2(-4 +i*2f , 7f), transform.rotation);
            enemy.GetComponent<S1Scooter>().setStopMove(new Vector2(0, -3), 1, 2, new Vector2(0, -2));
            enemy.GetComponent<S1Scooter>().setShootSector(new Vector2(0, -2f), 5, 60f, false, 1f, 1f, 1);
            yield return delayTime;
        }

        delayTime = new WaitForSeconds(0.3f);
        for(int i=0; i<5; i++){
            enemy = Instantiate(StageObj.Enemies["green_scooter"], new Vector2(-6f , 2f), transform.rotation);
            enemy.GetComponent<S1Scooter>().setStraightMove(new Vector2(5f, 0));
            enemy.GetComponent<S1Scooter>().setHealth(30);
            yield return delayTime;
        }

        delayTime = new WaitForSeconds(0.5f);
        for(int i=0; i<5; i++){
            enemy = Instantiate(StageObj.Enemies["blue_scooter"], new Vector2(4, 7f), transform.rotation);
            enemy.GetComponent<S1Scooter>().setTurnCircle(new Vector2(0, -3f), 2f, 0.5f, -90f);
            enemy.GetComponent<S1Scooter>().setShootCircle(
                ourTool.vectorToPlayer(enemy).normalized *3, 1, true, 0.3f+i*0.1f, 0.7f, 3);
            yield return delayTime;
        }
        yield return delayOneSec;

        delayTime = new WaitForSeconds(0.5f);
        for(int i=0; i<=4; i++){
            enemy = Instantiate(StageObj.Enemies["red_scooter"], new Vector2(-4 +i*2f , 7f), transform.rotation);
            enemy.GetComponent<S1Scooter>().setStopMove(new Vector2(0, -3), 1, 2, new Vector2(0, -2));
            enemy.GetComponent<S1Scooter>().setShootSector(new Vector2(0, -2f), 5, 60f, false, 1f, 1f, 1);
            yield return delayTime;
        }

        delayTime = new WaitForSeconds(0.3f);
        for(int i=0; i<5; i++){
            enemy = Instantiate(StageObj.Enemies["green_scooter"], new Vector2(6f , 2f), transform.rotation);
            enemy.GetComponent<S1Scooter>().setStraightMove(new Vector2(-5f, 0));
            enemy.GetComponent<S1Scooter>().setHealth(30);
            yield return delayTime;
        }
        
        delayTime = new WaitForSeconds(0.5f);
        for(int i=0; i<5; i++){
            enemy = Instantiate(StageObj.Enemies["blue_scooter"], new Vector2(-4, 7f), transform.rotation);
            enemy.GetComponent<S1Scooter>().setTurnCircle(new Vector2(0, -3f), 2f, 0.5f, 90f);
            enemy.GetComponent<S1Scooter>().setShootCircle(
                ourTool.vectorToPlayer(enemy).normalized *3, 1, true, 0.3f+i*0.1f, 0.7f, 3);
            yield return delayTime;
        }
        yield return delayOneSec;
        yield return delayOneSec;
        

        delayTime = new WaitForSeconds(0.5f);
        for(int i=0; i<5; i++){
            enemy = Instantiate(StageObj.Enemies["red_scooter"], new Vector2(-4 +i*2f , 7f), transform.rotation);
            enemy.GetComponent<S1Scooter>().setStraightMove(new Vector2(0, -4));
            enemy.GetComponent<S1Scooter>().setShootSector(new Vector2(0, -2f), 5, 100f, true, 0.5f, 1f, 1);
            yield return delayTime;
        }

        delayTime = new WaitForSeconds(0.3f);
        for(int i=0; i<5; i++){
            enemy = Instantiate(StageObj.Enemies["green_scooter"], new Vector2(-6f , 2f), transform.rotation);
            enemy.GetComponent<S1Scooter>().setStraightMove(new Vector2(5f, 0));
            enemy.GetComponent<S1Scooter>().setHealth(30);
            yield return delayTime;
        }

        delayTime = new WaitForSeconds(0.5f);
        for(int i=4; i>=0; i--){
            enemy = Instantiate(StageObj.Enemies["blue_scooter"], new Vector2(-4 +i*2f , 7f), transform.rotation);
            enemy.GetComponent<S1Scooter>().setStraightMove(new Vector2(0, -4));
            enemy.GetComponent<S1Scooter>().setShootSector(new Vector2(0, -2f), 5, 100f, true, 0.5f, 1f, 1);
            yield return delayTime;
        }

        delayTime = new WaitForSeconds(0.3f);
        for(int i=0; i<5; i++){
            enemy = Instantiate(StageObj.Enemies["green_scooter"], new Vector2(6f , 2f), transform.rotation);
            enemy.GetComponent<S1Scooter>().setStraightMove(new Vector2(-5f, 0));
            enemy.GetComponent<S1Scooter>().setHealth(30);
            yield return delayTime;
        }

        delayTime = new WaitForSeconds(2);
        for(int i=0; i<2; i++){
            enemy = Instantiate(StageObj.Enemies["red_car"], new Vector2(-3 +6*i, 7f), transform.rotation);
            enemy.GetComponent<S1Car>().setStraightMove(new Vector2(0, -2));
            enemy.GetComponent<S1Car>().setShootCircle(new Vector2(0, -1.5f), 11, true, 0.7f, 1f, 1);
        }
        yield return delayTime;

        delayTime = new WaitForSeconds(0.5f);
        for(int i=0; i<7; i++){
            enemy = Instantiate(StageObj.Enemies["blue_scooter"], new Vector2(4, 7f), transform.rotation);
            enemy.GetComponent<S1Scooter>().setTurnCircle(new Vector2(0, -3f), 2f, 0.5f, -90f);
            enemy.GetComponent<S1Scooter>().setShootCircle(
                ourTool.vectorToPlayer(enemy).normalized *3, 1, true, 0.5f, 1f +0.2f*i, 1);
            yield return delayTime;
            
            enemy = Instantiate(StageObj.Enemies["blue_scooter"], new Vector2(-4, 7f), transform.rotation);
            enemy.GetComponent<S1Scooter>().setTurnCircle(new Vector2(0, -3f), 2f, 0.5f, 90f);
            enemy.GetComponent<S1Scooter>().setShootCircle(
                ourTool.vectorToPlayer(enemy).normalized *3, 1, true, 0.5f, 1f +0.2f*i, 1);
            yield return delayTime;
        }
        while(stageTimer <= 40f){
            yield return null;
        }
        StartCoroutine(midBoss());
    }
    IEnumerator midBoss(){
        GameObject midBoss = Instantiate(taxi, new Vector3(-3f, 7.9f, 0), new Quaternion());
        midBoss.GetComponent<abstractBoss>().active();

        while(midBoss.GetComponent<abstractBoss>().isRun()){
            yield return null;
        }

        
        background.start_change();
        StartCoroutine(fillWave());

        while(stageTimer <= 70f){
            yield return null;
        }
        Destroy(midBoss);
        StartCoroutine(secondWave());
    }
    IEnumerator fillWave(){
        GameObject enemy;
        YieldInstruction delayTime;
        delayTime = new WaitForSeconds(1f);
        yield return delayTime;

        while(stageTimer <= 70f){
            enemy = Instantiate(StageObj.Enemies["green_scooter"], new Vector2(-6f , 4f), transform.rotation);
            enemy.GetComponent<S1Scooter>().setTurnCircle(new Vector2(4f, 0), 2f, 1f, -90f);
            enemy.GetComponent<S1Scooter>().setShootSector(new Vector2(0, -3), 3, 30f, true, 0.5f, 0.5f, 2);
            enemy.GetComponent<S1Scooter>().setHealth(50);
            yield return delayTime;

            if(stageTimer > 70f)
                break;
            
            enemy = Instantiate(StageObj.Enemies["green_scooter"], new Vector2(6f , 4f), transform.rotation);
            enemy.GetComponent<S1Scooter>().setTurnCircle(new Vector2(-4f, 0), 2f, 1f, 90f);
            enemy.GetComponent<S1Scooter>().setShootSector(new Vector2(0, -3), 3, 30f, true, 0.5f, 0.5f, 2);
            enemy.GetComponent<S1Scooter>().setHealth(50);
            yield return delayTime;
        }
    }
    IEnumerator secondWave(){
        GameObject enemy;
        YieldInstruction delayTime, delayOneSec;
        delayOneSec = new WaitForSeconds(1);

        delayTime = new WaitForSeconds(1f);
        for(int i=0; i<3; i++){
            enemy = Instantiate(StageObj.Enemies["red_scooter"], new Vector2(2 +i*1f , 7f), transform.rotation);
            enemy.GetComponent<S1Scooter>().setStopMove(new Vector2(0, -3), 1, 2, new Vector2(1, -2));
            enemy.GetComponent<S1Scooter>().setShootSector(new Vector2(0, -2f), 1, 10f, true, 1f, 0.2f, 3);

            enemy = Instantiate(StageObj.Enemies["red_scooter"], new Vector2(-2 -i*1f , 7f), transform.rotation);
            enemy.GetComponent<S1Scooter>().setStopMove(new Vector2(0, -3), 1, 2, new Vector2(-1, -2));
            enemy.GetComponent<S1Scooter>().setShootSector(new Vector2(0, -2f), 1, 10f, true, 1f, 0.2f, 3);
            yield return delayTime;
        }
        yield return delayOneSec;

        delayTime = new WaitForSeconds(0.9f);
        for(int i=0; i<2; i++){
            enemy = Instantiate(StageObj.Enemies["red_car"], new Vector2(0, 7f), transform.rotation);
            enemy.GetComponent<S1Car>().setStraightMove(new Vector2(0, -2));
            enemy.GetComponent<S1Car>().setShootCircle(new Vector2(0, -2f), 13, true, 1f, i+0.2f, 2);
            yield return delayTime;
        }
        yield return delayOneSec;

        delayTime = new WaitForSeconds(0.3f);
        for(int i=0; i<5; i++){
            enemy = Instantiate(StageObj.Enemies["blue_scooter"], new Vector2(5f , 7f), transform.rotation);
            enemy.GetComponent<S1Scooter>().setStraightMove(new Vector2(0, -3));
            enemy.GetComponent<S1Scooter>().setShootCircle(
                ourTool.vectorToPlayer(enemy).normalized *3, 1, true, 0.5f, 1f +0.2f*i, 2);
            enemy.GetComponent<S1Scooter>().setHealth(40);
            yield return delayTime;
        }

        delayTime = new WaitForSeconds(0.3f);
        for(int i=0; i<5; i++){
            enemy = Instantiate(StageObj.Enemies["blue_scooter"], new Vector2(-5f , 7f), transform.rotation);
            enemy.GetComponent<S1Scooter>().setStraightMove(new Vector2(0, -3));
            enemy.GetComponent<S1Scooter>().setShootCircle(
                ourTool.vectorToPlayer(enemy).normalized *3, 1, true, 0.5f, 1f +0.2f*i, 2);
            enemy.GetComponent<S1Scooter>().setHealth(40);
            yield return delayTime;
        }
        yield return delayOneSec;
        
        delayTime = new WaitForSeconds(0.6f);
        enemy = Instantiate(StageObj.Enemies["red_scooter"], new Vector2(0 , 7f), transform.rotation);
        enemy.GetComponent<S1Scooter>().setStraightMove(new Vector2(0, -2f));
        enemy.GetComponent<S1Scooter>().setShootSector(new Vector2(0, -3f), 1, 10f, true, 1f, 1.4f, 5);
        yield return delayTime;

        for(int i=1; i<4; i++){
            enemy = Instantiate(StageObj.Enemies["red_scooter"], new Vector2(1.5f*i , 7f), transform.rotation);
            enemy.GetComponent<S1Scooter>().setStraightMove(new Vector2(0, -2f));
            enemy.GetComponent<S1Scooter>().setShootSector(new Vector2(0, -3f), 1, 10f, true, 1f, 1.4f, 5);

            enemy = Instantiate(StageObj.Enemies["red_scooter"], new Vector2(-1.5f*i , 7f), transform.rotation);
            enemy.GetComponent<S1Scooter>().setStraightMove(new Vector2(0, -2f));
            enemy.GetComponent<S1Scooter>().setShootSector(new Vector2(0, -3f), 1, 10f, true, 1f, 1.4f, 5);

            if(i==3){
                enemy = Instantiate(StageObj.Enemies["red_car"], new Vector2(0, 7f), transform.rotation);
                enemy.GetComponent<S1Car>().setStraightMove(new Vector2(0, -2f));
                enemy.GetComponent<S1Car>().setShootCircle(new Vector2(0, -2f), 7, false, 1f, 1.4f, 3);
            }
            yield return delayTime;
        }

        for(int i=2; i>=1; i--){
            enemy = Instantiate(StageObj.Enemies["red_scooter"], new Vector2(1.5f*i , 7f), transform.rotation);
            enemy.GetComponent<S1Scooter>().setStraightMove(new Vector2(0, -2f));
            enemy.GetComponent<S1Scooter>().setShootSector(new Vector2(0, -3f), 1, 10f, true, 1f, 1.4f, 5);

            enemy = Instantiate(StageObj.Enemies["red_scooter"], new Vector2(-1.5f*i , 7f), transform.rotation);
            enemy.GetComponent<S1Scooter>().setStraightMove(new Vector2(0, -2f));
            enemy.GetComponent<S1Scooter>().setShootSector(new Vector2(0, -3f), 1, 10f, true, 1f, 1.4f, 5);
            yield return delayTime;
        }

        delayTime = new WaitForSeconds(0.5f);
        enemy = Instantiate(StageObj.Enemies["red_scooter"], new Vector2(0 , 7f), transform.rotation);
        enemy.GetComponent<S1Scooter>().setStraightMove(new Vector2(0, -2f));
        enemy.GetComponent<S1Scooter>().setShootSector(new Vector2(0, -3f), 1, 10f, true, 1f, 1.4f, 5);
        
        while(stageTimer <= 93){
            yield return null;
        }
        StartCoroutine(missBang());
    }

    IEnumerator missBang(){
        player.canShoot = false;
        dsOne.ActivateDialogue();
        yield return new WaitForSeconds(1f);
        GameObject mb = Instantiate(MissBang, new Vector3(0, 5, 0), new Quaternion());
        while(dsOne.IsRunning()){
            yield return null;
        }
        player.canShoot = true;
        nowPlaying.Stop();
        nowPlaying = bangMusic;
        nowPlaying.Play();
        mb.GetComponent<abstractBoss>().active();
        background.start_bang();

        while(mb.GetComponent<abstractBoss>().isRun())
        {
            yield return null;
        }
        dsTwo.ActivateDialogue();
        while(dsTwo.IsRunning()){
            yield return null;
        }
        StartCoroutine(ending());
    }
    IEnumerator ending(){
        SpriteRenderer esr = Ending.GetComponent<SpriteRenderer>();
        esr.enabled = true;
        bangMusic.Stop();
        yield return new WaitForSeconds(1f);
        Ending.GetComponent<VideoPlayer>().Play();
        Ending.GetComponent<AudioSource>().Play();
        float timer=0f, fadeTimer=5f;
        while(timer < fadeTimer){
            timer += Time.deltaTime;
            if(timer >= fadeTimer){
                timer = fadeTimer;
            }
            esr.color = new Color(timer/fadeTimer, timer/fadeTimer, timer/fadeTimer, timer/fadeTimer);
            yield return null;
        }
        endButton.SetActive(true);
    }
    IEnumerator walkchangefield(){
        while(stageTimer <= 77f){
            yield return null;
        }
        background.start_walkchange();
    }
}
