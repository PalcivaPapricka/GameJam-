using UnityEngine;

public class BackgroundMusic : MonoBehaviour {
    public AudioClip musicClip; // Assign your MP3 file here in the Inspector
    [Range(0f, 1f)] public float volume = 0.5f; // Adjustable volume slider in the Inspector

    private AudioSource audioSource;

    void Start() {
        // Create an AudioSource component if not already attached
        audioSource = gameObject.AddComponent<AudioSource>();

        if (musicClip != null) {
            audioSource.clip = musicClip;   // Set the audio clip
            audioSource.loop = true;       // Ensure it loops
            audioSource.playOnAwake = true; // Start playing when the scene loads
            audioSource.volume = volume;   // Set the initial volume
            audioSource.Play();            // Start the music
        }
        else {
            Debug.LogWarning("No music clip assigned to BackgroundMusic script.");
        }
    }

    void Update() {
        // Dynamically update the volume if needed
        audioSource.volume = volume;
    }
}
