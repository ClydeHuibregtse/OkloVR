using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/// <summary>
/// This class contains methods to control on click functionality in the main menu.
/// </summary>
public class MainMenuRayCastingFuncs : MonoBehaviour {

    public OBJLoader objLoad;
    private static GameObject mainMenu;


    private void Awake()
    {

        // Protect against multiple main menus being created
        DontDestroyOnLoad(gameObject);
        if (mainMenu == null)
            mainMenu = gameObject;
        else
            Destroy(gameObject);



    }

    /// <summary>
    /// Called when player selects a particular main menu option.
    /// </summary>
    /// <param name="self"></param>
    public void OnClickFunc(GameObject self)
    {

        switch (self.name)
        {
            case "OutdoorSceneImage":
                GameObject.Find("ApplicationInfoStarter").GetComponent<AudioSource>().Stop();
                SceneManager.LoadScene("OkloVROutdoors_PROPRIETARY");
                break;
            case "ControlSceneImage":
                GameObject.Find("ApplicationInfoStarter").GetComponent<AudioSource>().Stop();
                SceneManager.LoadScene("OkloVRControlRoom_PROPRIETARY");
                break;
            case "SmallSceneImage":
                GameObject.Find("ApplicationInfoStarter").GetComponent<AudioSource>().Stop();
                SceneManager.LoadScene("OkloVRSmall_PROPRIETARY");
                break;

            //case "OutdoorSet":
            //    objLoad.LoadModel("Outdoor");
            //    break;

            //case "SmallSet":
            //    objLoad.LoadModel("Small");
            //    break;

            //case "ControlSet":
            //    objLoad.LoadModel("Control");
            //    break;

        }
    }


    private void Update()
    {

        // Seems redundant - but whenever scene is loaded the canvas loses the camera component associated with the canvas
        if (GetComponent<Canvas>().worldCamera == null)
        {
            GetComponent<Canvas>().worldCamera = GameObject.Find("CenterEyeAnchor").GetComponent<Camera>();

        }
    }


}
