using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class allows the user to toggle gravity on and off for individual reactor components that have been grabbed.
/// </summary>
public class PhysicsWorkaronds : MonoBehaviour {

    private OVRGrabbable OVRgrab;

    private bool gravOn = false;

    private SmallSceneInputManager input;


    // Use this for initialization
    public void Start () {

        OVRgrab = GetComponent<OVRGrabbable>();
        GetComponent<Rigidbody>().useGravity = false;

        input = GameObject.Find("Starter").GetComponent<SmallSceneInputManager>();
	}
	
	// Update is called once per frame
	void Update () {


        if (OVRgrab.grabbedBy != null && input.lTStickB)
        {
            //Left stick 
            gravOn = !gravOn;
            GetComponent<Rigidbody>().useGravity = gravOn;

        }
	}
}
