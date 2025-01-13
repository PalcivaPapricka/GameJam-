using UnityEngine;
using UnityEngine.Tilemaps;

public class colorShift : MonoBehaviour
{
    private Tilemap tm;
    private float hue;
    [SerializeField] public float hue_scroll_speed = 0.01f;

    void Start()
    {
        tm = GetComponentInParent<Tilemap>();
        hue = 0.5f;
    }

    void Update()
    {
        hue += Time.deltaTime * hue_scroll_speed;
        if (hue > 1f) hue -= 1f;
        tm.color = Color.HSVToRGB(hue, 1f, 1f);
    }
}