using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_ba : MonoBehaviour
{
    public GameObject normal_bullet_1;
    public GameObject normal_bullet_2;
    public GameObject sc_bullet;
    objectPooler instance;
    Rigidbody2D rb;
    GameObject player;
    bool move_c=false,normal_atk_c=false,time_c=false,rice_c=false;
    int nor_atk_time=2,nor_shooter_num=4;
    
    void Start()
    {
        instance = objectPooler.instance;
        rb=gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(total_action());
    }
    IEnumerator total_action()
    {
        StartCoroutine(move(new Vector2(3,5),5));
        yield return new WaitWhile(() =>move_c==true);
        StartCoroutine(normal_attack());
        yield return new WaitWhile(() => normal_atk_c==true);
        StartCoroutine(move(new Vector2(-3,5),5));
        yield return new WaitWhile(() =>move_c==true);
        StartCoroutine(normal_attack());
        yield return new WaitWhile(() => normal_atk_c==true);
        StartCoroutine(move(new Vector2(0,6),5));
        yield return new WaitWhile(() =>move_c==true);
        StartCoroutine(sc_RiceSea());
    }
    IEnumerator move(Vector2 destination,int velocity)
    {
        move_c=true;
        Vector2 v = destination-(Vector2)transform.position;
        Vector2 dir = v.normalized;
        rb.velocity=dir.normalized*velocity;
        yield return new WaitForSeconds(v.magnitude/velocity);
        rb.velocity=new Vector2(0f,0f);
        move_c=false;
    }
    IEnumerator normal_attack()
    {
        normal_atk_c=true;
        GameObject[] nor_shooter = new GameObject[nor_shooter_num];
        orbit_half_cycle[] ro = new orbit_half_cycle[nor_shooter_num];
        Vector2[] ini_pos = new Vector2[nor_shooter_num];
        for(int i=0;i<nor_shooter_num;i++)
        {
            nor_shooter[i] = transform.GetChild(i).gameObject;
            ro[i] = nor_shooter[i].GetComponent<orbit_half_cycle>();
            ro[i].rotate_time=0;
            ini_pos[i] = nor_shooter[i].transform.position;
            ro[i].shooter_rotate_v = Mathf.Abs(ro[i].shooter_rotate_v);
            ro[i].enabled = true;
        }
        GameObject colone;
        while(ro[0].rotate_time<=nor_atk_time)
        {
            yield return new WaitForSeconds(0.075f);
            for(int k=0;k<nor_shooter_num;k++)
            {
                if(nor_shooter[k].activeSelf==false) nor_shooter[k].SetActive(true);

                if((ro[k].shooter_rotate_v < 0) && (ro[0].rotate_time<=nor_atk_time))
                {
                    colone = instance.spawnFromPool("orange_bullet",nor_shooter[k].transform.position,nor_shooter[k].transform.rotation,gameObject);
                    colone.transform.parent = gameObject.transform;
                }
                else if(ro[0].rotate_time<=nor_atk_time)
                {
                    colone = instance.spawnFromPool("red_bullet",nor_shooter[k].transform.position,nor_shooter[k].transform.rotation,gameObject);
                    colone.transform.parent = gameObject.transform;
                }
            }
        }
        for(int i=0;i<nor_shooter_num;i++) 
        {
            if(nor_shooter[i].activeSelf==true) nor_shooter[i].SetActive(false);
            nor_shooter[i].transform.position = ini_pos[i];
        }
        normal_atk_c=false;
    }
    IEnumerator timer(int second)
    {
        time_c = true;
        yield return new WaitForSeconds(second);
        time_c = false;
    }
    IEnumerator sc_RiceSea()
    {
        int shooter_num = 3;
        int move_state = 0;
        GameObject[] shooter = new GameObject[shooter_num];
        Vector2[] ini_pos = new Vector2[shooter_num];
        for(int i=0;i<shooter_num;i++) 
        {
            shooter[i] = gameObject.transform.GetChild(i).gameObject;
            shooter[i].GetComponent<orbit_half_cycle>().enabled=false;
            ini_pos[i] = shooter[i].transform.localPosition;
        }

        shooter[0].transform.localPosition = new Vector2(-0.2f,-1);
        shooter[1].transform.localPosition = new Vector2(0.2f,-1);
        shooter[2].transform.localPosition = new Vector2(0,-1);
        StartCoroutine(rice_sea());
        while(rice_c)
        {
            yield return new WaitForSeconds(0.7f);
            if(move_state==0) 
            {
                StartCoroutine(move(new Vector2(2,3),5));
                move_state = 1;
            }
            else if(move_state==1)
            {
                StartCoroutine(move(new Vector2(-2,3),7));
                move_state = 2;
            }
            else 
            {
                StartCoroutine(move(new Vector2(0,5),5));
                move_state = 0;
            }
            yield return new WaitWhile(() => move_c==true);

            GameObject colone;
            int layer = 0;
            for(int i=0;i<5;i++)
            {
                yield return new WaitForSeconds(0.1f);
                for(int j=0;j<shooter_num;j++)
                {
                    colone = instance.spawnFromPool("red_bullet",shooter[j].transform.position,
                    shooter[j].transform.rotation,gameObject);
                    colone.GetComponent<SpriteRenderer>().sortingOrder = layer;
                    layer++;
                }
            }
        }

        for(int i=0;i<2;i++) 
        {
            shooter[i].transform.localPosition = ini_pos[i];
            shooter[i].GetComponent<orbit_half_cycle>().enabled=true;
        }
    }
    IEnumerator rice_sea()
    {
        rice_c=true;
        yield return new WaitForSeconds(1f); 
        GameObject sc_manager = transform.GetChild(4).gameObject;
        GameObject[] sc_shooter = new GameObject[2];
        for(int i=0;i<2;i++) sc_shooter[i] = sc_manager.transform.GetChild(i).gameObject;
        sc_shooter[0].transform.position = new Vector2(-3.5f,6f);
        sc_shooter[1].transform.position = new Vector2(3.5f,6f);

        StartCoroutine(timer(15));
        GameObject colone;
        int layer=0;
        while(time_c)
        {
            yield return new WaitForSeconds(0.15f);
            sc_shooter[0].transform.position = new Vector3((-3.5f)+(1.5f)*Mathf.Sin(Time.time*2),6,0);
            sc_shooter[1].transform.position = new Vector3((3.5f)+(1.5f)*Mathf.Sin(Time.time*2),6,0);

            colone = instance.spawnFromPool("rice",sc_shooter[0].transform.position,sc_shooter[0].transform.rotation,gameObject);
            colone.transform.Rotate(new Vector3(0,0,-90),Space.Self);
            colone.GetComponent<Rigidbody2D>().velocity = new Vector3(0,-5,0);
            colone.GetComponent<SpriteRenderer>().sortingOrder = layer;
            layer++;

            colone = instance.spawnFromPool("rice",sc_shooter[0].transform.position+new Vector3(-2,0,0),sc_shooter[0].transform.rotation,gameObject);
            colone.transform.Rotate(new Vector3(0,0,-90),Space.Self);
            colone.GetComponent<Rigidbody2D>().velocity = new Vector3(0,-5,0);
            colone.GetComponent<SpriteRenderer>().sortingOrder = layer;
            layer++;

            colone = instance.spawnFromPool("rice",sc_shooter[1].transform.position,sc_shooter[1].transform.rotation,gameObject);
            colone.transform.Rotate(new Vector3(0,0,90),Space.Self);  
            colone.GetComponent<Rigidbody2D>().velocity = new Vector3(0,-5,0);
            colone.GetComponent<SpriteRenderer>().sortingOrder = layer;
            layer++;

            colone = instance.spawnFromPool("rice",sc_shooter[1].transform.position+new Vector3(2,0,0),sc_shooter[0].transform.rotation,gameObject);
            colone.transform.Rotate(new Vector3(0,0,90),Space.Self);
            colone.GetComponent<Rigidbody2D>().velocity = new Vector3(0,-5,0);
            colone.GetComponent<SpriteRenderer>().sortingOrder = layer;
            layer++;     
        }
        rice_c = false;
    }

}
