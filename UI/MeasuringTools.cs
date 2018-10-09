using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeasuringTools : MonoBehaviour {

    [Header("Oculus Hand Anchors")]
    public GameObject rHand;
    public GameObject lHand;

    public GameObject measurementToolTip;

    private SmallSceneInputManager input;

    // Use this for initialization
    void Start () {
        input = GameObject.Find("Starter").GetComponent<SmallSceneInputManager>();

    }

    // Update is called once per frame
    void Update()
    {

        if ((input.rPHTrig == 1 && (input.rNearTouch || input.rTTouch) && !(input.rITouchNear)) && (input.lPHTrig == 1 && (input.lNearTouch || input.lTTouch) && !(input.lITouchNear)))
        {
            // Double Pointers

            List<Vector3> positions = new List<Vector3>();

            // Find the start and end positions of our mesuring stick
            positions.Add(rHand.transform.position + rHand.transform.forward* .08f);
            positions.Add(lHand.transform.position + rHand.transform.forward* .08f);


            // Find the distance between the two
            float disp = (rHand.transform.position - lHand.transform.position).magnitude;
            
            // Open the litle tooltip for displaying the measurement
            measurementToolTip.GetComponent<Animator>().ResetTrigger("CloseMeasurement");
            measurementToolTip.GetComponent<Animator>().SetTrigger("OpenMeasurement");
            measurementToolTip.GetComponentInChildren<Text>().text = disp.ToString();

            // Enable the line renderer
            GetComponent<LineRenderer>().enabled = true;
            GetComponent<LineRenderer>().SetPositions(positions.ToArray());



        }
        else
        {
            // Disable all measurement related gameobjects
            measurementToolTip.GetComponent<Animator>().ResetTrigger("OpenMeasurement");
            measurementToolTip.GetComponent<Animator>().SetTrigger("CloseMeasurement");

            GetComponent<LineRenderer>().enabled = false;


        }
    }
}
