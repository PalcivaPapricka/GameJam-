using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGeneration : MonoBehaviour
{
    private GameObject cam;
    private Transform cam_trans;
    private Vector3 cam_pos;

    // Chunk and rendering settings
    public int chunk_size = 10;
    public int max_render_distance = 2;

    // Tilemap and Tile
    public Tilemap tilemap;           // Reference to the Tilemap component
    public TileBase customTile;       // Reference to the custom tile (assigned in the inspector)

    private Vector2Int current_chunk;

    void Start()
    {
        // Find the main camera
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        if (cam != null)
        {
            cam_trans = cam.GetComponentInParent<Transform>();
        }

        // Initialize the current chunk
        current_chunk = new Vector2Int(int.MaxValue, int.MaxValue);
    }

    void Update()
    {
        if (cam != null)
        {
            // Track camera position
            cam_pos = cam_trans.position;

            // Calculate the chunk the camera is in
            Vector2Int new_chunk = new Vector2Int(
                Mathf.FloorToInt(cam_pos.x / chunk_size),
                Mathf.FloorToInt(cam_pos.y / chunk_size)
            );

            // Generate new chunks if camera has moved into a new chunk
            if (new_chunk != current_chunk)
            {
                current_chunk = new_chunk;
                generate_chunks();
            }
        }
    }

    // Generate the chunks around the current chunk
    public void generate_chunks()
    {
        for (int x = -max_render_distance; x <= max_render_distance; x++)
        {
            for (int y = -max_render_distance; y <= max_render_distance; y++)
            {
                Vector2Int chunk_coord = new Vector2Int(current_chunk.x + x, current_chunk.y + y);
                generate_chunk(chunk_coord);
            }
        }
    }

    // Generate an individual chunk
    public void generate_chunk(Vector2Int chunk_coord)
    {
        // Loop through the grid positions in the chunk and place tiles
        for (int x = 0; x < chunk_size; x++)
        {
            for (int y = 0; y < chunk_size; y++)
            {
                Vector3Int tilePosition = new Vector3Int(chunk_coord.x * chunk_size + x, chunk_coord.y * chunk_size + y, 0);
                tilemap.SetTile(tilePosition, customTile);  // Set the custom tile at the grid position
            }
        }
    }
}
