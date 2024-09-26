using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpBar : MonoBehaviour
{
    [SerializeField] Slider sl;
    public void setHP(int health)
    {
        sl.value = health;
    }
    public void setHpBar(int maxHp)
    {
        sl.maxValue = maxHp;
    }
}
