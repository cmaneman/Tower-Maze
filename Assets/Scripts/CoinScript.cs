using System.Collections;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public GameObject coin;
    public AudioSource coinSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coinSound = GetComponent<AudioSource>();
        coin = gameObject;
    }

    // Update is called once per frame

    private void CollectCoin()
    {
        PlayerScript.coinCount++;
        coinSound.Play();
        //Debug.Log("This is: " + name);
        //UpdateScoreText();
        StartCoroutine(TimeBeforeDestroy(0.15f));
        

        //StartCoroutine(TimeBeforeDestroy());
        //GameObject.Destroy(CoinSoundScript.coin);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.CompareTag("Player"))
        { 
            CollectCoin();
        }
    }
private IEnumerator TimeBeforeDestroy(float delay)
    {
        //Print the time of when the function is first called.
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);
        //GameObject.Destroy(CoinSoundScript.coin);
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(delay);
        Destroy(coin);

        //After we have waited 5 seconds print the time again.
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }

}
