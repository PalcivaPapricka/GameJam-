using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health_pickup : MonoBehaviour
{
    private GameObject player; // Reference to the player GameObject
    private Clippy cli; // Reference to the Clippy script for health management
    private bool follow = false; // Flag to determine if the pickup should follow the player

    public float followDistance = 4f; // Distance to maintain from the player
    public float followSpeed = 25f; // Speed at which to follow the player

    void Start()
    {
        // Find the player GameObject by tag
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void Update()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        // Check if the player is assigned and follow is true
        if (player != null && follow)
        {
            // Calculate the distance to the player
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            // Move towards the player if not within followDistance
            if (distanceToPlayer > followDistance)
            {
                // Calculate direction to the player
                Vector3 direction = (player.transform.position - transform.position).normalized; 
                // Move towards the player
                transform.position += direction * followSpeed * Time.deltaTime; 
            }
            else
            {
                // Increase player's health
                player.GetComponent<Clippy>().player_health += 25;
                // Destroy the health pickup
               
                Destroy(gameObject); 
            }
        }

     
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        // Check if the collider is the player to start following
        if (col.CompareTag("Player"))
        {
            follow = true; // Start following the player
        }
    }

}
