using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;


/// <summary>
/// This class carries the brunt of WaveFront OBJ file import and gameobject setup for each of the three scenes.
/// For debugging purposes, if one feels that a file has been imported improperly, check this script for 
/// scaling, transform or otherwise confusing import behavior. 
/// </summary>
public class OBJLoader : MonoBehaviour {

    // Contains all of the imported GameObjects and arrays of all of their associated children
    [HideInInspector]
    public GameObject controlRoomModel;
    [HideInInspector]
    public GameObject[] controlAllComps;
    [HideInInspector]
    public GameObject smallRoomModel;
    [HideInInspector]
    public GameObject[] smallAllComps;
    [HideInInspector]
    public GameObject outdoorModel;
    [HideInInspector]
    public GameObject[] outdoorAllComps;

    // Deprecated but not deleted
    public static GameObject keyboard;
    private string activeSetter;

    // static object containing main menu (wont be destroyed on load, 
    // but we only want to store 1 so returning to main menu does not create extra menus)
    public static GameObject mainMenu;


    // Scale factors (will be adjusted when we collect data about the lengths of 
    // objects and can be more precise about our size adjustments to fit each scene
    public float controlRoomScaleFactor = 3f;
    public float smallScaleFactor = 1f;
    public float outdoorScaleFactor = 1f;
  

    private void Start()
    {
        LoadAllModels();
    }

    private void LoadAllModels()
    {
        // Open and read the Modules.txt file
        string[] lines = File.ReadAllLines(Application.dataPath + "/" + "Modules.txt");

        // Iterate through the lines (there should only be 3)
        foreach (string line in lines)
        {
            // Find the name of the scene we wish to access
            string whichScene = line.Split(':')[0];

            // Perform loading operations for each scene
            switch (whichScene)
            {
                case "ControlRoom":
                    // delete all current models that exist - redundant but used to be necessary
                    Destroy(controlRoomModel);

                    // Create new empty game object to store the loaded model
                    GameObject controlToPlace = new GameObject("ToSave");

                    // call the OBJReader function to load the obj file as a list of all components
                    controlAllComps = GetComponent<ObjReader>().ConvertFile(Application.dataPath + "/" + line.Split(' ')[1], true, (Material)Resources.Load("Materials/GeneralReactorMat"));

                    // iterate through the loaded components and set each parent to the new gameobject
                    foreach (GameObject comp in controlAllComps) comp.transform.SetParent(controlToPlace.transform);

                    // adjust the pointer such that it points to our loaded model gameobject
                    controlRoomModel = controlToPlace;

                    // deactivate it such that the user does not see it in the main menu
                    controlRoomModel.SetActive(false);

                    // CRITICAL: do not destroy the object when it is loaded into the new scene
                    DontDestroyOnLoad(controlRoomModel);

                    // Call method to properly set all components of the loaded gameobjects
                    ControlStart();

                    // set the text to reflect the loaded model
                    GameObject.Find("ControlSet").GetComponentInChildren<Text>().text = "Loaded Model: " + line.Split(' ')[1];
                    break;

                case "Outdoors":

                    // delete all current models that exist - redundant but used to be necessary
                    Destroy(outdoorModel);

                    // Create new empty game object to store the loaded model
                    GameObject outdoorToPlace = new GameObject("ToSave");

                    // call the OBJReader function to load the obj file as a list of all components
                    outdoorAllComps = GetComponent<ObjReader>().ConvertFile(Application.dataPath + "/" + line.Split(' ')[1], true, (Material)Resources.Load("Materials/GeneralReactorMat"));

                    // iterate through the loaded components and set each parent to the new gameobject
                    foreach (GameObject comp in outdoorAllComps) comp.transform.SetParent(outdoorToPlace.transform);

                    // adjust the pointer such that it points to our loaded model gameobject
                    outdoorModel = outdoorToPlace;

                    // deactivate it such that the user does not see it in the main menu
                    outdoorModel.SetActive(false);

                    // CRITICAL: do not destroy the object when it is loaded into the new scene
                    DontDestroyOnLoad(outdoorModel);

                    // Call method to properly set all components of the loaded gameobjects
                    OutdoorStart();

                    // set the text to reflect the loaded model
                    GameObject.Find("OutdoorSet").GetComponentInChildren<Text>().text = "Loaded Model: " + line.Split(' ')[1];
                    break;

                case "SmallRoom":

                    // delete all current models that exist - redundant but used to be necessary
                    Destroy(smallRoomModel);

                    // Create new empty game object to store the loaded model
                    GameObject smallToPlace = new GameObject("ToSave");

                    // call the OBJReader function to load the obj file as a list of all components
                    smallAllComps = GetComponent<ObjReader>().ConvertFile(Application.dataPath + "/" + line.Split(' ')[1], true, (Material)Resources.Load("Materials/GeneralReactorMat"));

                    // iterate through the loaded components and set each parent to the new gameobject
                    foreach (GameObject comp in smallAllComps) comp.transform.SetParent(smallToPlace.transform);

                    // adjust the pointer such that it points to our loaded model gameobject
                    smallRoomModel = smallToPlace;
                   
                    // deactivate it such that the user does not see it in the main menu 
                    smallRoomModel.SetActive(false);
                    
                    // CRITICAL: do not destroy the object when it is loaded into the new scene
                    DontDestroyOnLoad(smallRoomModel);
                    
                    // Call method to properly set all components of the loaded gameobjects
                    SmallStart();
                    
                    // set the text to reflect the loaded model
                    GameObject.Find("SmallSet").GetComponentInChildren<Text>().text = "Loaded Model: " + line.Split(' ')[1];
                    break;

            }
        }
    }

    // Component Appliers
    private void ControlStart()
    {

        // Instantiate
        GameObject aTT = controlRoomModel;

        
        aTT.name = "AllTheThings";

        // Set the transform of the parent object
        aTT.transform.position = new Vector3(250, 25, 250);
        aTT.transform.eulerAngles = new Vector3(90, 0, 0);
        aTT.transform.localScale = Vector3.one * controlRoomScaleFactor;


        // Set parent components:
        
        // Text file reader component
        aTT.AddComponent<ReactorCompProps>();

        // Rotation component
        RotationHelper rotHelper = aTT.AddComponent<RotationHelper>();
        rotHelper.aClipRotate = (AudioClip)Resources.Load("Audio/WooshRotateSound(1)"); // add proper sound to rotation component

        // Animator
        Animator aTTAnim = aTT.AddComponent<Animator>();
        aTTAnim.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Animations/ControlScene/ControlModelAnimator");


        for (int i = 0; i < aTT.transform.GetChildCount(); i++)
        {
            // Set child components:
            GameObject child = aTT.transform.GetChild(i).gameObject;


            
            child.tag = "ReactorComponent";
            child.layer = 10; // Holographic component layer

            child.GetComponent<Renderer>().material = (Material)Resources.Load("Materials/GeneralReactorMat"); // blank material

            // necessary mesh collider for raycasting 
            child.AddComponent<MeshCollider>();

            // properties component
            child.AddComponent<CompProps>();

            // set animator and assign proper runtime animator controller
            Animator childAnim = child.AddComponent<Animator>();
            childAnim.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Animations/ControlScene/ComponentAnimator");

        }


    }

    private void OutdoorStart()
    {
        // Set parent components:
        GameObject aTT = outdoorModel;
        aTT.name = "UnderGroundModule";

        // assign proper transform (CRITICAL: these values must change if any gameobjects are moved in this scene)
        aTT.transform.localScale = Vector3.one * outdoorScaleFactor;
        aTT.transform.position = new Vector3(-112.38f, 11.25f, 538.45f);

        // gives player ability to control the opacity of the "ground/grass" gameobjects
        aTT.AddComponent<OpacityControls>();

        for (int i = 0; i < aTT.transform.GetChildCount(); i++)
        { 
            // Set child components:
            GameObject child = aTT.transform.GetChild(i).gameObject;

            // child local rotation must be set as model comes in sideways (Be careful that this consistent across all models)
            child.transform.eulerAngles = new Vector3(-90, 0, 0);

            // Grass and ground require separate treament and shoudl retain original green/brown coloring
            if (child.name == "ground" || child.name == "grass")
            {
                child.layer = 9; // hands layer - helps alleviate raycasting errors - raycaster ignores this layer
            }
            else
            {
                // Everything else is a valid reactor component
                child.GetComponent<Renderer>().material = (Material)Resources.Load("Materials/GeneralReactorMat");
                child.AddComponent<CompProps>();

                // reactor component layer
                child.layer = 8;
                child.tag = "ReactorComponent";
            }

            // player must be able to stand on every component regardless of whether or not 
            // it is a valid reactor component
            child.AddComponent<MeshCollider>();

        }
        
    }

    private void SmallStart()
    {

        // Set parent components:
        GameObject aTT = smallRoomModel;

        aTT.name = "SingleCan";

        // set transform to be directly above table
        aTT.transform.position = new Vector3(-1, 2, -.75f);
        aTT.transform.eulerAngles = new Vector3(0, 90, 0);

        // This may be unnecessary
        aTT.AddComponent<MeshRenderer>();

        // Allows entire can to rotate when rotation button is pressed
        aTT.AddComponent<RotateFunc>();

        // Text file reader - extract information about individual reactor components
        aTT.AddComponent<ReactorCompProps>();

        for (int i = 0; i < aTT.transform.GetChildCount(); i++)
        {
            // Set child components:

            GameObject child = aTT.transform.GetChild(i).gameObject;

            child.transform.localScale = Vector3.one * smallScaleFactor;

            child.tag = "ReactorComponent";
            child.layer = 8; // reactor component

            MeshCollider childMeshC = child.AddComponent<MeshCollider>();
            childMeshC.convex = true; // Convex helps user get better grab ability -
                                      // otherwise user has to be more deliberate with his or her grabbing motion

            // Rigidbodies are required for OVR Grabbable - no gravity such that they float
            Rigidbody childRB = child.AddComponent<Rigidbody>();
            childRB.useGravity = false;

            // Gives toggleable gravity
            child.AddComponent<PhysicsWorkaronds>();

            // Allows for changing scale and opacity
            child.AddComponent<VisualizationTools>();

            // Displays tooltip when component is grabbed
            child.AddComponent<ToolTipControls>();

            // Properties component
            child.AddComponent<CompProps>();

            // How the rift interprets user grabs
            child.AddComponent<OVRGrabbable>();
        }


    }




    ////// DEPRECATED //////

    // Deprecated but not deleted
    public void LoadModel(string whichScene)
    {
        keyboard.SetActive(true);
        mainMenu.SetActive(false);

        keyboard.transform.position = GameObject.Find("OVRPlayerController").transform.position + new Vector3(0, 0, 1.5f);

        activeSetter = whichScene;

    }

    // Deprecated but not deleted
    public void OnGo(Text input)
    {

        keyboard.SetActive(false);

        mainMenu.SetActive(true);

        string filePath = Application.dataPath + "/" + input.text;
        Debug.Log(input.text);
        //filePath = Application.dataPath + "/" + "ModuleAssemblyLight.obj";
        //filePath = Application.dataPath + "/" + "SingleCell.obj";
        //filePath = Application.dataPath + "/" + "UndergroundModule.obj";
        filePath = Application.dataPath + "/" + "Nexus.obj";
        // load the input into whatever object is required
        Debug.Log(filePath);
        GameObject[] allComps = GetComponent<ObjReader>().ConvertFile(filePath, true, (Material)Resources.Load("Materials/GeneralReactorMat"));

        GameObject modelToPlace = new GameObject("ToSave");

        foreach (GameObject comp in allComps)
        {
            comp.transform.SetParent(modelToPlace.transform);
            //comp.transform.localScale = new Vector3(.001f, .001f, .001f);
            //comp.transform.position = new Vector3(250, 10, 250);
            //comp.GetComponent<Renderer>().material = (Material)Resources.Load("Materials/GeneralReactorMat");
        }

        DontDestroyOnLoad(modelToPlace);
        modelToPlace.SetActive(false);

        switch (activeSetter)
        {
            case "Outdoor":
                Destroy(outdoorModel);

                GameObject.Find("OutdoorSet").GetComponentInChildren<Text>().text = "Loaded Model: " + input.text;
                outdoorModel = modelToPlace;
                outdoorAllComps = allComps;
                OutdoorStart();
                break;
            case "Control":
                Destroy(controlRoomModel);
                GameObject.Find("ControlSet").GetComponentInChildren<Text>().text = "Loaded Model: " + input.text;
                controlRoomModel = modelToPlace;
                controlAllComps = allComps;
                ControlStart();
                break;
            case "Small":
                Destroy(smallRoomModel);

                GameObject.Find("SmallSet").GetComponentInChildren<Text>().text = "Loaded Model: " + input.text;
                smallAllComps = allComps;
                smallRoomModel = modelToPlace;
                SmallStart();
                break;


        }
    }


}
