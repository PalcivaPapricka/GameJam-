using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int health;
    public float speed;
    public int damage;
    public float detectionRange;

    public void Tudu()
    {
        Debug.Log("TUDUUUUUU");
    }

}
