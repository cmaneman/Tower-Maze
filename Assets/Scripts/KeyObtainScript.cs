using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObtainScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] static public bool KeyGetCheck = false;
    [SerializeField] private GameObject Key;
    public AudioSource keySound;

    void Start()
    {
        Key = gameObject;
        keySound = GetComponent<AudioSource>();
    }

    private void GotKey()
    {
        //play sound
        //When player collects key, it will be true and allow you to touch the door.
        
        //Debug.Log(DoorScript.Door2D.enabled);
        //Debug.Log("-------------------");
        //DoorScript.Door2D.isTrigger = true;
       //DoorScript.DoorFalse();
        //Debug.Log(DoorScript.Door2D.isTrigger);
        //Key.SetActive(false);
        KeyGetCheck = true;
        //Destroy(Key);
        keySound.Play();
        StartCoroutine(TimeBeforeDisable());
        
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            GotKey();
        }
    }

    private IEnumerator TimeBeforeDisable()
    {
        yield return new WaitForSeconds(0.2f);
        Key.SetActive(false);
    }
}
