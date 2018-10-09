using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Analogous to the TouchScreenController but for the hand menu. 
/// Contains the methods for selecting and executing each option.
/// </summary>
public class HandMenuController : MonoBehaviour {

    // child panel
    public GameObject optionsPanel;
    
    // player object
    public OVRPlayerController playerController;
    
    // input manager
    public SmallSceneInputManager input;

    // camera rig on the player controller
    public GameObject cameraRig;

    // tool tip parent under right hand anchor
    public GameObject toolTipHolder;

    // selected option gameobjects
    private GameObject selectedOption = null;
    private GameObject lastSelectedOption = null;

    // describes whether or not we should execute a function on release
    private bool shouldExecute;

    // contains info on whether or not the hand menu is open
    private bool isOpen = false;


    // AudioSources
    private AudioSource aSourceOpen;
    private AudioSource aSourceClick;

    [Header("Audio")]
    // AudioClips
    public AudioClip aClipOpen;
    public AudioClip aClipClick;
    


    public AudioSource SetAudioSource(AudioClip clip, bool loop, bool playAwake, float vol)
    {
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.clip = clip;
        newSource.loop = loop;
        newSource.playOnAwake = playAwake;
        newSource.volume = vol;

        return newSource;

    }

    private void Awake()
    {
        // need more than one audio source on this object, must be procedurally built
        aSourceOpen = SetAudioSource(aClipOpen, false, false, 1f);
        aSourceClick = SetAudioSource(aClipClick, false, false, 1f);
    }

    // Use this for initialization
    void Start ()
    {
        optionsPanel.SetActive(false);   
	}
	
	// Update is called once per frame
	void Update () {
		

        if (input.rPITrig == 1)
        {
            // shut off tool tip if hand menu comes on
            if (cameraRig.GetComponent<RayCastingComponents>().tooltipOn)
            cameraRig.GetComponent<RayCastingComponents>().ToolTipActivation(false);

            if (!isOpen)
            {
                //GetComponent<Animator>().SetTrigger("HandOpen");
                //GetComponent<Animator>().ResetTrigger("HandClose");

                aSourceOpen.Play(0);

                isOpen = true;

            }

            // hand menu is open and we should now execute a function
            shouldExecute = true;

            // view menu
            optionsPanel.SetActive(true);

            // stop player from rotating such that he or she can use the right stick to navigate the menu
            playerController.RotationAmount = 0;
            playerController.RotationRatchet = 0;
            
            // Finds the button nearest to the right stick's position 
            selectedOption = FindNearestOption(new Vector2(input.rStickH, input.rStickV));

            if (selectedOption == null)
            {
                // if nothing is selected (i.e. FindNearestOption returned null or (0,0) on the right stick) 
                // then all choices should be white
                for (int i = 0; i < optionsPanel.transform.GetChildCount(); i++)
                {
                    GameObject child = optionsPanel.transform.GetChild(i).gameObject;
                    Color tmp = child.GetComponent<Image>().color;

                    tmp = Color.white;
                    tmp.a = .5f;

                    child.GetComponent<Image>().color = tmp;
                }
            }

            
            if (selectedOption != lastSelectedOption && lastSelectedOption != null && selectedOption != null)
            {
                // We have selected something other than the most recently selected object and so we must make it cyan,
                // as well as return the last selected object back to its original color

                aSourceClick.Play(0);

                Color tmp = selectedOption.GetComponent<Image>().color;

                tmp = Color.cyan;
                tmp.a = .5f;
                selectedOption.GetComponent<Image>().color = tmp;
                //selectedOption.transform.localScale = new Vector3(1, 2, 1);

                //lectedOption.transform.position += new Vector3(0, 0, .1f);
                tmp = Color.white;
                tmp.a = .5f;
                lastSelectedOption.GetComponent<Image>().color = tmp;

                //lastSelectedOption.transform.localScale = new Vector3(1, 1, 1);
                //lastSelectedOption.transform.position -= new Vector3(0, 0, .1f);

            }
            lastSelectedOption = selectedOption;


        }
        else
        {
            // close methods (attempted animations - there are some issues with rendering such small curved canvases - will come back to this eventually)
            optionsPanel.SetActive(false);

            if (isOpen)
            {
                //GetComponent<Animator>().SetTrigger("HandClose");
                //GetComponent<Animator>().ResetTrigger("HandOpen");
                isOpen = false;
            }

            // allow the player to move again
            playerController.RotationAmount = 1.5f;
            playerController.RotationRatchet = 45f;

            // EXECUTE called on a non null option
            if (shouldExecute && selectedOption != null)
            {
                if (SceneManager.GetActiveScene().name == "OkloVROutdoors_PROPRIETARY")
                    selectedOption.GetComponent<HandFuncsOutdoors>().Execute();
                else
                    selectedOption.GetComponent<HandFuncs>().Execute();

            }
            shouldExecute = false; // should not execute until menu is opened again

            // ensure that all options return to white upon close
            if (lastSelectedOption != null)
            {
                Color tmp = lastSelectedOption.GetComponent<Image>().color;
                tmp = Color.white;
                tmp.a = .5f;
                lastSelectedOption.GetComponent<Image>().color = tmp;
            }
        }
    }

    /// <summary>
    /// Iterate through all options given the current position of the right stick - return the option with the smallest displacement
    /// </summary>
    /// <param name="rPos"></param>
    /// <returns></returns>
    private GameObject FindNearestOption(Vector2 rPos)
    {
        if (rPos == Vector2.zero) return null;
        Vector2 L = new Vector2(-1, 0);
        Vector2 R = new Vector2(1, 0);
        Vector2 U = new Vector2(0, 1);
        Vector2 D = new Vector2(0, -1);

        Dictionary<string, float> distanceDict = new Dictionary<string, float>();

        distanceDict["L"] = (L - rPos).magnitude;
        distanceDict["R"] = (R - rPos).magnitude;
        distanceDict["U"] = (U - rPos).magnitude;
        distanceDict["D"] = (D - rPos).magnitude;

        string bestOpt = (from x in distanceDict where x.Value == distanceDict.Min(v => v.Value) select x.Key).First();
        
        switch (bestOpt)
        {
            case "L":
                return optionsPanel.transform.GetChild(0).gameObject;
                break;

            case "U":
                return optionsPanel.transform.GetChild(1).gameObject;
                break;

            case "R":
                return optionsPanel.transform.GetChild(2).gameObject;
                break;

            case "D":
                return optionsPanel.transform.GetChild(3).gameObject;
                break;

            default:
                return null;
                break;
        }




    }


}
