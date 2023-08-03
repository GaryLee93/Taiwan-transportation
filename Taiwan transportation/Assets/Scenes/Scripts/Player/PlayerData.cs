using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public int power=0;
    public int powerMode = 0;
    public float normal_speed = 7f;
    public float slow_speed = 3f;
    public int remain_life = 3;
    public int bomb_count = 3;
    public int highestScore;
    public int score = 0;
    public void dataInit()
    {
        score = 0;
        bomb_count = 3;
        remain_life = 3;
        power = 0;
    }
}
