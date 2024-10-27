using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    private GameObject cam;
    private Transform cam_trans;
    private Vector3 cam_pos;
    int chunk_size = 10;
    int max_render_distance = 2;

    private Vector2Int current_chunk;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        if (cam != null)
        {
            cam_trans = cam.GetComponentInParent<Transform>();
        }

        // Initialize the current chunk to be an invalid value (to force first load)
        current_chunk = new Vector2Int(int.MaxValue, int.MaxValue);
    }

    void Update()
    {
        if (cam != null)
        {
            cam_pos = cam_trans.position;
            //Debug.Log(cam_pos.x + " " + cam_pos.y);

            Vector2Int new_chunk = new Vector2Int(
                Mathf.FloorToInt(cam_pos.x / chunk_size),
                Mathf.FloorToInt(cam_pos.y / chunk_size)
            );
            if (new_chunk != current_chunk)
            {
                current_chunk = new_chunk;
                generate_chunks();
            }
        }
    }

    public void generate_chunks()
    {
        // Clear any existing chunks if necessary (optional depending on your generation strategy)

        // Loop through the chunks around the current chunk, based on render distance
        for (int x = -max_render_distance; x <= max_render_distance; x++)
        {
            for (int y = -max_render_distance; y <= max_render_distance; y++)
            {
                Vector2Int chunk_coord = new Vector2Int(current_chunk.x + x, current_chunk.y + y);
                generate_chunk(chunk_coord);
            }
        }
    }

    public void generate_chunk(Vector2Int chunk_coord)
    {
        // Convert chunk coordinates to world position
        Vector3 chunk_wold_pos = new Vector3(chunk_coord.x * chunk_size, chunk_coord.y * chunk_size, 0);

        Debug.Log("Generating chunk at: " + chunk_wold_pos);

        // Here, you can place logic to generate tiles, objects, or terrain in the chunk
        // For example, instantiate prefabs or place tiles in a Tilemap.

        // Example: Instantiate a simple cube to represent a chunk (for demonstration)
        GameObject chunk = GameObject.CreatePrimitive(PrimitiveType.Cube);
        chunk.transform.position = chunk_wold_pos;
        chunk.transform.localScale = new Vector3(chunk_size, chunk_size, 1); // Adjust size as needed
    }
}