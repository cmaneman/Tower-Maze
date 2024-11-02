using UnityEngine;

public class EndLevelScript : MonoBehaviour
{
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            //play sound
        }
    }
}

//When player touches portal, the player will be moved to the center of the portal and rotate(spin around) for 3-5 secs and disapper(using transparent properties or disable it).
