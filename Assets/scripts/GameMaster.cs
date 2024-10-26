using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public GameObject clippy;
    public Transform clippySpawn;
    // spawning logic
    public List<GameObject> enemies_to_spawn;
    public int spawn_count = 4;
    public int spawn_at_distance = 10;
    public float spawn_wait_for = 0.5f;
    public int spawn_limit = 50;
    private List<Vector3> spawns = new List<Vector3>();


    public GameObject gameOver;


    public void Start()
    {
        //spawning enemies
        generate_spawns();
        StartCoroutine(SpawnEnemiesInterval());

        //disable gameover UI 
        gameOver = GameObject.FindWithTag("gameoverUI");
        gameOver.SetActive(false);

        //Spawn player
        GameObject cliSpawn = (GameObject)Instantiate(clippy, clippySpawn.position, Quaternion.identity);
        
    }

    private IEnumerator SpawnEnemiesInterval()
    // spawns enemy every period
    {
        while (true)
        {
            if (count_enemies() < spawn_limit) 
            { 
                // Instantiate a new enemy bomber
                Vector3 spawn_pos = clippy.transform.position + spawns[Random.Range(0, spawn_count)];
                GameObject enemy = Instantiate(enemies_to_spawn[Random.Range(0, enemies_to_spawn.Count)], spawn_pos, Quaternion.identity);
            }
            // Wait for the specified spawn interval before spawning the next enemy
            yield return new WaitForSeconds(spawn_wait_for);
        }
    }

    private void generate_spawns()
    {
        float angle = 0;
        for (int index=0; index< spawn_count; index++)
        {
            // Calculate spawn position based on the angle and distance
            float x = spawn_at_distance * Mathf.Cos(angle);
            float y = spawn_at_distance * Mathf.Sin(angle);
            spawns.Add(new Vector3(x, y, 0));

            angle += Mathf.PI * 2 / spawn_count;
        }

        //debug
        //for (int index = 0; index < spawns.Count; index++)
        //{
        //    Debug.Log(spawns[index]);
        //}
    }

    private int count_enemies()
    {
        int count = 0;
        // Find all objects with a Renderer component in the scene
        Renderer[] renderers = FindObjectsOfType<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            // Check if the Renderer is in the specified sorting layer
            if (renderer.sortingLayerName == "enemies")
            {
                count++;
            }
        }

        return count;
    }

    public void RestartGame()
    {
        Debug.Log("som tu");
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
    
}