using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    private GameObject player;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player.GetComponent<Clippy>().player_health -= 10;
            Destroy(gameObject);
        }
    }
}


