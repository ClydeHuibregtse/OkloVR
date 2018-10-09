using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class TouchScreenController : MonoBehaviour {


    private GameObject mainPanel;
    private Animator mainAnim;
    private GameObject colorByPanel;
    private GameObject rotatePanel;

    private bool isRotating = false;

    private List<GameObject> allPanels = new List<GameObject>();
    [HideInInspector]
    public GameObject activePanel;

    // Use this for initialization
    void Start () {

        mainPanel = transform.GetChild(0).gameObject;
        mainAnim = mainPanel.GetComponent<Animator>();
        colorByPanel = transform.GetChild(1).gameObject;
        rotatePanel = transform.GetChild(2).gameObject;
        activePanel = mainPanel;
        //colorByPanel.SetActive(false);

        

    }

    public IEnumerator SwitchToColorByCanvas()
    {
        yield return new WaitForSeconds(.617f);

        mainAnim.SetTrigger("ScreenOff");
        mainAnim.ResetTrigger("ScreenOn");

        yield return new WaitForSeconds(.75f);

        colorByPanel.GetComponent<Animator>().SetTrigger("ScreenOn");
        colorByPanel.GetComponent<Animator>().ResetTrigger("ScreenOff");

        //colorByPanel.SetActive(true);
        //mainPanel.SetActive(false);
        activePanel = colorByPanel;

    }
    public IEnumerator SwitchToRotateCanvas()
    {
        yield return new WaitForSeconds(.617f);

        mainAnim.SetTrigger("ScreenOff");
        mainAnim.ResetTrigger("ScreenOn");

        yield return new WaitForSeconds(.75f);

        rotatePanel.GetComponent<Animator>().SetTrigger("ScreenOn");
        rotatePanel.GetComponent<Animator>().ResetTrigger("ScreenOff");

        //colorByPanel.SetActive(true);
        //mainPanel.SetActive(false);
        activePanel = rotatePanel;

    }

    public IEnumerator colorBy(string indicator)
    {
        yield return new WaitForSeconds(.617f);

        GameObject[] reactorComps = GameObject.FindGameObjectsWithTag("ReactorComponent");

        //Maximums - used for scaling colors
        float maxVol = reactorComps.Max(c => c.GetComponent<CompProps>().Volume);
        float maxMass = reactorComps.Max(c => c.GetComponent<CompProps>().Mass);
        float maxAvgHeat = reactorComps.Max(c => c.GetComponent<CompProps>().avgHeat);

        if (indicator == "Mass")
        {

            foreach (GameObject comp in reactorComps)
            {
 
                comp.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.black, Color.blue, comp.GetComponent<CompProps>().Mass / (maxMass + .001f)); // Stops from divide by 0 errors
                comp.GetComponent<VisualizationTools>().oldColor = comp.GetComponent<MeshRenderer>().material.color;
                //comp.GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, comp.GetComponent<CompProps>().Mass / maxMass, 0.5f);
            }
        }
        if (indicator == "Volume")
        {

            foreach (GameObject comp in reactorComps)
            {
                //comp.GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, comp.GetComponent<CompProps>().Volume / maxVol, 0.5f);
                comp.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.black, Color.blue, comp.GetComponent<CompProps>().Volume / (maxVol + .001f)); // Stops from divide by 0 errors
                comp.GetComponent<VisualizationTools>().oldColor = comp.GetComponent<MeshRenderer>().material.color;

            }
        }
        if (indicator == "Material")
        {

            foreach (GameObject comp in reactorComps)
            {
               // GameObject.Find("SingleCan").GetComponent<ReactorComponents>().setMat(comp);
               // comp.GetComponent<VisualizationTools>().oldColor = comp.GetComponent<MeshRenderer>().material.color;

            }
        }
        if (indicator == "AvgHeat")
        {

            foreach (GameObject comp in reactorComps)
            {
                comp.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.black, Color.red, comp.GetComponent<CompProps>().avgHeat / (maxAvgHeat + .001f)); // Stops from divide by 0 errors
                comp.GetComponent<VisualizationTools>().oldColor = comp.GetComponent<MeshRenderer>().material.color;

                //comp.GetComponent<MeshRenderer>().material.color = new Color(comp.GetComponent<CompProps>().avgHeat / maxAvgHeat,0f,0f, 0.5f);
            }
        }
    }

    public IEnumerator Return()
    {
        
        yield return new WaitForSeconds(.617f);


        activePanel.GetComponent<Animator>().SetTrigger("ScreenOff");
        activePanel.GetComponent<Animator>().ResetTrigger("ScreenOn");
        yield return new WaitForSeconds(.75f);

        //activePanel.SetActive(false);
        //mainPanel.SetActive(true);
        mainAnim.SetTrigger("ScreenOn");
        mainAnim.ResetTrigger("ScreenOff");

        activePanel = mainPanel;
    }


    public IEnumerator RotateModel(Collider other)
    {
        yield return new WaitForSeconds(.617f);
        GameObject rotateLight =  new GameObject("RotateLight");

        VRButtonFuncs.RotateComponents(other, isRotating);

        isRotating = !isRotating;
    }


    public IEnumerator RotateUp()
    {
        yield return new WaitForSeconds(.617f);
        GameObject.Find("SingleCan").GetComponent<RotateFunc>().modelRotateSpeed += 20;
        GameObject.Find("RotateSpeedImg").GetComponentInChildren<Text>().text = GameObject.Find("SingleCan").GetComponent<RotateFunc>().modelRotateSpeed.ToString();

    }

    public IEnumerator RotateDown()
    {
        yield return new WaitForSeconds(.617f);
        GameObject.Find("SingleCan").GetComponent<RotateFunc>().modelRotateSpeed -= 20;
        GameObject.Find("RotateSpeedImg").GetComponentInChildren<Text>().text = GameObject.Find("SingleCan").GetComponent<RotateFunc>().modelRotateSpeed.ToString();

    }

    // Update is called once per frame
    void Update () {
		
	}
}
