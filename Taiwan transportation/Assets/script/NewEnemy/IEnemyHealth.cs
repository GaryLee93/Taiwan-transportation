using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyHealth{
    int health{get; set;}
    public void takeDamage(int damage);
}
