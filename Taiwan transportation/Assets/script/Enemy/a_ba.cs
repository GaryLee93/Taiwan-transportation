using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_ba : MonoBehaviour
{
    public GameObject normal_bullet_1;
    public GameObject normal_bullet_2;
    public GameObject sc_bullet;
    Rigidbody2D rb;
    GameObject player;
    Clock clock;
    bool actionChecker=false;
    bool[] sectionCheck = new bool[5];
    int HP=100,limitHP=20,state,section=0;
    private enum State{idle,setPos,normalAttack,riceSea,sleep}
    void Start()
    {
        clock = Clock.clockInstance;
        rb=gameObject.GetComponent<Rigidbody2D>();
        state = (int)State.sleep;
        section=0;
        actionChecker=false;
        for(int i=0;i<5;i++) sectionCheck[i] = false;
        active();
    }
    void Update() 
    {
        action();
    }    
    IEnumerator normal_attack(float time)
    {
        sectionCheck[(int)State.normalAttack] = true;
        int colNum=8,oritation=1,bulletEachCol=6;
        float bulletInterval=0.4f,openAngle=120f;
        GameObject shooter = transform.GetChild(0).gameObject,clone;
        Vector2 ini_pos = shooter.transform.position;
        Vector2 dire;
        Transform tem = shooter.transform;

        tem.localPosition = new Vector2(0,0);
        clock.setTimer("timer",time);
        while(clock.checkTimer("timer") && HP>limitHP)
        {
            yield return new WaitForSeconds(0.5f);
            if(oritation>0) dire = ourTool.rotate_vector(new Vector2(0,-1),-45f);
            else dire = ourTool.rotate_vector(new Vector2(0,-1),45f+openAngle/(2*colNum));
            for(int i=0;i<colNum;i++)
            {
                if(i==colNum-1 && oritation>0) break;
                for(int j=0;j<bulletEachCol;j++)
                {
                    if(oritation>0)
                    {
                        clone = objectPooler.spawnFromPool("red_bullet",(Vector2)tem.position+dire*(bulletInterval*j),tem.rotation,null);
                        clone.GetComponent<Rigidbody2D>().velocity = dire*5;
                    }
                    else 
                    {
                        clone = objectPooler.spawnFromPool("orange_bullet",(Vector2)tem.position+dire*(bulletInterval*j),tem.rotation,null);
                        clone.GetComponent<Rigidbody2D>().velocity = dire*5;
                    } 
                }
                dire = ourTool.rotate_vector(dire,(openAngle/colNum)*oritation);
            }
            oritation*=-1;
        }
        shooter.SetActive(false);
        shooter.transform.position = ini_pos;
        section++;
        state = (int)State.idle;
    }
    IEnumerator riceAdditoin(float time)
    {
        GameObject shooter = transform.GetChild(0).gameObject,colone;
        Vector2 ini_pos = shooter.transform.position,dire;
        Transform shooterTrans = shooter.transform;
        int colNum=3,bulletEachCol=5;
        float bulletInterval=0.4f,openAngle=30f;
        shooterTrans.localPosition = new Vector2(0,0);
        clock.setTimer("addTimer",time);
        while(clock.checkTimer("addTimer") && HP>limitHP)
        {
            yield return new WaitForSeconds(0.8f);
            dire = ourTool.rotate_vector(new Vector2(0,-1),-(openAngle/2));
            int layer = 0;
            for(int i=0;i<colNum;i++)
            {
                for(int j=0;j<bulletEachCol;j++)
                {
                    colone = objectPooler.spawnFromPool("red_bullet",(Vector2)shooterTrans.position+dire*(1+bulletInterval*j),shooterTrans.rotation,null);
                    colone.GetComponent<SpriteRenderer>().sortingOrder = layer;
                    colone.GetComponent<Rigidbody2D>().velocity = dire*5;
                    layer++;
                }
                dire = ourTool.rotate_vector(dire,openAngle/(colNum-1));
            }
        }
        shooter.transform.localPosition = ini_pos;
    }
    IEnumerator rice_sea(float time)
    {
        sectionCheck[(int)State.riceSea] = true;
        yield return new WaitForSeconds(1f); 
        GameObject sc_manager = transform.GetChild(4).gameObject;
        GameObject[] sc_shooter = new GameObject[2];
        Transform[] tem = new Transform[2];
        for(int i=0;i<2;i++) 
        {
            sc_shooter[i] = sc_manager.transform.GetChild(i).gameObject;
            tem[i] = sc_shooter[i].transform;
        }
        sc_shooter[0].transform.localPosition = new Vector2(-3.5f,5f);
        sc_shooter[1].transform.localPosition = new Vector2(3.5f,5f);

        clock.setTimer("timer",time);
        GameObject colone;
        int layer=0,correct=-1;
        while(clock.checkTimer("timer") && HP>limitHP)
        {
            yield return new WaitForSeconds(0.15f);
            sc_shooter[0].transform.position = new Vector3((-3.5f)+(1.5f)*Mathf.Sin(Time.time*2),tem[0].position.y,0);
            sc_shooter[1].transform.position = new Vector3((3.5f)+(1.5f)*Mathf.Sin(Time.time*2),tem[1].position.y,0);
            for(int i=0;i<2;i++)
            {
                colone = objectPooler.spawnFromPool("rice",tem[i].position,tem[i].rotation,gameObject);
                colone.transform.Rotate(new Vector3(0,0,90*correct),Space.Self);
                colone.GetComponent<Rigidbody2D>().velocity = new Vector3(0,-5,0);
                colone.GetComponent<SpriteRenderer>().sortingOrder = layer;
                layer++;

                colone = objectPooler.spawnFromPool("rice",tem[i].position+new Vector3(2,0,0)*correct,tem[i].rotation,gameObject);
                colone.transform.Rotate(new Vector3(0,0,90*correct),Space.Self);
                colone.GetComponent<Rigidbody2D>().velocity = new Vector3(0,-5,0);
                colone.GetComponent<SpriteRenderer>().sortingOrder = layer;
                layer++;
                correct*=(-1);
            }   
        }
        section++;
        state = (int)State.idle;
    }
    void setPos() // not complete yet
    {
        sectionCheck[(int)State.setPos] = true;
        transform.position = new Vector2(0,4);
        state = (int)State.idle; 
    }
    void stateMachine()
    {
        if(state==(int)State.idle)
        {
            setPos();
            if(section==0) state = (int)State.normalAttack;
            else if(section==1) state = (int)State.riceSea;
            else if(section==2) state = (int)State.sleep;
        }
    }
    void action()
    {
        if(actionChecker)
        {
            if(state == (int)State.normalAttack && !sectionCheck[(int)State.normalAttack])
            {
                StartCoroutine(normal_attack(5f));
            }
            else if(state == (int)State.riceSea && !sectionCheck[(int)State.riceSea])
            {
                StartCoroutine(rice_sea(5f));
                StartCoroutine(riceAdditoin(5f));
            }
            else if(state == (int)State.sleep)
            {
                actionChecker = false;
            }

            stateMachine();
        }
    }
    public void active()
    {
        actionChecker = true;
        state = (int)State.idle;
    }
    public bool isRun()
    {
        return actionChecker;
    }
}
