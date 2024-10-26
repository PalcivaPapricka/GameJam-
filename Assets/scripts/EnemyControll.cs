using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyControll : MonoBehaviour
{
    public EnemyData enemyData;
    private EnemyMove em;

    private GameObject bomber;
    private GameObject brain;
    private GameObject arrow;

    private GameObject player;

    private int currentHealth;
    private Rigidbody2D rb;

    private Animator anim;

    private Coroutine explosionCoroutine;
    public Color colliderColor = Color.red;

    private ParticleSystem particleEffect;


    private bool isCollidingWithPlayer = false;


    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public float flashDuration = 0.1f; // Duration of the red flash


    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        em = GetComponent<EnemyMove>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        bomber = GameObject.FindWithTag("bomber");
        brain = GameObject.FindWithTag("brain");
        arrow = GameObject.FindWithTag("arrow");

        rb = GetComponent<Rigidbody2D>();
        currentHealth = enemyData.health;
        InitializeEnemy();

        

        anim=GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

 
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is missing on: " + gameObject.name);
        }


        particleEffect = GetComponentInChildren<ParticleSystem>();

        if (particleEffect == null)
        {
            Debug.LogError("No ParticleSystem found in the child objects!");
        }
        
        
        

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
        
    }

    public void CheckDistance()
    {
        float dis = (this.transform.position - player.transform.position).magnitude;
        //Debug.Log("HURAAAAA: " + dis);
        if (dis <= 14)
        {
            float damage = 50 * (1 - (dis / 20));

            // Apply the damage to the player's health
            player.GetComponent<Clippy>().player_health -= damage ;
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
                spriteRenderer.enabled = false;
                bomber.layer = LayerMask.NameToLayer("explosion");
            }

            CheckDistance();
            Die(); // Call the Die method to perform the explosion
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
        if(this.tag == "brain")
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
            anim.SetBool("death",true);
            //getComponent()
            Die();
        }
    }

    private void Die()
    {
        // Destroy enemy and add effects or animations if needed
        Destroy(gameObject,0.5f);
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
