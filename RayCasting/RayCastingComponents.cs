using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;


/// <summary>
/// This class controls all of the tool tip functionality for both scenes that utilize it.
/// </summary>
public class RayCastingComponents : MonoBehaviour {

    public GameObject lHand;
    public GameObject rHand;
    public HandFuncs handFunc;
    public Canvas resetValueCanvas;

    private GameObject[] allComps;

    // Reset objects
    private bool resetHasStarted = false;
    private Coroutine resetCO;
    private Coroutine pctCounter;

    [Header("Tool Tip Animator")]
    public Animator ToolTipAnimator;
    public Animator rayCastAnimator;

    [Header("OVRCameraRig Line Renderer")]
    public LineRenderer rayCaster;

    [Header("Tool Tip Image")]
    public Image compTool;

    private GameObject tooltipIndicator;
    [HideInInspector]
    public bool tooltipOn = false;


    // AudioSources
    private AudioSource aSourceReset;
    private bool resetSoundStarted = false;
    private AudioSource aSourceRayOut;
    private AudioSource aSourceRayIn;
    private AudioSource aSourceClicker;

    // AudioClips
    public AudioClip aClipReset;
    public AudioClip aClipRayOut;
    public AudioClip aClipRayIn;
    public AudioClip aClipClick;
    //public AudioClip aClipToolTip;


    public AudioSource SetAudioSource(AudioClip clip, bool loop, bool playAwake, float vol)
    {
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.clip = clip;
        newSource.loop = loop;
        newSource.playOnAwake = playAwake;
        newSource.volume = vol;

        return newSource;

    }

    public void Awake()
    {

        // set all audiosources to have the proper clips and settings
        aSourceReset = SetAudioSource(aClipReset, false, false, .5f);
        aSourceRayOut = SetAudioSource(aClipRayOut, false, false, .5f);
        aSourceRayIn = SetAudioSource(aClipRayIn, false, false, .5f);
        aSourceClicker = SetAudioSource(aClipClick, false, false, .5f);


        // user has not begun resetting
        resetValueCanvas.gameObject.SetActive(false);

        rayCastAnimator = GetComponent<Animator>();

        // Grab all the components for the current scene
        switch (SceneManager.GetActiveScene().name)
        {

            case "OkloVRControlRoom_PROPRIETARY":
                allComps = GameObject.Find("ApplicationInfoStarter").GetComponent<OBJLoader>().controlAllComps;
                break;
            case "OkloVRSmall_PROPRIETARY":
                allComps = GameObject.Find("ApplicationInfoStarter").GetComponent<OBJLoader>().smallAllComps;
                break;
            case "OkloVROutdoors_PROPRIETARY":
                allComps = GameObject.Find("ApplicationInfoStarter").GetComponent<OBJLoader>().outdoorAllComps;
                break;
        }

    }


    ////// HOVER FUNCTIONS //////
    public void OnHoverEnter(Transform t)
    {
        
        if (t.tag != "ReactorComponent" && t.tag != "Eagle") return; // Accept only reactor components and the eagle
        if (t.tag != "Eagle")
        {
            // Reactor component found

            if (!t.name.Contains("dummy")) // Non dummy - only one
                t.gameObject.GetComponent<Renderer>().material.color = Color.cyan;
            else // otherwise update all the similar dummies
                HighlightSimilarDummies(t);
        }
    }

    public void OnHoverExit(Transform t)
    {
        if (t.tag != "ReactorComponent" && t.tag != "Eagle") return;// Accept only reactor components and the eagle


        if (t.tag != "Eagle")
        {
            // Reactor component found

            if (!t.name.Contains("dummy")) // Non dummy - only one
                t.gameObject.GetComponent<Renderer>().material.color = Color.white;
            else // otherwise update all the similar dummies
                DeHighlightSimilarDummies(t);
        }



        Text compText = compTool.GetComponentInChildren<Text>();

        // No proper reactor component has been found
        compText.text = "No reactor component found...";

        // Change color of line to red for bad raycasts
        rayCaster.material.color = new Color(255, 0, 0);

    }
    public void OnHover(Transform t)
    {
        // Change color of line to red for bad raycasts
        rayCaster.material.color = new Color(255, 0, 0);

        if (t.tag != "ReactorComponent" && t.tag != "Eagle")  return;

        Text compText = compTool.GetComponentInChildren<Text>();



        // We have hit a proper reactor component

        // Update the text of the tooltip to reflect the object on the raycast

        // Grab the component's properties 
        CompProps compProps = t.GetComponent<CompProps>();



        if (t.name == "eagle")
        {
            rayCaster.material.color = new Color(0, 255, 0);

            compText.text = "AMERICA";
            return;
        }
        else
        {

            compText.text = "Name: " + t.gameObject.name + "\n" +
                            "Material: " + compProps.materialName + " \n" +
                            "Mass: " + compProps.Mass + " \n" +
                            "Volume: " + compProps.Volume + " \n" +
                            "Avg. Heat: " + compProps.avgHeat + " \n" +
                            "Random Property: " + compProps.anyProp + "\n";// + 
                                                                           //"Squeeze to deactivate component";
        }
        // Change color of line to green for good raycast hits:
        rayCaster.material.color = new Color(0, 255, 0);

        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.LTouch))
        {
            aSourceClicker.Play(0);
            if (!t.name.Contains("dummy"))
                t.gameObject.SetActive(false);
            else
                DeactivateSimilarDummies(t);

        }

    }



    ////// GROUP HIGHLIGHT FUNCTIONS //////
    private void DeactivateSimilarDummies(Transform t)
    {

        string baseName = t.name.Split(new string[] { "_dummy" }, StringSplitOptions.None)[0];

        foreach (GameObject dummyComp in allComps)
        {
            if (!dummyComp.name.Contains("dummy")) continue;

            if (dummyComp.name.Contains(baseName))
            {
                dummyComp.SetActive(false); // deactivate
            }
        }
    }
    private void HighlightSimilarDummies(Transform t)
    {

        string baseName = t.name.Split(new string[] { "_dummy" }, StringSplitOptions.None)[0];

        foreach (GameObject dummyComp in allComps)
        {
            if (!dummyComp.name.Contains("dummy")) continue;

            if (dummyComp.name.Contains(baseName))
            {
                dummyComp.GetComponent<Renderer>().material.color = Color.cyan; // highlight
            }
        }

    }
    private void DeHighlightSimilarDummies(Transform t)
    {

        string baseName = t.name.Split(new string[] { "_dummy" }, StringSplitOptions.None)[0];

        foreach (GameObject dummyComp in allComps)
        {
            if (!dummyComp.name.Contains("dummy")) continue;


            if (dummyComp.name.Contains(baseName))
            {
                dummyComp.GetComponent<Renderer>().material.color = Color.white; // de highlight
            }
        }

    }

    // Toggles the Tool Tip
    public void ToolTipActivation(bool onOff)
    {
        if (onOff)
        {

            aSourceRayOut.Play(0);

            ToolTipAnimator.SetTrigger("ToolTipOpen");
            //ToolTipAnimator.ResetTrigger("ToolTipClose");

            rayCastAnimator.SetTrigger("RayCastOpen");
            rayCastAnimator.ResetTrigger("RayCastClose");
            tooltipOn = true;

        }
        else
        {
            aSourceRayIn.Play(0);
            ToolTipAnimator.SetTrigger("ToolTipClose");
            //ToolTipAnimator.ResetTrigger("ToolTipOpen");


            rayCastAnimator.SetTrigger("RayCastClose");

            rayCastAnimator.ResetTrigger("RayCastOpen");
            tooltipOn = false;

        }
    }

    private void Update()
    {
        bool rNearTouch = OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons, OVRInput.Controller.RTouch);
        bool lNearTouch = OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons, OVRInput.Controller.LTouch);

        //Right and left hand Hand Triggers
        float rPHTrig = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch);
        float lPHTrig = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);

        //Right and left hand Index Triggers
        float rPITrig = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
        float lPITrig = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch);


        // TOGGLE FUNCTIONALITY
        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.RTouch))
        {
            //if (!handFunc.isLaser)
            if (SceneManager.GetActiveScene().name == "OkloVROutdoors_PROPRIETARY" || !handFunc.isLaser)
            {
                if (!tooltipOn)
                {
                    ToolTipActivation(true);
                }
                else
                {
                    ToolTipActivation(false);

                }
            }
        }

        // RESETTING FUNCTIONAILTY
        if ((lPITrig == 1 && lPHTrig == 1 && lNearTouch))// && (rPITrig == 1 && rPHTrig == 1 && rNearTouch))
        {
            if (!resetSoundStarted)
            {
                aSourceReset.Play(0);
                resetSoundStarted = true;
            }
            resetValueCanvas.gameObject.SetActive(true);

            float curPct = resetValueCanvas.transform.GetChild(0).GetChild(0).GetComponent<ProgressBar>().currentPercent;

            curPct += Time.deltaTime / 1.25f * 100f;


            if (curPct >= 100)
            {
                if (!resetHasStarted)
                {
                    StartCoroutine(DelayReset());
                    resetValueCanvas.transform.GetChild(0).GetChild(0).GetComponent<ProgressBar>().currentPercent = 100;
                    resetHasStarted = false;
                }
                resetValueCanvas.gameObject.SetActive(false);

            }
            else
            {
                VRButtonFuncs.VibrateTouch(lHand.GetComponent<Collider>());
                resetValueCanvas.transform.GetChild(0).GetChild(0).GetComponent<ProgressBar>().currentPercent = curPct;
            }
        }
        else
        {
            aSourceReset.Stop();
            resetSoundStarted = false;
            resetValueCanvas.transform.GetChild(0).GetChild(0).GetComponent<ProgressBar>().currentPercent = 0;
            resetValueCanvas.gameObject.SetActive(false);
        }
    }


    public void ResetAll()
    {
        foreach (GameObject comp in allComps)
        {
            if (comp.name == "grass" || comp.name == "ground") continue;

            comp.SetActive(true);
            comp.transform.localPosition = new Vector3(0, 0, 0);
            comp.transform.localScale= new Vector3(1, 1, 1);
            comp.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    private IEnumerator DelayReset()
    { 
        // controls the timer for resetting so user does not accidentally reset all of his or her components
        ResetAll();
        resetValueCanvas.gameObject.SetActive(false);
        resetHasStarted = true;

        yield return new WaitForSeconds(1.5f);

        resetValueCanvas.transform.GetChild(0).GetChild(0).GetComponent<ProgressBar>().currentPercent = 0;
        
    }



}
