using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    public EnemyData enemyData;

    private float currentHealth;
    private Rigidbody2D rb;

    private void Start()
    {
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
    }

    private void Patrol()
    {
        // Simple patrol movement (for example, left-right movement)
        rb.linearVelocity = new Vector2(enemyData.speed, rb.linearVelocity.y);
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
