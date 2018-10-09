using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipControls : MonoBehaviour {

    private GameObject hand;

    private GameObject toolTipCanvas;

    private OVRGrabbable OVRGrab;

    private Animator anim;

    private Transform origTrans;

    private bool justLetGo = true;

    private CompProps compProps;


	// Use this for initialization
	void Start () {
 

        OVRGrab = GetComponent<OVRGrabbable>();

        toolTipCanvas = GameObject.Find("ToolTip");
        origTrans = toolTipCanvas.transform;
        compProps = GetComponent<CompProps>();
        anim = toolTipCanvas.GetComponent<Animator>();
    }
	



	// Update is called once per frame
	void Update () {

        float scale = Mathf.Pow((transform.localScale.x), 1); // 3 for volumetric scaling
        float opacity = GetComponent<Renderer>().material.color.a * 100;


        if (OVRGrab.grabbedBy != null)
        {
            toolTipCanvas.GetComponentInChildren<Text>().text = "Name: " + gameObject.name + "\n" +
                                                                "Material: " + compProps.materialName + " \n" +
                                                                "Mass: " + compProps.Mass + " kg \n" +
                                                                "Volume: " + compProps.Volume + " m^3 \n" +
                                                                "Avg. Heat: " + compProps.avgHeat + " \n" +
                                                                "Random Property: " + compProps.anyProp + "\n" +
                                                                "Current Scale: " + scale + ":1 \n" +
                                                                "Current Opacity: " + opacity + "%";
            anim.ResetTrigger("ToolTipClose");
            anim.SetTrigger("ToolTipOpen");
            justLetGo = true;
            
        }
        else
        {
            if (justLetGo)
            {
                anim.SetTrigger("ToolTipClose");
                anim.ResetTrigger("ToolTipOpen");
            }
            justLetGo = false;
        }

	}
}
