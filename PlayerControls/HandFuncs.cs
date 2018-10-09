using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Analogous to the Touch Screen Button class - should be applied to each of the four options, 
/// giving it the necessary setup to commit to its Execute method.
/// </summary>
public class HandFuncs : MonoBehaviour {

    // needed to begin laser mode
    private GameObject camRig;
    private GameObject toolTipHolder;
    private GameObject[] hands;
    private GameObject[] allComps;
    public bool isLaser = false;



	// Use this for initialization
	void Start () {

        

        // Grab all required Laser Mode objects
        camRig = GameObject.Find("OVRCameraRig");
        toolTipHolder = GameObject.Find("ToolTipPrefab");
        hands = GameObject.FindGameObjectsWithTag("Hand");
        allComps = GameObject.Find("ApplicationInfoStarter").GetComponent<OBJLoader>().controlAllComps;


    }


    public void Execute()
    {
        GameObject player = GameObject.Find("OVRCharacterController");


        switch (gameObject.name)
        {

            // Adjust player transform
            case "LaunchPad":

                player.transform.position = new Vector3(176, 5, 250);
                player.transform.eulerAngles = new Vector3(0, -90, 0);
                break;

            // Adjust player transform
            case "ControlSeat":

                player.transform.position = new Vector3(250, 25, 230);
                player.transform.eulerAngles = new Vector3(0, 0, 0);
                break;

            // Initiate Laser Mode / Close Laser Mode
            case "LaserMode":
                if (isLaser)
                {
                    // OPEN
                    foreach (GameObject hand in hands)
                    {
                        hand.GetComponent<LineRenderer>().enabled = false;
                    }

                    foreach (GameObject comp in allComps)
                    {
                        comp.GetComponent<Animator>().enabled = true;
                        comp.GetComponent<Renderer>().material.color = Color.white;

                    }

                    camRig.GetComponent<RayPointer>().enabled = true;
                    camRig.GetComponent<LaserModeControls>().enabled = false;

                    //toolTipHolder.SetActive(true);
                    toolTipHolder.GetComponent<Animator>().ResetTrigger("ToolTipClose");
                    toolTipHolder.GetComponent<Animator>().ResetTrigger("ToolTipOpen");
                    //toolTipHolder.SetActive(true);

                    isLaser = false;
                }
                else
                {

                    //CLOSE

                    foreach (GameObject hand in hands)
                    {
                        hand.GetComponent<LineRenderer>().enabled = true;
                    }

                    foreach (GameObject comp in allComps)
                    {
                        comp.GetComponent<Animator>().enabled = false;
                    }

//                    toolTipHolder.SetActive(false);
                    toolTipHolder.GetComponent<Animator>().ResetTrigger("ToolTipOpen");
                    toolTipHolder.GetComponent<Animator>().ResetTrigger("ToolTipClose");
                    //toolTipHolder.SetActive(false);

                    camRig.GetComponent<RayPointer>().enabled = false;
                    camRig.GetComponent<LaserModeControls>().enabled = true;

                    isLaser = true;
                }
                break;

            // Return to main menu
            case "MainMenu":
                GameObject.Find("AllTheThings").SetActive(false);
                SceneManager.LoadScene("MainMenu");
                break;
        }

    }


}
