using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    private GameObject player;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");


          int enemiesLayer = LayerMask.NameToLayer("enemy");
          int projectileLayer = LayerMask.NameToLayer("player_projectile");
          int enprojectile = LayerMask.NameToLayer("enemy_projectile");
       
       
            Physics2D.IgnoreLayerCollision(enemiesLayer, enprojectile, true);
            Physics2D.IgnoreLayerCollision(projectileLayer,enprojectile,  true);
       
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player.GetComponent<Clippy>().player_health -= 10;

        }

        Destroy(gameObject);
    }
}


