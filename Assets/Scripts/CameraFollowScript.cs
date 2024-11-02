using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    //public float smoothSpeed = 0.125f;
    public float smoothSpeed = 1.1f;
    public Vector3 offset;

    // Update is called once per frame
    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset;  
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);  
            transform.position = smoothedPosition;  
        }
    }
}
//The script that I have is attached to a game object. How do I reference another game object's component from this script?