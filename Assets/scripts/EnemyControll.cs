using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyControll : MonoBehaviour
{
    public EnemyData enemyData;
    private EnemyMove em;

    private GameObject bomber;
    private GameObject arrow;

    private int currentHealth;
    private Rigidbody2D rb;

     private Animator anim;


    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public float flashDuration = 0.1f; // Duration of the red flash

    private void Start()
    { 
        em = GetComponent<EnemyMove>();

        bomber = this.gameObject;
        arrow = GameObject.FindWithTag("arrow");

        rb = GetComponent<Rigidbody2D>();
        currentHealth = enemyData.health;
        InitializeEnemy();

        anim=GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "arrow")
        {
            TakeDamage(20);
            Destroy(arrow);
        }
    }

    private void Patrol()
    {
        // Simple patrol movement (for example, left-right movement)
        if (bomber != null)
        {
            em.StraightMove();
        }
    }

    public void TakeDamage(int amount)
    {
        StartCoroutine(FlashRed());
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            anim.SetBool("death",true);
            Die();
        }
    }

    private void Die()
    {
        // Destroy enemy and add effects or animations if needed
        Destroy(gameObject,0.3f);
    }   

    private void OnDrawGizmosSelected()
    {
        // Visualize detection range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyData.detectionRange);
    }


    private IEnumerator FlashRed()
    {
        // Set color to red
        //spriteRenderer.color = Color.red;
        spriteRenderer.color = new Color(1f, 0f, 0f, 0.5f);
        // Wait for the flash duration
        yield return new WaitForSeconds(flashDuration);

        // Revert to original color
        spriteRenderer.color = originalColor;
    }
}
