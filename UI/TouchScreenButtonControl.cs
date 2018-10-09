using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// This class should be applied to every touch screen 
/// button created in the Control Room scene. It contains callable 
/// methods that are executed on the press event of each button assigned in the editor.
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class TouchScreenButtonControl : MonoBehaviour {


    // Editor assigned Touch Screen Controller - this should be the controller assigned to the screen on which the button lies. 
    public TouchScreenControllerControl tsController;

    // Only assigned for the animator panel in the upper control seat.
    public GameObject playpause;

    // Animates the button after its press
    private Animator anim;
    private bool recentlyTouched = false;

    // Only for the launch button Easter Egg
    public AudioClip aLaunchClip;


    public AudioSource SetAudioSource(AudioClip clip, bool loop, bool playAwake, float vol)
    {
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.clip = clip;
        newSource.loop = loop;
        newSource.playOnAwake = playAwake;
        newSource.volume = vol;

        return newSource;

    }


    /// <summary>
    /// Protects against double touches by enforcind a delay the same length as the press animation
    /// </summary>
    /// <returns></returns>
    private IEnumerator TouchDelay()
    {
        yield return new WaitForSeconds(.617f);
        recentlyTouched = false;
    }

    /// <summary>
    /// Called on the entry of the hand collider (essentially attached to the user's index finger) 
    /// to the box collider associated with each button.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {

        if (other.name != "HandCollider") return; // only index finger triggers should activate button presses
        
        // Stop from double touching;
        if (recentlyTouched) return;
        recentlyTouched = true;
        StartCoroutine(TouchDelay()); // start counting for next touch

        // Haptic function 
        VRButtonFuncs.VibrateTouch(other);

        // Grab the name of the button (CRITICAL: naming allows for proper function execution)
        string displayName = gameObject.name;

        // Button click animation
        anim.SetTrigger("TouchClick");

        // Execute proper function
        switch (displayName)
        {

            // LAUNCH!!!
            case "LaunchButton":
                // pause music and start 2001 A Space Odessey theme
                GameObject.Find("Starter").GetComponent<AudioSource>().Pause();
                AudioSource aLaunchMusic = SetAudioSource(aLaunchClip, false, false, 1f);
                aLaunchMusic.Play(0);
                // Start launch animation 
                StartCoroutine(tsController.InitiateLaunchSequence());
                break;


           // Reactor Orientations
            case "HorizontalOrientation":
                tsController.SwitchToHorizontalOrientation();
                break;

            case "VerticalOrientation":
                tsController.SwitchToVerticalOrientation();
                break;

            // Animations
            case "ConstructionAnimation":
                tsController.Animate("Construction");
                break;


            // Animation Controls (Switch to animation object with play pause skip forward backward overwrite methods)
            case "Play":
                tsController.activeAnimObj.Play();

                gameObject.name = "Pause";
                GetComponentInChildren<Text>().text = "Pause";
                break;

            case "Pause":
                tsController.activeAnimObj.Pause();

                gameObject.name = "Play";
                GetComponentInChildren<Text>().text = "Play";
                break;

            case "SkipForward":
                tsController.activeAnimObj.SkipForward();

                playpause.name = "Play";
                playpause.GetComponentInChildren<Text>().text = "Play";
                break;

            case "SkipBackward":
                tsController.activeAnimObj.SkipBackward();

                playpause.name = "Play";
                playpause.GetComponentInChildren<Text>().text = "Play";
                break;

        }
    }


    private void Start()
    {
        anim = GetComponent<Animator>();
        // find the playpause button
        playpause = GameObject.Find("Play");
        
    }
}
