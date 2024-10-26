using UnityEngine;

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

    private void Start()
    {
        
        em = FindObjectOfType<EnemyMove>();
       

        bomber = GameObject.FindWithTag("bomber");
        arrow = GameObject.FindWithTag("arrow");

        rb = GetComponent<Rigidbody2D>();
        currentHealth = enemyData.health;
        InitializeEnemy();

        anim=GetComponent<Animator>();
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
            //em.StraightMove();
            //em.RandomShoot();
        }
    }

    public void TakeDamage(int amount)
    {
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
        Destroy(gameObject,0.3f); //0.3f aby sa prehrala death animacia nesahajte na to !!
    }   

    private void OnDrawGizmosSelected()
    {
        // Visualize detection range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyData.detectionRange);
    }
}
