using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{

    public GameObject[] enemies;
    private EnemyMove em;
    public GameObject bomber;
    public GameObject spawn;

    public void Start()
    {
        StartCoroutine(SpawnEnemiesInterval());

        //bomber = GameObject.FindWithTag("bomber");

        //GameObject enemyBomber = (GameObject)Instantiate(bomber, spawn.transform.position, Quaternion.identity);
    }

    private IEnumerator SpawnEnemiesInterval()
    // spawns enemy every period
    {
        while (true)
        {
            // Instantiate a new enemy bomber at the spawn position
            GameObject enemyBomber = Instantiate(bomber, spawn.transform.position, Quaternion.identity);
            // You can assign any necessary components or configurations to the enemy here if needed

            // Wait for the specified spawn interval before spawning the next enemy
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
    
}