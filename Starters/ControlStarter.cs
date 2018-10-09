using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class carries functions necessary to open the Control Room scene. 
/// </summary>
public class ControlStarter : MonoBehaviour {


    // Use this for initialization
    void Start () {
        // Set Gravity
        Physics.gravity = new Vector3(0, -9.81f, 0);

        // Activate the preloaded control room model.
        GameObject.Find("ApplicationInfoStarter").GetComponent<OBJLoader>().controlRoomModel.SetActive(true);

    }

}
