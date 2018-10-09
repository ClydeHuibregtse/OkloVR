using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RayCastingFuncs : MonoBehaviour {

    [Header("Tool Tip Animator")]
    public Animator ToolTipAnimator;

    [Header("OVRCameraRig Line Renderer")]
    public LineRenderer rayCaster;

    [Header("Tool Tip Image")]
    public Image compTool;

    private GameObject tooltipIndicator;
    private bool tooltipOn = false;


    public void OnHoverExitComp(Transform t)
    {

        Text compText = compTool.GetComponentInChildren<Text>();


        // No proper reactor component has been found
        compText.text = "No reactor component found...";

        // Change color of line to red for bad raycasts
        rayCaster.material.color = new Color(255, 0, 0);


    }

    public void OnHoverComp(Transform t)
    {
        Text compText = compTool.GetComponentInChildren<Text>();


        if (t.tag == "ReactorComponent")
        {
            // We have hit a proper reactor component

            // Update the text of the tooltip to reflect the object on the raycast

            // Grab the component's properties 
            CompProps compProps = t.GetComponent<CompProps>();


            compText.text = "Name: " + t.gameObject.name + "\n" +
                            "Material: " + compProps.materialName + " \n" +
                            "Mass: " + compProps.Mass + " \n" +
                            "Volume: " + compProps.Volume + " \n" +
                            "Avg. Heat: " + compProps.avgHeat + " \n" +
                            "Random Property: " + compProps.anyProp + "\n";// + 
                            //"Squeeze to deactivate component";

            // Change color of line to green for good raycast hits:
            rayCaster.material.color = new Color(0,255,0);

        }
        else
        {
            // No proper reactor component has been found
            compText.text = "No reactor component found...";

            // Change color of line to red for bad raycasts
            rayCaster.material.color = new Color(255, 0, 0);

        }
    }	
}
