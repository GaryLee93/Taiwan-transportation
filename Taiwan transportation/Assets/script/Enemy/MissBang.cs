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
    int HP=100,limitHP=20,section=0,state=0;
    bool actionChecker=false;
    private enum State {idle,setPos,normalAtk_1,glassRain,normalAtk_2,brockenCar,sleep}
    bool[] sectionCheck = new bool[6]; //check whether a section is actived
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        an = gameObject.GetComponent<Animator>();
        clock = Clock.clockInstance;
        for(int i=0;i<6;i++)sectionCheck[i] = false;
        section=0;
        state = (int)State.idle;
        active();
    }
    void Update()
    {
        an.SetFloat("x_velocity",rb.velocity.x);
        action();
    }
    void setPos() // not complete yet
    {
        sectionCheck[(int)State.setPos] = true;
        transform.position = new Vector2(0,4);
        state = (int)State.idle; 
    }
    IEnumerator normalAtk_1()
    {
        sectionCheck[(int)State.normalAtk_1] = true;
        GameObject[] shooter = new GameObject[3];
        GameObject clone,player = GameObject.FindGameObjectWithTag("Player");
        int layer=0,bulletEachCol=10;
        for(int i=0;i<3;i++) shooter[i] = transform.GetChild(i).gameObject;
        shooter[0].transform.localPosition = new Vector2(0,-0.5f);
        shooter[1].transform.localPosition = new Vector2(-0.25f,-0.5f);
        shooter[2].transform.localPosition = new Vector2(0.25f,-0.5f);

        clock.setTimer("timer",5f);
        while(clock.checkTimer("timer") && HP>limitHP)
        {
            yield return new WaitForSeconds(1f);
            Vector2 target = player.transform.position;
            for(int i=0;i<bulletEachCol;i++)
            {
                yield return new WaitForSeconds(0.1f);
                for(int j=0;j<3;j++)
                {
                    Vector2 dire = target-(Vector2)shooter[j].transform.position;
                    dire.Normalize();
                    clone = objectPooler.spawnFromPool("purple_bullet",shooter[j].transform.position,shooter[j].transform.rotation,null);
                    clone.GetComponent<SpriteRenderer>().sortingOrder = layer;
                    clone.GetComponent<Rigidbody2D>().velocity = dire*5;
                    layer++;
                }
            }
        }
        state = (int)State.idle;
        section++;
    }
    IEnumerator normalAtk_2()
    {
        sectionCheck[(int)State.normalAtk_2] = true;
        GameObject shooter = transform.GetChild(0).gameObject;
        GameObject colone;
        Vector2 dire = new Vector2(1,0);
        int layer=0,bulletEachCircle=20;
        shooter.transform.localPosition = new Vector3(0,0,0);
        clock.setTimer("timer",5f);
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
        state = (int)State.idle;
        section++;
    }
    IEnumerator glassRain() 
    {
        sectionCheck[(int)State.glassRain] = true;
        GameObject shooter = transform.GetChild(0).gameObject;
        GameObject colone;
        shooter.transform.localPosition = new Vector3(0,0,0);
        
        clock.setTimer("glassRain",5f);
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
        state = (int)State.idle;
        section++;
    }
    IEnumerator brockenCar()
    {
        sectionCheck[(int)State.brockenCar] = true;
        GameObject [] shooter = new GameObject[3];
        GameObject clone;
        int bulletEachCircle=100,layer=0;

        for(int i=0;i<3;i++)
        {
            shooter[i] = transform.GetChild(i).gameObject;
            shooter[i].transform.localPosition = new Vector2(0,0);
        }
        for(int k=0;k<5;k++)
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
        state = (int)State.idle;
        section++;
    }
    void stateMachine()
    {
        if(state == (int)State.idle)
        {
            setPos();
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
                StartCoroutine(normalAtk_1());
            }

            if(state==(int)State.glassRain && !sectionCheck[(int)State.glassRain])
            {
                StartCoroutine(glassRain());
            }
            
            if(state==(int)State.normalAtk_2 && !sectionCheck[(int)State.normalAtk_2])
            {
                StartCoroutine(normalAtk_2());
            }

            if(state==(int)State.brockenCar && !sectionCheck[(int)State.brockenCar])
            {
                StartCoroutine(brockenCar());
            }

            if(state == (int)State.sleep)actionChecker = false;
            stateMachine();
        }
    }
    public void active()
    {
        state = (int)State.idle;
        actionChecker = true;
    }
    public bool isRun()
    {
        return actionChecker;
    }
}
