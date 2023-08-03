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
    public static Vector2 rotate_vector(Vector2 orign,float angle)
    {
        float radian = eulerToRadian(angle);
        float new_a = orign.x*Mathf.Cos(radian) - orign.y*Mathf.Sin(radian);
        float new_b = orign.x*Mathf.Sin(radian) + orign.y*Mathf.Cos(radian);
        return new Vector2(new_a,new_b);
    }
    public static Vector2 vectorToPlayer(GameObject obj)
    {
        if(Player.GetPlayer()==null) return new Vector2(0,0);
        return Player.GetPlayer().transform.position - obj.transform.position;
    }
    public static float eulerToRadian(float euler)
    {
        return (euler/180)*Mathf.PI;
    }
    public static float radianToEuler(float radian)
    {
        return  radian/Mathf.PI*180;
    }
    public static float angleOfVectors(Vector2 v1,Vector2 v2)
    {
        float innerProduct = v1.x*v2.x+v1.y*v2.y;
        innerProduct /= (v1.magnitude*v2.magnitude);

        return Mathf.Acos(innerProduct);
    }
}
