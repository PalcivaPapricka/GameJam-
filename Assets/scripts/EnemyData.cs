using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyData : ScriptableObject
{
    public string enemyName;
    public float health;
    public float speed;
    public float damage;
    public float detectionRange;

    public void Tudu()
    {
        Debug.Log("TUDUUUUUU");
    }

}
