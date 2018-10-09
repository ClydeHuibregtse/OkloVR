using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HandFuncsOutdoors : MonoBehaviour
{

    private GameObject camRig;
    private GameObject toolTipHolder;

    private GameObject[] hands;

    private GameObject[] allComps;


    public bool isLaser = false;



    // Use this for initialization
    void Start()
    {




        camRig = GameObject.Find("OVRCameraRig");
        toolTipHolder = GameObject.Find("ToolTipPrefab");
        hands = GameObject.FindGameObjectsWithTag("Hand");

        allComps = GameObject.Find("ApplicationInfoStarter").GetComponent<OBJLoader>().outdoorAllComps;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Execute()
    {
        GameObject player = GameObject.Find("OVRPlayerController");


        switch (gameObject.name)
        {

            case "GroundLevel":

                player.transform.position = new Vector3(-135, 18, 538);
                player.transform.eulerAngles = new Vector3(0, 90, 0);
                break;

            case "MainMenu":
                GameObject.Find("UnderGroundModule").SetActive(false);
                SceneManager.LoadScene("MainMenu");
                break;
        }

    }


}
