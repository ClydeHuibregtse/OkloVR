using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class TouchScreenButton : MonoBehaviour {

    public TouchScreenController tsController;

    private Animator anim;



    private void OnTriggerEnter(Collider other)
    {

        if (other.name != "HandCollider") return;
        VRButtonFuncs.VibrateTouch(other);

        string displayName = gameObject.name;

        anim.SetTrigger("TouchClick");

        switch (displayName)
        {

            // Panel Switches
            case "ColorBy":
                StartCoroutine(tsController.SwitchToColorByCanvas());
                break;

            case "Rotate":
                StartCoroutine(tsController.SwitchToRotateCanvas());
                break;

            case "Return":
                StartCoroutine(tsController.Return());
                break;

            // ColorBy commands
            case "Mass":
                StartCoroutine(tsController.colorBy("Mass"));
                break;

            case "Volume":
                StartCoroutine(tsController.colorBy("Volume"));
                break;

            case "Material":
                StartCoroutine(tsController.colorBy("Material"));
                break;

            case "AvgHeat":
                StartCoroutine(tsController.colorBy("AvgHeat"));
                break;

            // Rotate Commands
            case "RotateModel":
                StartCoroutine(tsController.RotateModel(other));
                break;

            case "RotateUp":
                StartCoroutine(tsController.RotateUp());
                break;

            case "RotateDown":
                StartCoroutine(tsController.RotateDown());
                break;

        }
        //GetComponent<Image>().color.a = 255f;
    }


    
    //public void DisplayText()
    //{
    //    string text = "This is some damn good text to write on the screen";

    //    GameObject.Find("MonitorScreen").GetComponentInChildren<Text>().text = text;


    //}




    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
