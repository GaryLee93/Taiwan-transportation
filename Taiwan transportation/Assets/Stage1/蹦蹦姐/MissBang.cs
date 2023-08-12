using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class MissBang : abstractBoss
{
    [SerializeField] GameObject backMirror;
    [SerializeField] GameObject redMidRound;
    [SerializeField] AudioSource doYouKnowWhoAmI;
    [SerializeField] AudioSource backMirrorSound;
    [SerializeField] AudioSource breakCarFour;
    VideoPlayer deadAnimation;
    Animator an;
    private enum SpellCard {brockenCar,glassRain,spellCardNum}
    bool[,] sectionCheck = new bool[(int)SpellCard.spellCardNum,2]; 
    //check whether a spellCard is actived, 0 for normal attack,1 for spellcard itself 
    private void Update()
    {
        action();
    }
    private void action()
    {
        if(actionCheck)
        {
            an.SetFloat("x_velocity",rb.velocity.x);
            if(section==(int)SpellCard.brockenCar) BrockenCar();
            else if(section==(int)SpellCard.glassRain) GlassRain();
            else if(section==(int)SpellCard.spellCardNum) die();
        }
    }
    private void GlassRain()
    {
        if(!sectionCheck[(int)SpellCard.glassRain,1])
        {          
            StartCoroutine(glassRain(40f));
            sectionCheck[(int)SpellCard.glassRain,1] = true;
        }
    }
    private void BrockenCar()
    {
        if(!sectionCheck[(int)SpellCard.brockenCar,0] && !useCard)
        {
            prepareNextAction(false,false,false,MaxHp/2,0);
            StartCoroutine(normalAtk(30f));
            clock.setSpellCardTimer(30f);
            sectionCheck[(int)SpellCard.brockenCar,0] = true;
        }
        else if(!sectionCheck[(int)SpellCard.brockenCar,1] && useCard)
        {
            StartCoroutine(brockenCar(35f));
            sectionCheck[(int)SpellCard.brockenCar,1] = true;
        }
    }

    protected override void die()
    {
        //deadAnimation.Play();
        Instantiate(explosionEffect, transform.position, transform.rotation);
        actionCheck = false;
        StageObj.eraseAllBullet();
        transform.Find("HpCanva").gameObject.SetActive(false);
        summonDrop(30,"score");
        summonDrop(30,"power");
        summonDrop(1,"bomb");
    }
    
    public override void active()
    {
        deadAnimation = GetComponent<VideoPlayer>();
        an = GetComponent<Animator>();
        for(int i=0;i<(int)SpellCard.spellCardNum;i++)
        {
            sectionCheck[i,0] = false;
            sectionCheck[i,1] = false;
        }
        init(4000);
        prepareNextAction(false,false,false,0,0);
        actionCheck = true;
    }
    IEnumerator normalAtk(float time)
    {
        yield return new WaitWhile(() => recoverCheck==true);
        yield return new WaitWhile(() => isMove()==true);
        yield return new WaitWhile(() => isOP() == true);
        
        GameObject shooter = transform.GetChild(0).gameObject;
        GameObject colone;
        Vector2 dire = new Vector2(1,0);
        int layer=0,bulletEachCircle=20,counts=0;
        shooter.transform.localPosition = new Vector3(0,0,0);

        setTimer("normalAtk",time);
        slowDownMove(new Vector2(-3,0),0.5f);
        while(checkTimer("normalAtk")&&currentHp>lowHp)
        {
            for(int i=0;i<bulletEachCircle;i++)
            {
                if(currentHp<lowHp) break; // early break to avoid take too many demage
                else if(!isMove() && counts%2==0 ) 
                {
                    slowDownMove(new Vector2(6,0),1.5f);
                    counts++;
                }
                else if(!isMove() && counts%2==1) 
                {
                    slowDownMove(new Vector2(-6,0),1.5f);
                    counts++;
                }
                yield return new WaitForSeconds(0.01f);

                colone = objectPooler.spawnFromPool(redMidRound,shooter.transform.position,
                shooter.transform.rotation);
                colone.GetComponent<SpriteRenderer>().sortingOrder = layer;
                colone.GetComponent<Rigidbody2D>().velocity = 3*dire;
                dire = ourTool.trans_matrix(dire,ourTool.eulerToRadian(360/bulletEachCircle));
                layer++;
            }
        }
        //finish
        stopMove();
        prepareNextAction(true,false,false,-1,1);
    }
    IEnumerator brockenCar(float time)
    {
        yield return new WaitWhile(() => recoverCheck==true);
        yield return new WaitWhile(() => isMove()==true);
        yield return new WaitWhile(() => isOP() == true);

        bp.startMove("MissBang",0.75f,1f);
        sp.startSmallize("車符「破蓋天鳴掌」",0.75f,1f);
        clock.setSpellCardTimer(35f);

        doYouKnowWhoAmI.Play();
        GameObject shooter = transform.GetChild(0).gameObject;
        GameObject clone;
        Transform tem = shooter.transform;
        int bulletEachCircle=80,layer=0;

        setTimer("brockenCar",time);
        while(checkTimer("brockenCar")&&currentHp>=0f)
        {
            Vector2 dire = new Vector2(0,-1);
            int oritation = Random.Range(-2,2);

            breakCarFour.Play();
            for(int i=0;i<3;i++)
            {
                yield return new WaitForSeconds(0.3f);
                for(int j=0;j<bulletEachCircle;j++)
                {
                    if(j>0)
                    {
                        clone = objectPooler.spawnFromPool("aqua_bullet",(Vector2)tem.position+dire*(i+1.5f),tem.rotation);
                        clone.GetComponent<SpriteRenderer>().sortingOrder = layer;
                        clone.GetComponent<Rigidbody2D>().velocity = dire*2;
                    }
                    dire = ourTool.trans_matrix(dire,ourTool.eulerToRadian(360f/bulletEachCircle));
                    layer++;
                }
                if(currentHp<0f) break;
                else if(oritation>0)
                {
                    dire = ourTool.trans_matrix(dire,ourTool.eulerToRadian(2));
                }
                else 
                {
                    dire = ourTool.trans_matrix(dire,ourTool.eulerToRadian(-2));
                }
            }
            if(currentHp<0f) break;
            yield return new WaitForSeconds(0.8f);
        }

        summonDrop(15,"score");
        summonDrop(15,"power");
        spellCardCalculate();
        prepareNextAction(true,true,true,-1,2.5f);
        
    }
    IEnumerator glassRain(float time) 
    {
        yield return new WaitWhile(() => recoverCheck==true);
        yield return new WaitWhile(() => isMove()==true);
        yield return new WaitWhile(() => isOP() == true);

        bp.startMove("MissBang",0.75f,1f);
        sp.startSmallize("鏡符「萬元的玻璃雨」",0.75f,1f);
        clock.setSpellCardTimer(40f);

        doYouKnowWhoAmI.Play();
        GameObject shooter = transform.GetChild(0).gameObject;
        GameObject colone;
        shooter.transform.localPosition = new Vector3(0,0,0);
        
        setTimer("glassRain",time);
        while(checkTimer("glassRain")&&currentHp>=lowHp)
        {
            Vector2 dire = new Vector2(1,0)*7.5f;
            colone = Instantiate(backMirror,shooter.transform.position,shooter.transform.rotation);
            int chose = Random.Range(1,3);
            if(chose==1) dire = ourTool.trans_matrix(dire,ourTool.eulerToRadian(Random.Range(30,80)));
            else if(chose==2) dire = ourTool.trans_matrix(dire,ourTool.eulerToRadian(Random.Range(100,150)));
            colone.GetComponent<Rigidbody2D>().velocity = dire;

            backMirrorSound.Play();
            
            if(currentHp<=lowHp) break;
            yield return new WaitForSeconds(1f);
            if(currentHp<=lowHp) break;
            yield return new WaitForSeconds(1f);
        }

        //finish
        spellCardCalculate();
        prepareNextAction(true,true,false,0,0);
    }
       
}
