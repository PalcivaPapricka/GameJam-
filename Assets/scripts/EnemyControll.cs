using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyControll : MonoBehaviour
{
    public EnemyData enemyData;
    private EnemyMove em;

    public GameObject bomber;

    private float currentHealth;
    private Rigidbody2D rb;

    private void Start()
    {
        em = FindObjectOfType<EnemyMove>();
        enemyData = FindObjectOfType<EnemyData>();

        bomber = GameObject.FindWithTag(enemyData.enemyName);

        rb = GetComponent<Rigidbody2D>();
        InitializeEnemy();
    }

    private void InitializeEnemy()
    {
        if (enemyData == null)
        {
            Debug.LogError("No Enemy Data assigned to " + gameObject.name);
            return;
        }

        currentHealth = enemyData.health;
    }

    private void Update()
    {
        // Sample movement code (could use AI pathfinding or other logic)
        Patrol();
        if (bomber != null)
        {
            em.StraightMove();
            em.RandomShoot();
        }
    }

    private void Patrol()
    {
        // Simple patrol movement (for example, left-right movement)
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Destroy enemy and add effects or animations if needed
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize detection range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyData.detectionRange);
    }
}
