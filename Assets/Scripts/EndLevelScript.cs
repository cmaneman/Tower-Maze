using System.Collections;
using UnityEngine;
public class EndLevelScript : MonoBehaviour
{
    public GameObject EndLevelPortal;
    public AudioSource EndSound;

    public TMPro.TMP_Text LevelComText; // Assign the Canvas GameObject in the Inspector

    private Transform portalOriginTransform;
    private float rotationSpeed = 360f; // Degrees per second
    private float rotationDuration = 2f; // Duration in seconds
    public bool isLevelEnded = false;

    [SerializeField] private Transform PlayerTransform;
    //private Transform OriginalPlayerTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EndLevelPortal = gameObject;
        portalOriginTransform = EndLevelPortal.transform;
        EndSound = GetComponent<AudioSource>();
        LevelComText.gameObject.SetActive(false);
        //LCUI_Text.levelCompleteText = GetComponentInChildren<TMPro.TMP_Text>();


        /*if (!LevelComText)
        {
            Debug.LogError("LvlCompleteText is not assigned in the inspector.");
        }
        else
        {
            Debug.Log("LvlCompleteText is assigned.");
        }*/
    }

    // Update is called once per frame
    public void EndCurrentLevel(Collider2D other)
    {
        //EndSound.Play();
        //other.gameObject.transform = EndLevelPortal.transform; //won't work...read only
        isLevelEnded = true;

        if (EndSound != null)
        {
            EndSound.Play();
        }
        else
        {
            Debug.LogError("EndSound is not assigned.");
        }

        StartCoroutine(RotateForSeconds(rotationDuration, other));

        if (LevelComText != null)
        {
            LevelComText.gameObject.SetActive(true);
            LevelComText.color = new Color(LevelComText.color.r, LevelComText.color.g, LevelComText.color.b, 0);
            StartCoroutine(FadeInText(LevelComText, 1f)); // Fade in over 1 second
        }
        else
        {
            Debug.LogError("LvlCompleteText is not assigned.");
        }

        //LvlCompleteText.ShowLevelCompleteScreen();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            EndCurrentLevel(other);
        }
    }
    private IEnumerator RotateForSeconds(float duration, Collider2D other) //The issue now is that the portal rotates but not the player...
    {
        float elapsed = 0f;
        var playerTransform = other.transform;
        playerTransform.position = portalOriginTransform.position;

        while (elapsed < duration)
        {
            float rotationAmount = rotationSpeed * Time.deltaTime;
            //other.gameObject.transform.Rotate(0, 0, rotationAmount); // Rotate on the Z-axis 
            playerTransform.transform.Rotate(0, 0, rotationAmount); // Rotate the portal
            elapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        playerTransform.gameObject.SetActive(false);
    }

    private IEnumerator FadeInText(TMPro.TMP_Text text, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float opacity = Mathf.Lerp(0f, 1f, elapsed / duration);
            text.color = new Color(text.color.r, text.color.g, text.color.b, opacity);
            elapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
    }
}

//When player touches portal, the player will be moved to the center of the portal and rotate(spin around) for 3-5 secs and disapper(using transparent properties or disable it).
