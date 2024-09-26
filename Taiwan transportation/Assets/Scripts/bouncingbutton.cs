using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bouncingbutton : MonoBehaviour
{
    [SerializeField] float XBORDER, YBORDER;
    [SerializeField] float initSpeed;
    RectTransform rt;
    private void Start() {
        GetComponent<Rigidbody2D>().velocity = initSpeed* ourTool.rotate_vector(Vector2.right, Random.Range(30f, 60f));
        rt = GetComponent<RectTransform>();
    }
    void FixedUpdate(){
        if(rt.anchoredPosition.x > XBORDER || rt.anchoredPosition.x < -XBORDER){
            Vector2 tmp = GetComponent<Rigidbody2D>().velocity;
            tmp.x *= -1;
            GetComponent<Rigidbody2D>().velocity = tmp;
            GetComponent<Image>().color = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
        }
        if(rt.anchoredPosition.y >= YBORDER || rt.anchoredPosition.y <= -YBORDER){
            Vector2 tmp = GetComponent<Rigidbody2D>().velocity;
            tmp.y *= -1;
            GetComponent<Rigidbody2D>().velocity = tmp;
            GetComponent<Image>().color = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
        }
    }
}
