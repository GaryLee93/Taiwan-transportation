using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissBang : MonoBehaviour
{
    Rigidbody2D rb;
    Animator an;
    move_certain_destination move_instance;
    objectPooler obj_instance;
    public GameObject normal_bullet_1;
    bool time_c=false,nor_sh_1_c=false; // use to check whether a courtine is over 
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        an = gameObject.GetComponent<Animator>();
        move_instance = gameObject.GetComponent<move_certain_destination>();
        obj_instance = objectPooler.instance;
        StartCoroutine(normal_shoot_2());
    }
    IEnumerator move() // just for test
    {
        StartCoroutine(move_instance.move(new Vector2(5,5),3,rb));
        yield return new WaitWhile(() => Mathf.Abs(rb.velocity.x)>0.1f); //check whether velocity of x_axis is still
        StartCoroutine(move_instance.move(new Vector2(-5,5),3,rb));
    }

    void Update()
    {
        an.SetFloat("x_velocity",rb.velocity.x);
    }
    IEnumerator timer(float second)
    {
        time_c = true;
        yield return new WaitForSeconds(second);
        time_c = false;
    }
    IEnumerator normal_shoot_1()
    {
        nor_sh_1_c = true;
        GameObject shooter = transform.GetChild(0).gameObject;
        orbit_half_cycle or = shooter.GetComponent<orbit_half_cycle>();
        float rotate_times = 1f;
        GameObject colone;
        int layer=0;
        Queue<Vector3> tem_0 = new Queue<Vector3>(); 
        Queue<Quaternion> tem_1 = new Queue<Quaternion>();
        or.shooter_rotate_v = 900f;
        or.enabled = true;
        or.constraint = false;
        StartCoroutine(timer(rotate_times*360/(shooter.GetComponent<orbit_half_cycle>().shooter_rotate_v)+0.05f));
        /* (rotate_times*360/(shooter.GetComponent<rotate_enemy>().shooter_rotate_v)) is the time 
        we need to rotate shooter and instantiate bullet*/

        if(!shooter.activeSelf) shooter.SetActive(true);
        while(time_c)
        {
            yield return new WaitForSeconds(0.005f);
            tem_0.Enqueue(shooter.transform.position);
            tem_1.Enqueue(shooter.transform.rotation); 
        }

        while(tem_1.Count>0)
        {
            colone = obj_instance.spawnFromPool("orange_bullet",tem_0.Dequeue(),tem_1.Dequeue()
            ,gameObject);
            colone.GetComponent<SpriteRenderer>().sortingOrder = layer;
            layer ++;
        }
        shooter.SetActive(false);
        nor_sh_1_c = false;
    }
    IEnumerator normal_atk_1() // still need to adjust
    {
        StartCoroutine(move_instance.move(new Vector2(0,5),4,rb));
        yield return new WaitWhile(() => Mathf.Abs(rb.velocity.x)>0.1f || 
        Mathf.Abs(rb.velocity.y)>0.1f);
        StartCoroutine(normal_shoot_1());
        yield return new WaitWhile(() => nor_sh_1_c==true);

        StartCoroutine(move_instance.move(new Vector2(4,5),6,rb));
        yield return new WaitWhile(() => Mathf.Abs(rb.velocity.x)>0.1f || 
        Mathf.Abs(rb.velocity.y)>0.1f);
        StartCoroutine(normal_shoot_1());
        yield return new WaitWhile(() => nor_sh_1_c==true);
        
        StartCoroutine(move_instance.move(new Vector2(-4,5),8,rb));
        yield return new WaitWhile(() => Mathf.Abs(rb.velocity.x)>0.1f || 
        Mathf.Abs(rb.velocity.y)>0.1f);
        StartCoroutine(normal_shoot_1());
    }
    IEnumerator normal_shoot_2()
    {
        GameObject shooter = transform.GetChild(0).gameObject;
        GameObject colone;
        int layer = 0;
        shooter.transform.localPosition = new Vector3(0,0,0);
        StartCoroutine(timer(8));
        while(time_c)
        {
            yield return new WaitForSeconds(0.5f);
            for(int i=0;i<10;i++)
            {
                yield return new WaitForSeconds(0.05f);
                colone = obj_instance.spawnFromPool("purple_bullet",shooter.transform.position,
                shooter.transform.rotation,gameObject);
                colone.GetComponent<SpriteRenderer>().sortingOrder = layer;
                layer++;
            }
        }
    }
}
