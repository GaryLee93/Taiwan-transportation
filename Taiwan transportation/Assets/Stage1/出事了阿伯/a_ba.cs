using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_ba : abstractBoss
{
    GameObject player;
    Vector2 oriPos;

    bool[,] sectionCheck = new bool[(int)SpellCard.spellCardNum,2]; 
    string[,] spellCardName = new string[(int)SpellCard.spellCardNum,2];
    private enum SpellCard{riceSea,spellCardNum}
    void Start()
    {
        active();
    }
    void Update() 
    {
        action();
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
                if(i==colNum-1 && oritation>0) break;
                for(int j=0;j<bulletEachCol;j++)
                {
                    if(oritation>0)
                    {
                        clone = objectPooler.spawnFromPool("red_mid_round",(Vector2)tem.position+dire*(bulletInterval*j),tem.rotation);
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
    }
    IEnumerator riceAdditoin(float time)
    {
        yield return new WaitWhile(() => recoverCheck==true);
        yield return new WaitWhile(() => isMove()==true);
        GameObject shooter = transform.GetChild(0).gameObject,colone;
        Transform player_t=GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 dire;
        Transform shooterTrans = shooter.transform;
        int colNum=3,bulletEachCol=5;
        float bulletInterval=0.4f,openAngle=30f;
        shooterTrans.localPosition = new Vector2(0,0);

        setTimer("riceAdditoin",time);
        while(checkTimer("riceAdditoin") && currentHp>lowHp)
        {
            slowDownMove(new Vector2(player_t.position.x,transform.position.y)-(Vector2)transform.position,1f);
            yield return new WaitWhile(()=> isMove()==true);
            yield return new WaitForSeconds(0.8f);
            dire = ourTool.rotate_vector(new Vector2(0,-1),-(openAngle/2));
            int layer = 0;
            for(int i=0;i<colNum;i++)
            {
                for(int j=0;j<bulletEachCol;j++)
                {
                    colone = objectPooler.spawnFromPool("red_bullet",(Vector2)shooterTrans.position+dire*(1+bulletInterval*j),shooterTrans.rotation);
                    colone.GetComponent<SpriteRenderer>().sortingOrder = layer;
                    colone.GetComponent<Rigidbody2D>().velocity = dire*5;
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
        sc_shooter[0].transform.localPosition = new Vector2(-3.5f,5f);
        sc_shooter[1].transform.localPosition = new Vector2(3.5f,5f);

        setTimer("rice_sea",time);
        GameObject colone;
        int layer=0,correct=-1;
        while(checkTimer("rice_sea") && currentHp>lowHp)
        {
            yield return new WaitForSeconds(0.15f);
            sc_shooter[0].transform.position = new Vector3((-3.5f)+(1.5f)*Mathf.Sin(Time.time*2),tem[0].position.y,0);
            sc_shooter[1].transform.position = new Vector3((3.5f)+(1.5f)*Mathf.Sin(Time.time*2),tem[1].position.y,0);
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
    protected override void resetPos() // not complete yet
    {
        if(useCard) sp.retriveTitle();
        startRecover();
        slowDownMove(oriPos-(Vector2)transform.position,0.5f);
        useCard = !useCard;
    }
    void RiceSea()
    {
        if(!sectionCheck[(int)SpellCard.riceSea,0] && !useCard)
        {
            StartCoroutine(normal_attack(10f));
            sectionCheck[(int)SpellCard.riceSea,0] = true;
        }
        else if(!sectionCheck[(int)SpellCard.riceSea,1] && useCard)
        {
            sp.startSmallize("稻符「黃金雨」",1f,0.75f);
            bp.startMove("taxi",1f,0.75f);
            StartCoroutine(riceAdditoin(10f));
            StartCoroutine(rice_sea(10f));
            sectionCheck[(int)SpellCard.riceSea,1] = true;
        }
    }
    void action()
    {
        if(actionCheck)
        {
            if(section==(int)SpellCard.riceSea)
            {
                RiceSea();
            }
            else if(section==(int)SpellCard.spellCardNum) actionCheck = false;
        }
    }
    public override void active()
    {
        oriPos = new Vector2(0,4);
        for(int i=0;i<(int)SpellCard.spellCardNum;i++)
        {
            sectionCheck[i,0] = false;
            sectionCheck[i,1] = false;
        }
        spellCardName[0,0] = "normal_attack";
        spellCardName[0,1] = "rice_sea";
        init(1050,262);
        slowDownMove(oriPos-(Vector2)transform.position,0.5f);
        useCard = false;
        actionCheck = true;
    }
}
