using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class contains static methods that should be called whever a VR button (3D button) is pressed. 
/// They vibrate the controller and then execute their desired method.
/// </summary>
public class VRButtonFuncs : MonoBehaviour {


    // Grab and store the controls for haptics on each touch controller
    private static OculusHapticsController leftControllerHaptics;     
    private static OculusHapticsController rightControllerHaptics; 

    private static bool rotateLightOn = false;


    public static void VibrateTouch(Collider other)
    {
        leftControllerHaptics = GameObject.Find("LeftHandAnchor").GetComponent<OculusHapticsController>();
        rightControllerHaptics = GameObject.Find("RightHandAnchor").GetComponent<OculusHapticsController>();

        // declare an empty pointer to locate which object did the button pushing
        GameObject pusher;

        switch (other.name)
        {

            case "HandCollider":
                pusher = other.transform.parent.gameObject;
                break;

            case "LeftHandAnchor":
                pusher = other.gameObject;
                break;

            case "RightHandAnchor":
                pusher = other.gameObject;
                break;


            default:
                pusher = other.GetComponent<CollisionHaptics>().pusher;
                break;



        }

        // VIBRATE the corresponding controller
        if (pusher == leftControllerHaptics.gameObject)
        {
            leftControllerHaptics.Vibrate(VibrationForce.Hard);
        }
        if (pusher == rightControllerHaptics.gameObject)
        {
            rightControllerHaptics.Vibrate(VibrationForce.Hard);
        }

    }

    public static void ResetAll(Collider other)
    {

        if ((other.name != "Button" && other.name != "HandCollider")) return;
        //if (!(other.name == "HandCollider")) return;

        //VibrateTouch(other);
        GameObject.Find("SingleCan").transform.eulerAngles = new Vector3(0, 90, 0);
        foreach (GameObject comp in GameObject.FindGameObjectsWithTag("ReactorComponent"))
        {
            comp.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            comp.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
            comp.GetComponent<Rigidbody>().useGravity = false;
            comp.transform.localPosition = new Vector3(0, 0, 0);
            comp.transform.localScale = new Vector3(1, 1f, 1f); 
            comp.transform.localEulerAngles = new Vector3(0, 0, 0);
        }

    }

    public static void RotateComponents(Collider other, bool isOn)
    {
        

        GameObject rotateLight = GameObject.Find("RotateLight");
        
        if (!isOn)
        {

            //VibrateTouch(other);
            ResetAll(other);
            Light(rotateLight);

            GameObject.Find("SingleCan").GetComponent<RotateFunc>().rotateable = true;
        }
        else
        {
            GameObject.Find("SingleCan").GetComponent<RotateFunc>().rotateable = false;

            //VibrateTouch(other);
            ResetAll(other);
            Destroy(rotateLight);
        }
       

    }

    private static void Light(GameObject rotateLight)
    {
        rotateLight.AddComponent(typeof(Light));
        rotateLight.GetComponent<Light>().type = LightType.Spot;

        rotateLight.transform.position = new Vector3(0, 1.2f, -.7f);
        rotateLight.transform.localEulerAngles = new Vector3(-90, 0, 0);
        rotateLight.GetComponent<Light>().intensity = 10f;
        rotateLight.GetComponent<Light>().spotAngle = 120f;
    }

    public static void ScreenOn(Collider other, bool isOn, GameObject holder)
    {

        Animator anim = GameObject.Find("FloatingScreenCanvasMain").GetComponent<Animator>();
        if (!isOn)
        {

            anim.SetTrigger("ScreenOn");
            anim.ResetTrigger("ScreenOff");

        }
        else
        {

            if (holder.GetComponent<TouchScreenController>())
            {
                holder.GetComponent<TouchScreenController>().activePanel.GetComponent<Animator>().SetTrigger("ScreenOff");
                holder.GetComponent<TouchScreenController>().activePanel.GetComponent<Animator>().ResetTrigger("ScreenOn");

            }
            else
            {
                holder.GetComponent<TouchScreenControllerOutDoor>().activePanel.GetComponent<Animator>().SetTrigger("ScreenOff");
                holder.GetComponent<TouchScreenControllerOutDoor>().activePanel.GetComponent<Animator>().ResetTrigger("ScreenOn");
            }



            //anim.SetTrigger("ScreenOff");
            //anim.ResetTrigger("ScreenOn");
        }

    }


    // DEPRECATED
    public static void TurnOnGravity(Collider other)
    {
        if (other.name != "Button") return;
        VibrateTouch(other);
        foreach (GameObject comp in GameObject.FindGameObjectsWithTag("ReactorComponent"))
        {
            comp.GetComponent<Rigidbody>().useGravity = true;
        }

    }
}
