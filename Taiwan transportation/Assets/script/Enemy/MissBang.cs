using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissBang : MonoBehaviour
{
    [SerializeField] GameObject normal_bullet_1;
    [SerializeField] GameObject backMirror;
    Rigidbody2D rb;
    Animator an;
    objectPooler obj_instance;
    ModelMovement MM;
    Clock clock;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        an = gameObject.GetComponent<Animator>();
        MM = GetComponent<ModelMovement>();
        obj_instance = objectPooler.instance;
        clock = Clock.clockInstance;
        StartCoroutine(brockenCar());
    }
    void Update()
    {
        an.SetFloat("x_velocity",rb.velocity.x);
    }
    IEnumerator normalShoot_1()
    {
        GameObject[] shooter = new GameObject[3];
        GameObject clone,player = GameObject.FindGameObjectWithTag("Player");
        int layer=0,bulletEachCol=5;
        for(int i=0;i<3;i++) shooter[i] = transform.GetChild(i).gameObject;
        shooter[0].transform.localPosition = new Vector2(0,-0.5f);
        shooter[1].transform.localPosition = new Vector2(-0.5f,-0.5f);
        shooter[2].transform.localPosition = new Vector2(0.5f,-0.5f);

        clock.setTimer("timer",10f);
        while(clock.checkTimer("timer"))
        {
            Vector2 target = player.transform.position;
            for(int i=0;i<bulletEachCol;i++)
            {
                yield return new WaitForSeconds(0.05f);
                for(int j=0;j<3;j++)
                {
                    Vector2 dire = target-(Vector2)shooter[j].transform.position;
                    dire.Normalize();
                    clone = obj_instance.spawnFromPool("purple_bullet",shooter[j].transform.position,shooter[j].transform.rotation,null);
                    clone.GetComponent<SpriteRenderer>().sortingOrder = layer;
                    clone.GetComponent<Rigidbody2D>().velocity = dire*5;
                    layer++;
                }
            }
        } 
    }
    IEnumerator normalShoot_2()
    {
        GameObject shooter = transform.GetChild(0).gameObject;
        GameObject colone;
        Vector2 dire = new Vector2(1,0);
        int layer=0,bulletEachCircle=20;
        shooter.transform.localPosition = new Vector3(0,0,0);
        clock.setTimer("timer",5f);
        while(clock.checkTimer("timer"))
        {
            yield return new WaitForSeconds(0.5f);
            for(int i=0;i<bulletEachCircle;i++)
            {
                yield return new WaitForSeconds(0.05f);
                colone = obj_instance.spawnFromPool("purple_bullet",shooter.transform.position,
                shooter.transform.rotation,gameObject);
                colone.GetComponent<SpriteRenderer>().sortingOrder = layer;
                colone.GetComponent<Rigidbody2D>().velocity = 3*dire;
                dire = ourTool.trans_matrix(dire,ourTool.eulerToRadian(360/bulletEachCircle));
                layer++;
            }
        }
    }
    IEnumerator glassRain() 
    {
        GameObject shooter = transform.GetChild(0).gameObject;
        GameObject colone;
        shooter.transform.localPosition = new Vector3(0,0,0);
        
        clock.setTimer("glassRain",30f);
        while(clock.checkTimer("glassRain"))
        {
            Vector2 dire = new Vector2(1,0)*10;
            colone = Instantiate(backMirror,shooter.transform.position,shooter.transform.rotation);
            ModelMovement MM = colone.GetComponent<ModelMovement>();
            int chose = Random.Range(1,3);
            if(chose==1) dire = ourTool.trans_matrix(dire,ourTool.eulerToRadian(Random.Range(30,80)));
            else if(chose==2) dire = ourTool.trans_matrix(dire,ourTool.eulerToRadian(Random.Range(100,150)));
            MM.setMovement(1,dire,1);
            MM.startAll();
            yield return new WaitForSeconds(2);
        }
    }
    IEnumerator brockenCar()
    {
        GameObject [] shooter = new GameObject[3];
        GameObject clone;
        int bulletEachCircle=120,layer=0;

        for(int i=0;i<3;i++)
        {
            shooter[i] = transform.GetChild(i).gameObject;
            shooter[i].transform.localPosition = new Vector2(0,0);
        }
        for(int i=0;i<3;i++)
        {
            yield return new WaitForSeconds(0.2f);
            Vector2 dire = new Vector2(0,-1);
            for(int j=0;j<bulletEachCircle;j++)
            {
                shooter[i].transform.Rotate(new Vector3(0,0,360/bulletEachCircle));
                clone = obj_instance.spawnFromPool("aqua_bullet",shooter[i].transform.position,shooter[i].transform.rotation,null);
                clone.GetComponent<SpriteRenderer>().sortingOrder = layer;
                clone.GetComponent<Rigidbody2D>().velocity = dire*5;

                dire = ourTool.trans_matrix(dire,ourTool.eulerToRadian(360/bulletEachCircle));
                layer++;
            }
            
        }
    }
}
