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

        // Ensure the enemies layer exists (layer index should be between 0 and 31)
        if (enemiesLayer >= 0 && enemiesLayer <= 31)
        {
            Physics2D.IgnoreLayerCollision(enemiesLayer, enemiesLayer, true);
        }
        else
        {
            Debug.LogError("Layer 'enemies' does not exist or is out of range. Check the layer setup in Unity.");
        }
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


