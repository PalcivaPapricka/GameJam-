using UnityEngine;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyControll : MonoBehaviour
{
    public EnemyData enemyData;
    private EnemyMove em;

    private GameObject bomber;
    private GameObject brain;
    private GameObject arrow;   //WTF is arrow ????

    private GameObject player;

    private int currentHealth;
    private Rigidbody2D rb;

    private Animator anim;

    private Coroutine explosionCoroutine;
    public Color colliderColor = Color.red;

    private ParticleSystem particleEffect;


    private bool isCollidingWithPlayer = false;


    private SpriteRenderer spriteRenderer;
    private Collider2D collider2d;
    private Color originalColor;
    public float flashDuration = 0.1f; // Duration of the red flash

    //brain attack TODO: needs to be done moved to attack script
    public GameObject bullet;
    private float time_of_last_shot = 0.0f;
    public float bullet_life_time = 25f;
    public float bullet_speed = 15;
    public float bullet_fire_rate = 1;
    // bullet function
    private void BasicConstantRangedAttackt()
    {
        // wait certain time between shots
        time_of_last_shot += Time.deltaTime;
        if (time_of_last_shot >= 1 / bullet_fire_rate)
        {
            time_of_last_shot = 0;

            // Instantiate the bullet
            GameObject blob_fired = Instantiate(bullet, transform.position, Quaternion.identity);
            // Calculate shooting direction
            Vector3 shootDirection = (player.transform.position - transform.position).normalized;

            // Set the velocity of the bullet
            Rigidbody2D rb = blob_fired.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = shootDirection * bullet_speed; // Set the speed
            }

            // Rotate the blob to face the shoot direction
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
            blob_fired.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

            // Destroy the blob after x seconds
            Destroy(blob_fired, bullet_life_time);
        }
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        em = GetComponent<EnemyMove>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2d = GetComponent<Collider2D>();

        bomber = GameObject.FindWithTag("bomber");
        arrow = GameObject.FindWithTag("arrow");

        rb = GetComponent<Rigidbody2D>();
        currentHealth = enemyData.health;
        InitializeEnemy();


        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;


        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is missing on: " + gameObject.name);
        }


        particleEffect = GetComponentInChildren<ParticleSystem>();
    }


    private void InitializeEnemy()
    {
        if (enemyData == null)
        {
            Debug.LogError("No Enemy Data assigned to " + gameObject.name);
            return;
        }
    }


    private void Update()
    {
        // Sample movement code (could use AI pathfinding or other logic)
        Patrol();

        if (tag == "brain")
        {
            BasicConstantRangedAttackt(); // debug code TODO: implement this better
        }   
    }

    public void CheckDistance()
    {
        float dis = (this.transform.position - player.transform.position).magnitude;
        //Debug.Log("HURAAAAA: " + dis);
        if (dis <= 14)
        {
            float damage = 50 * (1 - (dis / 20));

            // Apply the damage to the player's health
            player.GetComponent<Clippy>().player_health -= damage;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "arrow")
        {

            TakeDamage(20);
            Destroy(arrow);
        }

        if (collision.gameObject.tag == "bomber" && this.tag == "brain")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            isCollidingWithPlayer = true;
            StartCoroutine(FlashWhite());
            // Start the explode coroutine only if it's not already running
            if (explosionCoroutine == null)
            {
                explosionCoroutine = StartCoroutine(explode());
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            isCollidingWithPlayer = false;

            // Stop the coroutine if the player exits
            if (explosionCoroutine != null)
            {
                StopCoroutine(explosionCoroutine);
                explosionCoroutine = null; // Reset the coroutine reference
            }
        }
    }


    private IEnumerator explode()
{
    yield return new WaitForSeconds(2);

    if (isCollidingWithPlayer)
    {
        if (particleEffect != null)
        {
            // Play the particle effect
            particleEffect.Play();
        }

        if (bomber != null) // Ensure bomber is not null before accessing its layer
        {
            bomber.layer = LayerMask.NameToLayer("explosion");
        }

        CheckDistance();

        // Ensure that Die() does not access any destroyed references
        if (gameObject != null) // Check if the object is still active
        {
            Die(); // Call the Die method to perform the explosion
        }
    }

    explosionCoroutine = null;
}


    private void Patrol()
    {
        // Simple patrol movement (for example, left-right movement)
        if (this.tag == "bomber")
        {
            em.StraightMove();
        }
        if (this.tag == "brain")
        {
            em.keepDistance();
        }
    }


    public void TakeDamage(int amount)
    {
        StartCoroutine(FlashRed());
        currentHealth -= amount;
        if (currentHealth <= 0)
        {

            anim.SetBool("death", true);


            //getComponent()
            Die();
        }
    }


    private void Die()
    {
        // Destroy enemy and add effects or animations if needed
        Destroy(gameObject, 0.5f);
    }


    private void OnDrawGizmosSelected()
    {
        // Visualize detection range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyData.detectionRange);
    }


    private IEnumerator FlashRed()
    {

        if (spriteRenderer == null)
        {

            yield break; // Exit if spriteRenderer is null
        }

        // Set color to red
        //spriteRenderer.color = Color.red;
        spriteRenderer.color = new Color(1f, 0f, 0f, 0.5f);
        // Wait for the flash duration
        yield return new WaitForSeconds(flashDuration);

        // Revert to original color
        spriteRenderer.color = originalColor;
    }

    private IEnumerator FlashWhite()
    {

        if (spriteRenderer == null)
        {

            yield break; // Exit if spriteRenderer is null
        }


        // Set color to red
        //spriteRenderer.color = Color.red;
        spriteRenderer.color = new Color(0f, 0f, 0f, 1f);
        // Wait for the flash duration
        yield return new WaitForSeconds(flashDuration);

        // Revert to original color
        spriteRenderer.color = originalColor;
    }


}
