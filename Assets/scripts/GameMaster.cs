using UnityEngine;

public class GameMaster : MonoBehaviour
{

    public GameObject[] enemies;
    private EnemyMove em;
    public GameObject bomber;
    public GameObject spawn;

    public void Start()
    {


        //bomber = GameObject.FindWithTag("bomber");

        GameObject enemyBomber = (GameObject)Instantiate(bomber, spawn.transform.position, Quaternion.identity);
    }


    public void Update()
    {
        if (bomber != null)
        {
            bomber.GetComponent<EnemyMove>().StraightMove();
        }
    }

    
}

