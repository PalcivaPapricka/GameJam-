using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private GameObject player;
    private Transform positionPl;
    [SerializeField]
    private float followSpeed;
    public Vector3 offset;
    private Vector3 direction = Vector3.up;
    private Rigidbody2D rb;

    public void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            positionPl = player.transform;
         
          
        }
    }

    public void Update()
    {
        StraightMove();
        FacePlayer(); 
    }

    public void ZigZagMove()
    {
        if (player != null)
        {

        }
    }

    public void StraightMove()
    {
        if (positionPl != null)
        {
            rb.linearVelocity = Vector3.zero;
            transform.position = Vector2.MoveTowards(transform.position, positionPl.position, followSpeed * Time.deltaTime * 5);
            direction.x = 0;
            direction.y = 0;
        }
    }

     private void FacePlayer()
    {
        if (positionPl != null)
        {
            // Calculate the direction vector from the enemy to the player
            Vector2 directionToPlayer = (positionPl.position - transform.position).normalized;
            // Calculate the angle in degrees
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            // Rotate the enemy to face the player
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }   
}
