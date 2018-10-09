using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// This class carries functions necessary to open the Small Room scene. 
/// </summary>
public class SmallStarter : MonoBehaviour {

    // Use this for initialization
    void Start () {

        // Activate preloaded small room model
        GameObject.Find("ApplicationInfoStarter").GetComponent<OBJLoader>().smallRoomModel.SetActive(true);

    }

}
