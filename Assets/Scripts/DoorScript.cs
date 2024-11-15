using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    //[SerializeField] private bool DoorUnlockCheck = false;
    [SerializeField] private BoxCollider2D Door2D;

    [SerializeField] private AudioSource DoorSound;
    void Start()
    {
        Door2D = GetComponent<BoxCollider2D>();
        DoorSound = GetComponent<AudioSource>();
        
        //Door2D.enabled = true;
        Door2D.isTrigger = false;
    }
    void Update()
    {
        DoorAccess();
    }

    public void DoorAccess()
    {

        Debug.Log(Door2D.isTrigger);

        if(KeyObtainScript.KeyGetCheck == true)
        {
            //Door2D.enabled = false;
            //Debug.Log(Door2D.enabled);
            Door2D.isTrigger = true;
        }
        
    }

    void UnlockDoor()
    {
        //Go to GotKey method and make the value true...
        //play sound
        DoorSound.Play();
        StartCoroutine(TimeBeforeDisable());
          
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //When player touches the key, go to UnlockDoor method and change the Is Trigger value to true and make the door disappear/invisible/disable for the current level...
        if(other.CompareTag("Player"))
        {
            UnlockDoor();
        }
    }

    private IEnumerator TimeBeforeDisable()
    {
        yield return new WaitForSeconds(0.2f);
        Door2D.gameObject.SetActive(false); 
    }
}
//Maybe make the door a little ligher or some effect that makes it seem like its unlocked or give some indication.