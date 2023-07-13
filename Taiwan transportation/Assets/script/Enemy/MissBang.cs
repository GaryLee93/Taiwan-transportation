using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissBang : abstractBoss
{
    [SerializeField] GameObject normal_bullet_1;
    [SerializeField] GameObject backMirror;
    Animator an;
    Vector2 oriPos = new Vector2(0,4);
    private enum SpellCard {glassRain,brockenCar,spellCardNum}
    bool[,] sectionCheck = new bool[(int)SpellCard.spellCardNum,2]; 
    //check whether a spellCard is actived, 0 for normal attack,1 for spellcard itself 
    void Start()
    {
        an = GetComponent<Animator>();
        for(int i=0;i<(int)SpellCard.spellCardNum;i++)
        {
            sectionCheck[i,0] = false;
            sectionCheck[i,1] = false;
        }
        recoverCheck=false;
        oriPos = new Vector2(0,4);
        base.init(1050,262);
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
            if(section==(int)SpellCard.glassRain)
            {
                GlassRain();
            }
            else if(section==(int)SpellCard.brockenCar)
            {
                BrockenCar();
            }
            else if(section==(int)SpellCard.spellCardNum) actionCheck = false;
        }
    }
    void GlassRain()
    {
        if(!sectionCheck[(int)SpellCard.glassRain,0] && !useCard)
        {
            StartCoroutine(normalAtk_1(5f));
            sectionCheck[(int)SpellCard.glassRain,0] = true;
        }
        else if(!sectionCheck[(int)SpellCard.glassRain,1] && useCard)
        {
            bp.startMove(0.75f,1f);
            sp.startSmallize("鏡符「萬元的玻璃雨」",0.75f,1f);
            StartCoroutine(glassRain(5f));
            sectionCheck[(int)SpellCard.glassRain,1] = true;
        }
    }
    void BrockenCar()
    {
        if(!sectionCheck[(int)SpellCard.brockenCar,0] && !useCard)
        {
            StartCoroutine(normalAtk_2(35f));
            sectionCheck[(int)SpellCard.brockenCar,0] = true;
        }
        else if(!sectionCheck[(int)SpellCard.brockenCar,1] && useCard)
        {
            bp.startMove(0.75f,1f);
            sp.startSmallize("車符「破蓋天鳴掌」",0.75f,1f);
            StartCoroutine(brockenCar(40f));
            sectionCheck[(int)SpellCard.brockenCar,1] = true;
        }
    }
    protected override void resetPos() // not complete yet
    {
        startRecover();
        slowDownMove(oriPos - (Vector2)transform.position,0.5f);
    }
    IEnumerator normalAtk_1(float time)
    {
        //wait for preset over
        yield return new WaitWhile(() => recoverCheck==true);
        yield return new WaitWhile(() => isMove()==true);

        GameObject shooter = transform.GetChild(0).gameObject;
        GameObject clone;
        int layer=0,bulletEachCol=5,openAngle=150,oritation=1,colNum=10;
        float bulletInterval = 0.4f;
        shooter.transform.localPosition = new Vector2(0,0);

        setTimer("normalAtk_1",time);
        while(checkTimer("normalAtk_1") && currentHp>lowHp)
        {
            slowDownMove(new Vector2(oritation*5,4)-(Vector2)transform.position,1f);
            yield return new WaitWhile(()=> isMove()==true);

            Vector2 dire = new Vector2(0,-1);
            Transform t = shooter.transform;

            for(int i=0;i<colNum;i++)
            {
                yield return new WaitForSeconds(0.2f);
                for(int k=0;k<bulletEachCol;k++)
                {
                    clone = objectPooler.spawnFromPool("enemy_bullet",(Vector2)t.position+(dire*(1+bulletInterval*k)),t.rotation,null);
                    clone.GetComponent<SpriteRenderer>().sortingOrder = layer;
                    clone.GetComponent<Rigidbody2D>().velocity = dire*5;
                    layer++;
                }
                dire = ourTool.rotate_vector(dire,(-1)*oritation*(openAngle/colNum));
            }
            oritation *= (-1);
        }

        // finish
        useCard = true;
        resetPos();
    }
    IEnumerator normalAtk_2(float time)
    {
        //wait for preset over
        yield return new WaitWhile(() => recoverCheck==true);
        yield return new WaitWhile(() => isMove()==true);
        
        GameObject shooter = transform.GetChild(0).gameObject;
        GameObject colone;
        Vector2 dire = new Vector2(1,0);
        int layer=0,bulletEachCircle=20;
        shooter.transform.localPosition = new Vector3(0,0,0);
        setTimer("normalAtk_2",time);
        while(checkTimer("normalAtk_2")&&currentHp>lowHp)
        {
            for(int i=0;i<bulletEachCircle;i++)
            {
                yield return new WaitForSeconds(0.05f);
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
        resetPos();
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
            Vector2 dire = new Vector2(1,0)*10;
            colone = Instantiate(backMirror,shooter.transform.position,shooter.transform.rotation);
            int chose = Random.Range(1,3);
            if(chose==1) dire = ourTool.trans_matrix(dire,ourTool.eulerToRadian(Random.Range(30,80)));
            else if(chose==2) dire = ourTool.trans_matrix(dire,ourTool.eulerToRadian(Random.Range(100,150)));
            colone.GetComponent<Rigidbody2D>().velocity = dire;
            yield return new WaitForSeconds(3);
        }

        //finish
        resetPos();
        section++;
        useCard = false;
        sp.retriveTitle();
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
    }
    public override void active()
    {
        resetPos();
        actionCheck = true;
    }   
}
