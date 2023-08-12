using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class a_ba : abstractBoss
{
    VideoPlayer deadAnimation;
    [SerializeField] GameObject red_middle;
    bool[,] sectionCheck = new bool[(int)SpellCard.spellCardNum,2]; 
    private enum SpellCard{riceSea,spellCardNum}

    [SerializeField] AudioSource startSound;
    [SerializeField] AudioSource spellSound;

    private void Update() 
    {
        action();
    }  
    private void RiceSea()
    {
        if(!sectionCheck[(int)SpellCard.riceSea,0] && !useCard)
        {
            prepareNextAction(false,false,false,MaxHp/2,0);
            StartCoroutine(normalAttack(30f));
            clock.setSpellCardTimer(30f);
            sectionCheck[(int)SpellCard.riceSea,0] = true;
        }
        else if(!sectionCheck[(int)SpellCard.riceSea,1] && useCard)
        {
            bp.startMove("taxi",1f,0.75f);
            sp.startSmallize("稻符「黃金雨」",1f,0.75f);
            StartCoroutine(rice_sea(30f));
            clock.setSpellCardTimer(30f);
            sectionCheck[(int)SpellCard.riceSea,1] = true;
        }
    }
    private void action()
    {
        if(actionCheck)
        {
            if(section==(int)SpellCard.riceSea)
            {
                RiceSea();
            }
            else if(section==(int)SpellCard.spellCardNum) 
            {
                die();
            }
        }
    }
    
    protected override void die()
    {
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
        startSound.Play();
        oriPos = new Vector2(0,4);
        for(int i=0;i<(int)SpellCard.spellCardNum;i++)
        {
            sectionCheck[i,0] = false;
            sectionCheck[i,1] = false;
        }
        init(3000);
        prepareNextAction(false,false,false,0,0);

        actionCheck = true;
    }
    IEnumerator normalAttack(float time)
    {
        yield return new WaitWhile(() => isMove()==true);

        transform.GetChild(0).localPosition = new Vector3(0,0,0);
        int colNum=12,bulletEachCol=3,count=0,layer=0;
        float bulletInterval=0.8f,openAngle=360f,angle;
        Vector2 dire = ourTool.rotate_vector(new Vector2(0,-1),30f);
        Transform shooterTrans = transform.GetChild(0);
        GameObject clone;

        setTimer("normalAttack",time);
        while(checkTimer("normalAttack")&&currentHp>lowHp)
        {
            openAngle = 360f;
            for(int i=0;i<4;i++)
            {
                dire = ourTool.rotate_vector(new Vector2(0,1),45f*(i));
                angle = 45f*i;
                for(int j=0;j<=colNum;j++)
                {
                    for(int k=0;k<bulletEachCol;k++)
                    {
                        if(j==colNum/2) break;
                        clone = objectPooler.spawnFromPool(red_middle,(Vector2)shooterTrans.position+dire*(bulletInterval*k),shooterTrans.rotation);
                        clone.transform.Rotate(0,0,angle);
                        clone.GetComponent<SpriteRenderer>().sortingOrder = layer;
                        clone.GetComponent<Rigidbody2D>().velocity = dire*5;
                        layer++;
                    }
                    dire = ourTool.rotate_vector(dire,openAngle/colNum);
                    angle += openAngle/colNum;
                }
                openAngle -= 90f;
                yield return new WaitForSeconds(0.4f);
            }
            if(currentHp<=lowHp) break;
            for(int k=0;k<bulletEachCol;k++)
            {
                clone = objectPooler.spawnFromPool(red_middle,(Vector2)shooterTrans.position+new Vector2(0,-1)*(bulletInterval*k),shooterTrans.rotation);
                clone.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-1)*5;
            }

            if(currentHp<=lowHp) break;
            else if(count%3==0) slowDownMove(new Vector2(1,5)-(Vector2)transform.position,0.5f);
            else if(count%3==1) slowDownMove(new Vector2(-1,5)-(Vector2)transform.position,0.5f);
            else if(count%3==2) slowDownMove(oriPos-(Vector2)transform.position,0.5f);
            count++;
            yield return new WaitWhile(() => isMove()==true);
        }

        prepareNextAction(true,false,false,-1,1f);
    }
    IEnumerator rice_sea(float time)
    {
        spellSound.Play();
        
        slowDownMove(oriPos-(Vector2)transform.position,0.5f);
        yield return new WaitWhile(() => recoverCheck==true);
        yield return new WaitWhile(() => isMove()==true);
        yield return new WaitWhile(() => isOP()==true);
        GameObject sc_manager = transform.GetChild(2).gameObject;
        GameObject[] sc_shooter = new GameObject[2];
        Transform[] tem = new Transform[2];
        for(int i=0;i<2;i++) 
        {
            sc_shooter[i] = sc_manager.transform.GetChild(i).gameObject;
            tem[i] = sc_shooter[i].transform;
        }
        sc_shooter[0].transform.localPosition = new Vector2(-3.5f,2f);
        sc_shooter[1].transform.localPosition = new Vector2(3.5f,2f);
        
        setTimer("rice_sea",time);
        GameObject colone;
        int layer=0,correct=-1;
        while(checkTimer("rice_sea") && currentHp>=0)
        {
            yield return new WaitForSeconds(0.2f);
            sc_shooter[0].transform.position = new Vector3((-3.5f)+(1.5f)*1.5f*Mathf.Sin(Time.time*2),tem[0].position.y,0);
            sc_shooter[1].transform.position = new Vector3((3.5f)+(1.5f)*1.5f*Mathf.Sin(Time.time*2),tem[1].position.y,0);
            for(int i=0;i<2;i++)
            {
                colone = objectPooler.spawnFromPool("rice",tem[i].position,tem[i].rotation);
                colone.transform.Rotate(new Vector3(0,0,90*correct),Space.Self);
                colone.GetComponent<Rigidbody2D>().velocity = new Vector3(0,-5,0);
                colone.GetComponent<SpriteRenderer>().sortingOrder = layer;
                layer++;

                colone = objectPooler.spawnFromPool("rice",tem[i].position+new Vector3(2,0,0)*correct,tem[i].rotation);
                colone.transform.Rotate(new Vector3(0,0,90*correct),Space.Self);
                colone.GetComponent<Rigidbody2D>().velocity = new Vector3(0,-5,0);
                colone.GetComponent<SpriteRenderer>().sortingOrder = layer;
                layer++;
                correct*=(-1);
            }   
        }
        spellCardCalculate();
        prepareNextAction(false,true,false,-1,1f);
    }
}
