using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
//using System.Serializable;


/// <summary>
/// This class contains methods that are executed whenever a VR button is pressed. 
/// In a serializable field, the programmer can specify which function should apply 
/// to which button while still in the editor.
/// </summary>
public class VRButtonEvents : MonoBehaviour {

    [System.Serializable]
    public class OnTriggerType : UnityEvent<Collider> { };

    // Will set "OnClick" Event from the inspector
    public OnTriggerType m_OnTriggerType;

    // Bools for toggling various gameobject functions
    private bool screenisOn = false;
    private bool isRotating = false;
    private bool controlOn = false;

    // Invoke the assigned method (declared in the inspector) when the trigger actually happens
    private void OnTriggerEnter(Collider other)
    {
        m_OnTriggerType.Invoke(other);
    }


    //// Possible public "OnClick" methods to call when 3D button is pressed

    public void ResetClick(Collider other)
    {
        if (other.name != "Button") return;
        VRButtonFuncs.VibrateTouch(other);
        VRButtonFuncs.ResetAll(other);
    }

    public void QuitClick(Collider other)
    {
        if (other.name != "Button") return;
        VRButtonFuncs.VibrateTouch(other);
        SceneManager.LoadScene("MainMenu");
        GameObject.Find("ApplicationInfoStarter").GetComponent<AudioSource>().Play(0);
    }

    public void ScreenClick(Collider other)
    {
        if (other.name != "Button") return;

        VRButtonFuncs.VibrateTouch(other);
        GameObject holder = GameObject.Find("FloatingScreen");
        VRButtonFuncs.ScreenOn(other, screenisOn, holder);
        screenisOn = !screenisOn;
    }

    // DEPRECATED -- always open
    public void OpenControlScreenClick(Collider other)
    {
        if (other.name != "Button") return;
        VRButtonFuncs.VibrateTouch(other);
        GameObject controlScreen = GameObject.Find("ControlScreen");
       // VRButtonFuncs.ControlScreenOn(other, controlOn, controlScreen);
        controlOn = !controlOn;

    }



    // DEPRECATED -- Moved from physical button to panel on the screen
    public void RotateClick(Collider other)
    { 
        if (other.name != "Button") return;
        new GameObject("RotateLight");
        VRButtonFuncs.RotateComponents(other, isRotating);
        isRotating = !isRotating;
    }



}
