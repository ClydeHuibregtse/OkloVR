using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {


    [Header("UI Panel Container")]
    public GameObject actionMenuPanel;
    
    [Header("OVR Camera Rig")]
    public GameObject camRig;

    [Header("Tool Tip Parameters")]
    [Tooltip("Canvas")]
    public GameObject toolTipHolder;
    public Animator toolTipAnimator;

    [Header("Right Hand Anchor")]
    public GameObject rightHand;

    [Header("Ray Cast Animator")]
    public Animator rayCastAnim;

    private GameObject miniATT;

    /// <summary>
    /// Describes whether or not the RayCasting functionality is active or inactive
    /// </summary>
    private bool rayCastBool = false;

    /// <summary>
    /// Limits the number of miniature model prefabs present in any on frame to 1.
    /// </summary>
    private bool noSplits = true;

    // Use this for initialization
    void Start () {

        miniATT = GameObject.Find("AllTheThings_Mini"); // Could change with imports

        
        // Instantiate mini ATT as off
        miniATT.SetActive(false);

        // Instantiate tooltips as off
        toolTipHolder.SetActive(false);

    }

    // Update is called once per frame
    void Update () {

        //////          Controller Inputs         //////
        // (Those unused by OVR Character Controller //

        //// Axes ////

        //Right and left hand Hand Triggers
        float rPHTrig = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch);
        float lPHTrig = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);

        //Right and left hand Index Triggers
        float rPITrig = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
        float lPITrig = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch);

        //Right Stick Vertical
        float rStickV = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch)[1];

        //// Buttons (GetDown not Get) ////

        //Right and left hand Thumb Rests
        bool rTTouch = OVRInput.Get(OVRInput.Touch.PrimaryThumbRest, OVRInput.Controller.RTouch);
        bool lTTouch = OVRInput.Get(OVRInput.Touch.PrimaryThumbRest, OVRInput.Controller.LTouch);


        //Right and Left hand Stick Buttons
        bool rTStickB = OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.RTouch);
        bool lTStickB = OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.LTouch);

        //Buttons
        bool A = OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch);
        bool B = OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch);
        bool X = OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch);
        bool Y = OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch);

        bool Start = OVRInput.GetDown(OVRInput.Button.Start, OVRInput.Controller.LTouch);


        bool rNearTouch = OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons, OVRInput.Controller.RTouch);
        bool lNearTouch = OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons, OVRInput.Controller.LTouch);


        //----------------------------------------//


        /////// Right hand Mini-Model Spinner///////
        if (rPHTrig == 1 && rPITrig == 1 && rNearTouch)
        {
            // Right Fist
            miniATT.SetActive(true);
        }
        else  
        {
            miniATT.SetActive(false);
        }
        //-----------------------------------------//

        ////// Tool Tip toggling and animations //////

        if (lTStickB)
        {
            if (true)
            //if (!actionMenuPanel.activeInHierarchy)
            {
                    // The tooltip toggle button should only be functional if the Action Menu is off
                    if (!toolTipHolder.activeInHierarchy)
                {
                    // Default is off so on first click it should turn on
                    toolTipHolder.SetActive(true);
                }


                rayCastBool = !rayCastBool;
                if (rayCastBool)
                {
                    // It is time to raycast:
                    rayCastAnim.ResetTrigger("RayCastClose");
                    rayCastAnim.SetTrigger("RayCastOpen");
                    toolTipAnimator.ResetTrigger("ToolTipClose");
                    toolTipAnimator.SetTrigger("ToolTipOpen");
                }
                else
                {
                    // Stop raycasting
                    rayCastAnim.SetTrigger("RayCastClose");
                    rayCastAnim.ResetTrigger("RayCastOpen");
                    toolTipAnimator.ResetTrigger("ToolTipOpen");
                    toolTipAnimator.SetTrigger("ToolTipClose");
                }
            }
        }
        //if (actionMenuPanel.activeInHierarchy)
        //{
        //    //Action Menu is up: turn off tooltip functionality

        //    // prep rayCastBool for the next pass
        //    rayCastBool = false;
        //    // Run closing animations and prep for next trigger
        //    if (toolTipAnimator.enabled && toolTipAnimator.GetCurrentAnimatorStateInfo(0).IsName("ToolTipOpenIdle"))
        //    {
        //        rayCastAnim.SetTrigger("RayCastClose");
        //        rayCastAnim.ResetTrigger("RayCastOpen");
        //        toolTipAnimator.ResetTrigger("ToolTipOpen");
        //        toolTipAnimator.SetTrigger("ToolTipClose");
        //    }
        //}
        //---------------------------------------------------------//

        noSplits = GameObject.FindGameObjectsWithTag("Mini").Length == 0;

        if (B && noSplits)
        {
            GameObject splitPref = (GameObject)Instantiate(Resources.Load("SplitPrefab"));
            splitPref.transform.position = transform.position + new Vector3(0, .5f, 1);
        }


        ///// Player Controller Movements (Up-Down) //////
        float accel = GetComponent<OVRPlayerController>().Acceleration;
        transform.position += new Vector3(0, 10 * rStickV * accel * Time.deltaTime, 0);

        //--------------------------------------------------------//


    }

    

}
