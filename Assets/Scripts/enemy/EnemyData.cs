using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int health;
    public float speed;
    public int damage;
    public float detectionRange;
    public GameObject bullet;
    public int score;

}
