using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskCubeControls : MonoBehaviour {

    public GameObject maskCube;

    private bool isOn = false;

	// Use this for initialization
	void Start () {
        maskCube.SetActive(false);	
	}
	
	// Update is called once per frame
	void Update () {


        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            isOn = !isOn;
            maskCube.SetActive(isOn);
            

        }
		
	}
}
