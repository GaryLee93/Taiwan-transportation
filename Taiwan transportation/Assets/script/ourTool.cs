using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ourTool
{
    public static Vector2 trans_matrix(Vector2 orign,float radian)
    {
        float new_a = orign.x*Mathf.Cos(radian) - orign.y*Mathf.Sin(radian);
        float new_b = orign.x*Mathf.Sin(radian) + orign.y*Mathf.Cos(radian);
        return new Vector2(new_a,new_b);
    }
    public static Vector2 vectorToPlayer(GameObject obj)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return obj.transform.position - player.transform.position;
    }
    public static float eulerToRadian(float euler)
    {
        return (euler/180)*Mathf.PI;
    }
    public static float radianToEuler(float radian)
    {
        return  radian/Mathf.PI*180;
    }
}
