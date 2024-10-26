using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject  player;
    private bool stopped = false;

   
    void Start()
    {
        
        
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    void OnCollisionEnter2D(Collision2D col)
    {

         if (col.gameObject.CompareTag("Player"))
        {
            // Ignore collision with the Player
            return;
        }

       
        Destroy(gameObject);
    }
}


