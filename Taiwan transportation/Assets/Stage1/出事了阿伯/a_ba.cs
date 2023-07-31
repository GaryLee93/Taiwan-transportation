using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class a_ba : abstractBoss
{
    Vector2 oriPos;
    VideoPlayer deadAnimation;
    bool[,] sectionCheck = new bool[(int)SpellCard.spellCardNum,2]; 
    private enum SpellCard{riceSea,spellCardNum}
    private void Start()
    {
        deadAnimation = GetComponent<VideoPlayer>();
        active();
    }
    private void Update() 
    {
        action();
    }  
    private void RiceSea()
    {
        if(!sectionCheck[(int)SpellCard.riceSea,0] && !useCard)
        {
            setLowHp(MaxHp/2);
            StartCoroutine(normal_attack(30f));
            clock.setSpellCardTimer(30f);
            sectionCheck[(int)SpellCard.riceSea,0] = true;
        }
        else if(!sectionCheck[(int)SpellCard.riceSea,1] && useCard)
        {
            StageObj.eraseAllBullet();
            OPMode(2f);
            setLowHp(-1);
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
        deadAnimation.Play();
        actionCheck = false;
        StageObj.eraseAllBullet();
        transform.Find("HpCanva").gameObject.SetActive(false);
    }  
    protected override void resetPos() // not complete yet
    {
        if(useCard) sp.retriveTitle();
        startRecover();
        slowDownMove(oriPos-(Vector2)transform.position,0.5f);
        useCard = !useCard;
    }
    
    public override void active()
    {
        oriPos = new Vector2(0,4);
        for(int i=0;i<(int)SpellCard.spellCardNum;i++)
        {
            sectionCheck[i,0] = false;
            sectionCheck[i,1] = false;
        }
        init(1050);
        slowDownMove(oriPos-(Vector2)transform.position,0.5f);
        useCard = false;
        actionCheck = true;
    }
    
    IEnumerator normal_attack(float time)
    {
        yield return new WaitWhile(() => isMove()==true);
        int colNum=8,oritation=1,bulletEachCol=6;
        float bulletInterval=0.4f,openAngle=120f;
        GameObject shooter = transform.GetChild(0).gameObject,clone;
        Vector2 ini_pos = shooter.transform.position;
        Vector2 dire;
        Transform tem = shooter.transform;

        tem.localPosition = new Vector2(0,0);
        setTimer("normal_attack",time);
        while(checkTimer("normal_attack") && currentHp>lowHp)
        {
            yield return new WaitForSeconds(0.5f);
            if(oritation>0) dire = ourTool.rotate_vector(new Vector2(0,-1),-45f);
            else dire = ourTool.rotate_vector(new Vector2(0,-1),45f+openAngle/(2*colNum));
            for(int i=0;i<colNum;i++)
            {
                if((i==colNum-1 && oritation>0) || currentHp<lowHp) break;
                for(int j=0;j<bulletEachCol;j++)
                {
                    if(currentHp<lowHp) break;

                    if(oritation>0)
                    {
                        clone = objectPooler.spawnFromPool("circle_red",(Vector2)tem.position+dire*(bulletInterval*j),tem.rotation);
                        clone.GetComponent<Rigidbody2D>().velocity = dire*5;
                    }
                    else 
                    {
                        clone = objectPooler.spawnFromPool("orange_bullet",(Vector2)tem.position+dire*(bulletInterval*j),tem.rotation);
                        clone.GetComponent<Rigidbody2D>().velocity = dire*5;
                    } 
                }
                dire = ourTool.rotate_vector(dire,(openAngle/colNum)*oritation);
            }
            oritation*=-1;
        }
        shooter.SetActive(false);
        shooter.transform.position = ini_pos;
        useCard = true;
    }
    IEnumerator riceAdditoin(float time)
    {
        yield return new WaitWhile(() => recoverCheck==true);
        yield return new WaitWhile(() => isMove()==true);

        GameObject shooter = transform.GetChild(0).gameObject,colone;
        Transform player_t=GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 dire;
        Transform shooterTrans = shooter.transform;
        int colNum=2,bulletEachCol=5;
        float bulletInterval=0.4f,openAngle=30f;
        shooterTrans.localPosition = new Vector2(0,0);

        setTimer("riceAdditoin",time);
        while(checkTimer("riceAdditoin") && currentHp>=0)
        {
            slowDownMove(new Vector2(player_t.position.x,transform.position.y)-(Vector2)transform.position,1f);
            yield return new WaitWhile(()=> isMove()==true);
            dire = ourTool.rotate_vector(new Vector2(0,-1),-(openAngle/2));
            int layer = 0;
            for(int i=0;i<colNum;i++)
            {
                for(int j=0;j<bulletEachCol;j++)
                {
                    colone = objectPooler.spawnFromPool("red_mid_round",(Vector2)shooterTrans.position+dire*(1+bulletInterval*j),shooterTrans.rotation);
                    colone.GetComponent<SpriteRenderer>().sortingOrder = layer;
                    colone.GetComponent<Rigidbody2D>().velocity = dire*3;
                    layer++;
                }
                dire = ourTool.rotate_vector(dire,openAngle/(colNum-1));
            }
        }
    }
    IEnumerator rice_sea(float time)
    {
        yield return new WaitWhile(() => recoverCheck==true);
        yield return new WaitWhile(() => isMove()==true);
        GameObject sc_manager = transform.GetChild(2).gameObject;
        GameObject[] sc_shooter = new GameObject[2];
        Transform[] tem = new Transform[2];
        for(int i=0;i<2;i++) 
        {
            sc_shooter[i] = sc_manager.transform.GetChild(i).gameObject;
            tem[i] = sc_shooter[i].transform;
        }
        sc_shooter[0].transform.localPosition = new Vector2(-3.5f,3f);
        sc_shooter[1].transform.localPosition = new Vector2(3.5f,3f);
        Debug.Log(sc_shooter[1].transform.position);
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

        resetPos();
        section++;
    }
    
    
}
