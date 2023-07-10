using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissBang : MonoBehaviour
{
    [SerializeField] GameObject normal_bullet_1;
    [SerializeField] GameObject backMirror;
    Rigidbody2D rb;
    Animator an;
    Clock clock;
    Vector2 oriPos = new Vector2(0,4);
    EnemyMoveControl EMC;
    int HP=100,limitHP=20,section=0,state=0;
    bool actionChecker=false;
    private enum State {idle,setPos,normalAtk_1,glassRain,normalAtk_2,brockenCar,sleep}
    bool[] sectionCheck = new bool[7]; //check whether a section is actived
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        EMC = GetComponent<EnemyMoveControl>();
        oriPos = new Vector2(0,4);
        clock = Clock.clockInstance;
        for(int i=0;i<7;i++)sectionCheck[i] = false;
        section=0;
        state = (int)State.sleep;
        active();
    }
    void Update()
    {
        an.SetFloat("x_velocity",rb.velocity.x);
        action();
    }
    void setPos() // not complete yet
    {
        EMC.slowDownMove(oriPos - (Vector2)transform.position,0.5f);
        state = (int)State.idle; 
    }
    IEnumerator normalAtk_1(float time)
    {
        yield return new WaitWhile(() => EMC.isMove()==true);
        GameObject shooter = transform.GetChild(0).gameObject;
        GameObject clone;
        int layer=0,bulletEachCol=5;
        float bulletInterval = 0.4f;
        shooter.transform.localPosition = new Vector2(0,0);

        clock.setTimer("timer",time);
        
        while(clock.checkTimer("timer") && HP>limitHP)
        {
            yield return new WaitForSeconds(1f);
            Vector2 dire = ourTool.vectorToPlayer(shooter);
            Transform t = shooter.transform;
            dire.Normalize();
            for(int k=0;k<bulletEachCol;k++)
            {
                clone = objectPooler.spawnFromPool("red_bullet",(Vector2)t.position+(dire*(1+bulletInterval*k)),t.rotation,null);
                clone.GetComponent<SpriteRenderer>().sortingOrder = layer;
                clone.GetComponent<Rigidbody2D>().velocity = dire*5;
                layer++;
            }
        }

        // finish
        setPos();
        section++;
    }
    IEnumerator normalAtk_2(float time)
    {
        yield return new WaitWhile(() => EMC.isMove()==true);
        GameObject shooter = transform.GetChild(0).gameObject;
        GameObject colone;
        Vector2 dire = new Vector2(1,0);
        int layer=0,bulletEachCircle=20;
        shooter.transform.localPosition = new Vector3(0,0,0);
        clock.setTimer("timer",time);
        while(clock.checkTimer("timer"))
        {
            for(int i=0;i<bulletEachCircle;i++)
            {
                yield return new WaitForSeconds(0.05f);
                colone = objectPooler.spawnFromPool("purple_bullet",shooter.transform.position,
                shooter.transform.rotation,gameObject);
                colone.GetComponent<SpriteRenderer>().sortingOrder = layer;
                colone.GetComponent<Rigidbody2D>().velocity = 3*dire;
                dire = ourTool.trans_matrix(dire,ourTool.eulerToRadian(360/bulletEachCircle));
                layer++;
            }
        }
        setPos();
        section++;
    }
    IEnumerator glassRain(float time) 
    {
        yield return new WaitWhile(() => EMC.isMove()==true);
        sectionCheck[(int)State.glassRain] = true;
        GameObject shooter = transform.GetChild(0).gameObject;
        GameObject colone;
        shooter.transform.localPosition = new Vector3(0,0,0);
        
        clock.setTimer("glassRain",time);
        while(clock.checkTimer("glassRain"))
        {
            Vector2 dire = new Vector2(1,0)*10;
            colone = Instantiate(backMirror,shooter.transform.position,shooter.transform.rotation);
            int chose = Random.Range(1,3);
            if(chose==1) dire = ourTool.trans_matrix(dire,ourTool.eulerToRadian(Random.Range(30,80)));
            else if(chose==2) dire = ourTool.trans_matrix(dire,ourTool.eulerToRadian(Random.Range(100,150)));
            colone.GetComponent<Rigidbody2D>().velocity = dire;
            yield return new WaitForSeconds(2);
        }
        setPos();
        section++;
    }
    IEnumerator brockenCar(float time)
    {
        yield return new WaitWhile(() => EMC.isMove()==true);
        sectionCheck[(int)State.brockenCar] = true;
        GameObject [] shooter = new GameObject[3];
        GameObject clone;
        int bulletEachCircle=100,layer=0;

        for(int i=0;i<3;i++)
        {
            shooter[i] = transform.GetChild(i).gameObject;
            shooter[i].transform.localPosition = new Vector2(0,0);
        }
        clock.setTimer("timer",time);
        while(clock.checkTimer("timer"))
        {
            yield return new WaitForSeconds(1f);
            Vector2 dire = new Vector2(0,-1);
            int oritation = Random.Range(-2,2);
            for(int i=0;i<3;i++)
            {
                yield return new WaitForSeconds(0.2f);
                for(int j=0;j<bulletEachCircle;j++)
                {
                    if(j>1)
                    {
                        shooter[i].transform.Rotate(new Vector3(0,0,360/bulletEachCircle));
                        clone = objectPooler.spawnFromPool("aqua_bullet",shooter[i].transform.position,shooter[i].transform.rotation,null);
                        clone.GetComponent<SpriteRenderer>().sortingOrder = layer;
                        clone.GetComponent<Rigidbody2D>().velocity = dire*3;
                    }
                    dire = ourTool.trans_matrix(dire,ourTool.eulerToRadian(360f/bulletEachCircle));
                    layer++;
                }
                if(oritation>0)
                {
                    dire = ourTool.trans_matrix(dire,ourTool.eulerToRadian(5));
                }
                else 
                {
                    dire = ourTool.trans_matrix(dire,ourTool.eulerToRadian(-5));
                }
            }
        }
        setPos();
        section++;
    }
    void stateMachine()
    {
        if(state == (int)State.idle)
        {
            if(section==0) state = (int)State.normalAtk_1;
            else if(section==1) state = (int)State.glassRain;
            else if(section==2) state = (int)State.normalAtk_2;
            else if(section==3) state = (int)State.brockenCar;
            else if(section==4) state = (int)State.sleep;
        }
    }
    void action()
    {
        if(actionChecker)
        {
            if(state==(int)State.normalAtk_1 && !sectionCheck[(int)State.normalAtk_1])
            {
                sectionCheck[(int)State.normalAtk_1] = true;
                StartCoroutine(normalAtk_1(5f));
            }
            else if(state==(int)State.glassRain && !sectionCheck[(int)State.glassRain])
            {
                sectionCheck[(int)State.glassRain] = true;
                StartCoroutine(glassRain(5f));
            }
            else if(state==(int)State.normalAtk_2 && !sectionCheck[(int)State.normalAtk_2])
            {
                sectionCheck[(int)State.normalAtk_2] = true;
                StartCoroutine(normalAtk_2(5f));
            }
            else if(state==(int)State.brockenCar && !sectionCheck[(int)State.brockenCar])
            {
                sectionCheck[(int)State.brockenCar] = true;
                StartCoroutine(brockenCar(5f));
            }
            else if(state == (int)State.sleep)actionChecker = false;
            stateMachine();
        }
    }
    public void active()
    {
        setPos();
        actionChecker = true;
    }
    public bool isRun()
    {
        return actionChecker;
    }
}
