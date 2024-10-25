using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    private GameObject player;
    private Transform positionPl;
    public float followSpeed = 0.5f;
    public Vector3 offset;

    public void Start()
    {
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
        //spravit mopvement na zigzag
    }

    public void StraightMove()
    {
        if (positionPl != null)
        {
            Vector3 targetPosition = positionPl.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
        
        
    }   




}
