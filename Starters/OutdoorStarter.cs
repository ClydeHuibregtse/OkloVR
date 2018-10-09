using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class carries functions necessary to open the Outdoor scene. 
/// </summary>
public class OutdoorStarter: MonoBehaviour {


	// Use this for initialization
	void Start () {

        // Set Gravity
        Physics.gravity = new Vector3(0, -9.81f, 0);

        // Activate preloaded model 
        GameObject.Find("ApplicationInfoStarter").GetComponent<OBJLoader>().outdoorModel.SetActive(true);

    }
}
