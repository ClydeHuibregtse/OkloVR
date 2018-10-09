using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSceneInputManager : MonoBehaviour {

    //// Axes ////

    //Right and left hand Hand Triggers
    public float rPHTrig;
    public float lPHTrig;

    //Right and left hand Index Triggers
    public float rPITrig;
    public float lPITrig;

    //Right Stick Vertical
    public float rStickV;    
    //Right Stick Horizontal
    public float rStickH;

    //// Buttons (GetDown not Get) ////

    //Right and left hand Thumb Rests
    public bool rTTouch;
    public bool lTTouch;


    //Right and Left hand Stick Buttons
    public bool rTStickB;
    public bool lTStickB;

    //Buttons
    public bool A;
    public bool B;
    public bool X;
    public bool Y;

    public bool Start;


    //// Near Touches ////
    public bool rITouchNear;
    public bool lITouchNear;

    public bool rNearTouch;
    public bool lNearTouch;


    // Update is called once per frame
    void Update () {
        //////          Controller Inputs         //////
        // (Those unused by OVR Character Controller //

        //// Axes ////

        //Right and left hand Hand Triggers
         rPHTrig = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch);
         lPHTrig = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);

        //Right and left hand Index Triggers
         rPITrig = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
         lPITrig = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch);

        //Right Stick Vertical
        rStickV = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch)[1];
        //Right Stick Horizontal
        rStickH = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch)[0];

        //// Buttons (GetDown not Get) ////

        //Right and left hand Thumb Rests
        rTTouch = OVRInput.Get(OVRInput.Touch.PrimaryThumbRest, OVRInput.Controller.RTouch);
         lTTouch = OVRInput.Get(OVRInput.Touch.PrimaryThumbRest, OVRInput.Controller.LTouch);


        //Right and Left hand Stick Buttons
         rTStickB = OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.RTouch);
         lTStickB = OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.LTouch);

        //Buttons
         A = OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch);
         B = OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch);
         X = OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch);
         Y = OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch);

        Start = OVRInput.GetDown(OVRInput.Button.Start, OVRInput.Controller.LTouch);


        //// Near Touches ////
         rITouchNear = OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
         lITouchNear = OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger, OVRInput.Controller.LTouch);

;

         rNearTouch = OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons, OVRInput.Controller.RTouch);
         lNearTouch = OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons, OVRInput.Controller.LTouch);


        //----------------------------------------//
    }
}
