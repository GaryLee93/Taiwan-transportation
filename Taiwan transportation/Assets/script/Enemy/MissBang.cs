using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class MissBang : abstractBoss
{
    [SerializeField] GameObject backMirror;
    VideoPlayer deadAnimation;
    Animator an;
    Clock clock;
    Vector2 oriPos = new Vector2(0,4);
    private enum SpellCard {brockenCar,glassRain,spellCardNum}
    bool[,] sectionCheck = new bool[(int)SpellCard.spellCardNum,2]; 
    //check whether a spellCard is actived, 0 for normal attack,1 for spellcard itself 
    void Start()
    {
        deadAnimation = GetComponent<VideoPlayer>();
        an = GetComponent<Animator>();
        for(int i=0;i<(int)SpellCard.spellCardNum;i++)
        {
            sectionCheck[i,0] = false;
            sectionCheck[i,1] = false;
        }
        oriPos = new Vector2(0,4);
        base.init(2000,1000);
        active();
    }
    void Update()
    {
        an.SetFloat("x_velocity",rb.velocity.x);
        action();
    }
    void action()
    {
        if(actionCheck)
        {
            if(section==(int)SpellCard.brockenCar) BrockenCar();
            else if(section==(int)SpellCard.glassRain) GlassRain();
            else if(section==(int)SpellCard.spellCardNum) actionCheck = false;
        }
    }
    void GlassRain()
    {
        if(!sectionCheck[(int)SpellCard.glassRain,1])
        {
            OPMode(2f);            
            bp.startMove("MissBang",0.75f,1f);
            sp.startSmallize("鏡符「萬元的玻璃雨」",0.75f,1f);
            StartCoroutine(glassRain(40f));
            clock.setSpellCardTimer(40f);
            sectionCheck[(int)SpellCard.glassRain,1] = true;
        }
        
    }
    void BrockenCar()
    {
        if(!sectionCheck[(int)SpellCard.brockenCar,0] && !useCard)
        {
            StageObj.eraseAllBullet();
            StartCoroutine(normalAtk(30f));
            clock.setSpellCardTimer(30f);
            sectionCheck[(int)SpellCard.brockenCar,0] = true;
        }
        else if(!sectionCheck[(int)SpellCard.brockenCar,1] && useCard)
        {
            OPMode(2f);
            bp.startMove("MissBang",0.75f,1f);
            sp.startSmallize("車符「破蓋天鳴掌」",0.75f,1f);
            StartCoroutine(brockenCar(35f));
            clock.setSpellCardTimer(35f);
            sectionCheck[(int)SpellCard.brockenCar,1] = true;
        }
    }
    protected override void resetPos() // not complete yet
    {
        startRecover();
        slowDownMove(oriPos - (Vector2)transform.position,0.5f);
    }
    IEnumerator normalAtk(float time)
    {
        //wait for preset over
        yield return new WaitWhile(() => recoverCheck==true);
        yield return new WaitWhile(() => isMove()==true);
        
        GameObject shooter = transform.GetChild(0).gameObject;
        GameObject colone;
        Vector2 dire = new Vector2(1,0);
        int layer=0,bulletEachCircle=20;
        shooter.transform.localPosition = new Vector3(0,0,0);
        setTimer("normalAtk",time);
        while(checkTimer("normalAtk")&&currentHp>lowHp)
        {
            for(int i=0;i<bulletEachCircle;i++)
            {
                if(currentHp<lowHp && isMove()) break; // early break to avoid take too many demage
                else if(!isMove() && transform.position.x<0) slowDownMove(new Vector2(3,0),1f);
                else if(!isMove() && transform.position.x>=0) slowDownMove(new Vector2(-3,0),1f);

                yield return new WaitForSeconds(0.01f);
                colone = objectPooler.spawnFromPool("enemy_bullet",shooter.transform.position,
                shooter.transform.rotation,gameObject);
                colone.GetComponent<SpriteRenderer>().sortingOrder = layer;
                colone.GetComponent<Rigidbody2D>().velocity = 3*dire;
                dire = ourTool.trans_matrix(dire,ourTool.eulerToRadian(360/bulletEachCircle));
                layer++;
            }
        }

        //finish
        useCard = true;
        StageObj.eraseAllBullet();
    }
    IEnumerator glassRain(float time) 
    {
        //wait for preset over
        yield return new WaitWhile(() => recoverCheck==true);
        yield return new WaitWhile(() => isMove()==true);

        GameObject shooter = transform.GetChild(0).gameObject;
        GameObject colone;
        shooter.transform.localPosition = new Vector3(0,0,0);
        
        setTimer("glassRain",time);
        while(checkTimer("glassRain")&&currentHp>=0f)
        {
            Vector2 dire = new Vector2(1,0)*7.5f;
            colone = Instantiate(backMirror,shooter.transform.position,shooter.transform.rotation);
            int chose = Random.Range(1,3);
            if(chose==1) dire = ourTool.trans_matrix(dire,ourTool.eulerToRadian(Random.Range(30,80)));
            else if(chose==2) dire = ourTool.trans_matrix(dire,ourTool.eulerToRadian(Random.Range(100,150)));
            colone.GetComponent<Rigidbody2D>().velocity = dire;
            yield return new WaitForSeconds(2.5f);
        }

        //finish
        resetPos();
        section++;
        useCard = false;
        sp.retriveTitle();
        StageObj.eraseAllBullet();
        deadAnimation.Play();
    }
    IEnumerator brockenCar(float time)
    {
        //wait for preset over
        yield return new WaitWhile(() => recoverCheck==true);
        yield return new WaitWhile(() => isMove()==true);

        GameObject shooter = transform.GetChild(0).gameObject;
        GameObject clone;
        Transform tem = shooter.transform;
        int bulletEachCircle=100,layer=0;

        setTimer("brockenCar",time);
        while(checkTimer("brockenCar")&&currentHp>=0f)
        {
            yield return new WaitForSeconds(1f);
            Vector2 dire = new Vector2(0,-1);
            int oritation = Random.Range(-2,2);
            for(int i=0;i<3;i++)
            {
                yield return new WaitForSeconds(0.2f);
                for(int j=0;j<bulletEachCircle;j++)
                {
                    if(j>2)
                    {
                        clone = objectPooler.spawnFromPool("aqua_bullet",(Vector2)tem.position+dire*(i),tem.rotation,null);
                        clone.GetComponent<SpriteRenderer>().sortingOrder = layer;
                        clone.GetComponent<Rigidbody2D>().velocity = dire*2;
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

        //finish
        resetPos();
        section++;
        useCard = false;
        sp.retriveTitle();
        StageObj.eraseAllBullet();
    }
    public override void active()
    {
        resetPos();
        actionCheck = true;
        clock = Clock.clockInstance;
    }   
}
