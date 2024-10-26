using UnityEngine;
    

public class zoom : MonoBehaviour
{

     // Set the min and max size of the camera zoom
    public float minZoom = 2f;
    public float maxZoom = 10f;

    // Speed of zooming
    public float zoomSpeed = 2f;

    private Camera cam;

    void Start()
    {
        // Get the camera component attached to the GameObject
        cam = GetComponent<Camera>();

        // Ensure the camera is set to Orthographic mode
        if (!cam.orthographic)
        {
            Debug.LogWarning("CameraZoom script is designed for Orthographic cameras.");
        }
    }

    void Update()
    {
        // Get the mouse scroll wheel input (positive for zooming in, negative for zooming out)
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Calculate the new orthographic size based on scroll input and speed
        float newSize = cam.orthographicSize - scrollInput * zoomSpeed;

        // Clamp the new size to be within the min and max zoom limits
        cam.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
}
}