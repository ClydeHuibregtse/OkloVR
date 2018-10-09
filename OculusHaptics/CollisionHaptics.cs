using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHaptics : MonoBehaviour {
    
    [HideInInspector]
    /// <summary>
    /// Stores the gameobject that committed the pushing action. Will call a vibrate function on this object on each button press.
    /// </summary>
    public GameObject pusher;

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Hand")
            // Only want to track objects that are Oculus Touch controllers
            pusher = collision.gameObject;


    }

}
