using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonGenerator : MonoBehaviour
{
    /// <summary>
    /// List of Button GameObjects for each specified GROUP of reactor components
    /// </summary>
    protected List<GameObject> hierarchyButtons = new List<GameObject>();
 
    /// <summary>
    /// List of Button GameObjects for each individual reactor component
    /// </summary>
    protected List<GameObject> verboseButtons = new List<GameObject>();

    /// <summary>
    /// Reactor GameObject
    /// </summary>
    private GameObject AllTheThings;
    
    [Header("Button Parent")]
    public GameObject ButtonListContent;

    /// <summary>
    /// Button that contains the verbosity commands
    /// </summary>
    public GameObject verbositySwitcher;

    /// <summary>
    /// Bool: whether or not hierarchy or individual buttons were present in the last frame
    /// </summary>
    private bool pastVerbosity = true;


    // Use this for initialization
    void Start()
    {

        // Grab the object containing all of the reactor components
        AllTheThings = GameObject.Find("AllTheThings");

        // Instantiate a new ReactorComponents Object -- fills the reactorComponentsList object
        ReactorComponents reactorComponents = AllTheThings.GetComponent<ReactorComponents>();

        // Create a button object for each component
        foreach (GameObject comp in reactorComponents.reactorComponentsList)
        { 
            createButtonObject(comp, ButtonListContent);
        }
        
        // Create a hierarchy button for each hierarchy group
        foreach (KeyValuePair<string, List<GameObject>> entry in reactorComponents.groupNames)
        {
            createHierarchyButtonObject(entry.Value, ButtonListContent);
        }

    }

	void Update()
	{

        SwitchVerbosity();
       
    }

    void SwitchVerbosity()
    {

        // The VerbositySwitcher class gets updated with a bool of same type as pastVerbosity
        if (pastVerbosity != verbositySwitcher.GetComponent<verbositySwitcher>().verbose)
        {
            // If we got a swtich in verbosity and we want individuals
            if (verbositySwitcher.GetComponent<verbositySwitcher>().verbose)
            {
                // Set first selected object
                //EventSystem.current.SetSelectedGameObject(verboseButtons[0]);

                foreach (GameObject btn in hierarchyButtons)
                {
                    btn.SetActive(false);
                }
                foreach (GameObject btn in verboseButtons)
                {
                    btn.SetActive(true);
                }
            }

            // If we got a switch in verbosity and we want hierarchical buttons
            else
            {
                // Set first selected object
                //EventSystem.current.SetSelectedGameObject(hierarchyButtons[0]);

                foreach (GameObject btn in hierarchyButtons)
                {
                    
                    btn.SetActive(true);

                }
                foreach (GameObject btn in verboseButtons)
                {

                    btn.SetActive(false);
                }
            }
            pastVerbosity = verbositySwitcher.GetComponent<verbositySwitcher>().verbose;
        }
    }
	

	void createHierarchyButtonObject(List<GameObject> compList, GameObject parent)
    {
        GameObject newButton;
        GameObject sampleComp = compList[0];

        // Create new button object from the prefab
        newButton = (GameObject)Instantiate(Resources.Load("Button_VR_Prefab"));

        // Parent it correctly under the ButtonListContent Object
        newButton.transform.SetParent(parent.transform, false);

        // Change the name to the specific component name
        newButton.name = "ButtonPrefab_" + sampleComp.name.Split('_')[0];

        // Change the text to the specifc component text name
        newButton.GetComponentInChildren<Text>().text = sampleComp.name.Split('_')[0];


        // Add onclick events for the button -- more could be added
        for (int i = 0; i < compList.Count; i++)
        {
            
            int k = i; //Helps with closure issues
            GameObject compNew = compList[k];

            newButton.GetComponent<Button>().onClick.AddListener(delegate { changeStatus(compNew); });// changeStatus(miniComp); });
        }

        newButton.SetActive(false);

        hierarchyButtons.Add(newButton);

    }

    void createButtonObject(GameObject comp, GameObject parent)
    {
        GameObject newButton;

        // Create new button object from the prefab
        newButton = (GameObject)Instantiate(Resources.Load("Button_VR_Prefab"));

        // Parent it correctly under the ButtonListContent Object
        newButton.transform.SetParent(parent.transform, false);

        // Change the name to the specific component name
        newButton.name = "ButtonPrefab_" + comp.name;

        // Change the text to the specifc component text name
        newButton.GetComponentInChildren<Text>().text = comp.name;

        // Add onclick events for the button -- more could be added
        newButton.GetComponent<Button>().onClick.AddListener(delegate
        {
            changeStatus(comp);
        });

        newButton.SetActive(false);
        verboseButtons.Add(newButton);

    }

    void changeStatus(GameObject comp)
    {
        bool isActive = comp.activeInHierarchy;
        if (isActive)
        {
            // Disable the component
            comp.SetActive(false);
        }
        else
        {
            // Enable the component;
            comp.SetActive(true);
        }
    }

    public void resetAll()
    {
        ReactorComponents reactorComponents = AllTheThings.GetComponent<ReactorComponents>();

        foreach (GameObject comp in reactorComponents.reactorComponentsList)
        {
            comp.SetActive(true);
        }

    }



}