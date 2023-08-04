using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class abstractBoss : MonoBehaviour
{
    private bool MoveCheck,noDemageMode;
    private Vector2 moveAccel;
    private Dictionary<string,float> timers;
    protected bool actionCheck,recoverCheck,useCard,bonusCheck;
    protected int currentHp,MaxHp,lowHp,section;
    protected bossPicPerform bp;
    protected spellCardPerform sp;
    protected Rigidbody2D rb;
    protected Clock clock;
    [SerializeField] hpBar healthBar;
    
    private void FixedUpdate() 
    {
        #region countTimer
        List<string> tem = new List<string>();
        if(timers != null)
        {
            foreach(var timer in timers) tem.Add(timer.Key);
            for(int i=0;i<tem.Count;i++) 
            {
                timers[tem[i]] = timers[tem[i]] - Time.fixedDeltaTime;
                if(timers[tem[i]]<=0) timers.Remove(tem[i]);
            }
        }  
        #endregion
        
        #region noDemageMode
        if(noDemageMode&&!checkTimer("OPMode")) noDemageMode = false;
        #endregion
        recover();
        slowDown(moveAccel);
    }
    private void slowDown(Vector2 accel) //slowDown to velocity is zero
    {
        if(checkTimer("Move")) rb.velocity += accel*Time.fixedDeltaTime;
        else 
        {
            rb.velocity = new Vector2(0f,0f);
            moveAccel = new Vector2(0f,0f);
            MoveCheck = false;
        }
    }
    private void recover()
    {
        if(recoverCheck)
        {
            float recoverValue = ((float)MaxHp)/0.5f;
            currentHp += (int)recoverValue;
            healthBar.setHP(currentHp);
            if(currentHp>=MaxHp)
            {
                currentHp = MaxHp;
                healthBar.setHP(MaxHp);
                recoverCheck = false;
                noDemageMode = false;
            }
        }
        else return;
    }
    
    protected abstract void die();
    protected abstract void resetPos();
    protected bool checkTimer(string name)
    {
        return timers.ContainsKey(name);
    }
    protected void slowDownMove(Vector2 displace,float time)
    {
        if(MoveCheck)
        {
            Debug.LogWarning("can't set");
            return ;
        }
        setTimer("Move",time);
        moveAccel = displace*(-2f)/(time*time);
        rb.velocity = new Vector2(moveAccel.x*(-time),moveAccel.y*(-time));
        MoveCheck = true;
    } 
    protected void init(int MaxHealth) // initiate boss and set maxHp to maxHealth
    {
        timers = new Dictionary<string, float>();
        rb = GetComponent<Rigidbody2D>();
        bp = bossPicPerform.instance;
        sp = spellCardPerform.instance;
        clock = Clock.clockInstance;
        section = 0;
        MaxHp = MaxHealth;
        currentHp = MaxHp;
        noDemageMode = false;
        MoveCheck = false;
        useCard = false;
        actionCheck = false;
        recoverCheck = false;
        bonusCheck = false;
        healthBar.setHpBar(MaxHp);
        healthBar.setHP(currentHp);
    }
    protected void setTimer(string name,float time)
    {
        if(timers==null) 
        {
            Debug.Log("initial first! you foolish");
            return;
        }
        timers.Add(name,time);
    }
    protected void startRecover()
    {
        noDemageMode = true;
        recoverCheck = true;
    }
    protected void setLowHp(int low)
    {
        lowHp = low;
    }
    protected void OPMode(float time)
    {
        noDemageMode = true;
        setTimer("OPMode",time);
    }
    
    public abstract void active();
    public bool isRun()
    {
        return actionCheck;
    }
    public bool isMove()
    {
        return MoveCheck;
    }
    public void takeDamage(int damage)
    {
        if(noDemageMode) return;
        if(currentHp<=lowHp)
        {
            currentHp = lowHp;
            return;
        }
        currentHp -= damage;
        healthBar.setHP(currentHp);
    }
    public void showUp()
    {
        slowDownMove(new Vector2(0,4)-(Vector2)transform.position,0.5f);
    }
    public void summonDrop(int count, string type){
        GameObject cb;
        for(int i=0; i<count; i++){
            cb = Instantiate(StageObj.Collectables[type], transform.position, transform.rotation);
            cb.transform.Translate(Random.Range(-0.3f, 0.3f), 0, 0);
            cb.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(2f, 5f));
        }
    }
}
