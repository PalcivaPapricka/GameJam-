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




}
