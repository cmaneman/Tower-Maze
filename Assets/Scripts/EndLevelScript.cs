using System.Collections;
using UnityEngine;

public class EndLevelScript : MonoBehaviour
{
    public GameObject EndLevelPortal;
    public AudioSource EndSound;
    private float rotationSpeed = 360f; // Degrees per second
    private float rotationDuration = 2f; // Duration in seconds

    [SerializeField] private Transform PlayerTransform;
    private Transform OriginalPlayerTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EndLevelPortal = gameObject;
        EndSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void EndCurrentLevel()
    {
        EndSound.Play();
        PlayerTransform = EndLevelPortal.transform;
        StartCoroutine(RotateForSeconds(rotationDuration));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            EndCurrentLevel();
        }
    }
    private IEnumerator RotateForSeconds(float duration) //The issue now is that the portal rotates but not the player...
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float rotationAmount = rotationSpeed * Time.deltaTime;
            PlayerTransform.Rotate(0, 0, rotationAmount); // Rotate on the X-axis 
            elapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
    }
}

//When player touches portal, the player will be moved to the center of the portal and rotate(spin around) for 3-5 secs and disapper(using transparent properties or disable it).
