using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//Idea: End Door to an end level portal(animated sprite)?
//Make level now...
//Added background...

public class PlayerScript : MonoBehaviour
{

    public Rigidbody2D Player;
    public GameObject coin;
    public TMP_Text scoreText; //Can probably be static... ///Get text by making a new script and make an object to get it
    [SerializeField] private int initialCoinCount = 0;
    public static int coinCount = 0; //score


    [SerializeField] float horizontal;
    [SerializeField] float vertical;
    [SerializeField] float moveLimiter = 0.7f;

    public float runSpeed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<Rigidbody2D>();
        //Debug.Log("This is Player: ", Player);
        UpdateScoreText();
        transform.rotation = Quaternion.Euler(0, 0, 0);
        coinCount = initialCoinCount;
        //Maybe use a boolean to disallow the player control until the next level...

    }

    // Update is called once per frame
    void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
        UpdateScoreText();
        //Debug.Log("coin Num: " + coinCount);
    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        Player.linearVelocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.CompareTag("EndLevelPortal"))
        {
            transform.localPosition = other.gameObject.transform.position;
            //Maybe disable movement until the next level is available...?
        }

    }

    
    public void UpdateScoreText()
    {
        scoreText.text = "Coins: " + coinCount; // Update the UI text with the current score
    }

    /*private IEnumerator TimeBeforeDestroy()
    {
        //Print the time of when the function is first called.
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);
        //GameObject.Destroy(CoinSoundScript.coin);
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.1f);
        
        //After we have waited 5 seconds print the time again.
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }*/

}
