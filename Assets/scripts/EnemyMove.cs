using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private GameObject player;
    private Transform positionPl;
    [SerializeField]
    private float followSpeed;
    public int keep_distance;
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
        //StraightMove();
        FacePlayer();
        keepDistance();
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

    // Rotate enemy to Player
     private void FacePlayer()
    {
        if (positionPl != null)
        {
            // Calculate the direction vector from the enemy to the player
            Vector2 directionToPlayer = (positionPl.position - transform.position).normalized;

            // Calculate the angle in degrees
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

            // Constrain the rotation so it doesn’t flip upside down
            if (angle > 90 || angle < -90)
            {
                // Flip the enemy sprite instead of rotating upside down
                transform.localScale = new Vector3(-1, 1, 1);
                angle += 180; // Adjust the angle by 180 degrees
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            // Apply rotation to face the player
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    // Range attack for enemies, follow the player and stop at a certain distance
    private void keepDistance()
    {
        
        float distance_from_player;

        if (positionPl != null)
        {
            // Calculate the distance between the enemy and the player
            distance_from_player = Vector3.Distance(transform.position, positionPl.position);
            if (distance_from_player > keep_distance)
            {
                transform.position = Vector2.MoveTowards(transform.position, positionPl.position, followSpeed * Time.deltaTime * 5);
            }

            // Optional: print the distance to the console for debugging
            //Debug.Log("Distance to Player: " + distance_from_player);
        }


    }
}
