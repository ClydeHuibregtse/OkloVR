using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class verbositySwitcher : MonoBehaviour {

    public bool verbose = false;

	public void SwitchVerbosity () {
		
        //if (Input.GetKeyDown(KeyCode.JoystickButton2)) // Circle Button
        //{
        // Call resetAll() on the button list and negate the verbosity bool
        GameObject ButtonListContent = GameObject.Find("ButtonListContent");
        ButtonListContent.GetComponent<ButtonGenerator>().resetAll();
        verbose = !verbose;
        if (verbose)
        GetComponentInChildren<Text>().text = "Groups";
        else
        GetComponentInChildren<Text>().text = "Individual";

        //}

    }
}
