using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    /*
    private GameObject player;
    private Transform positionPl;
    [SerializeField]
    private float followSpeed;
    public Vector3 offset;
    private Vector3 direction = Vector3.up;
    private Rigidbody2D rb;
    private Rigidbody2D bulletrb;
    public GameObject bullet;
    public GameObject spawn;

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
        StraightAttack();
    }


    public void StraightAttack()
    {
        if (positionPl != null)
        {

            GameObject followBullet = (GameObject)Instantiate(bullet, spawn.transform.position, Quaternion.identity);

            

            if (followBullet != null)
            {
                bulletrb = followBullet.GetComponent<RigidBody2D>();

                bulletrb.linearVelocity = Vector3.zero;
                transform.position = Vector2.MoveTowards(transform.position, positionPl.position, followSpeed * Time.deltaTime * 5);
                direction.x = 0;
                direction.y = 0;
            }

            
        }


    }

    */
}
