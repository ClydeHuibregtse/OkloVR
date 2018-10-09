using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class TouchScreenControllerOutDoor : MonoBehaviour
{

    private GameObject mainPanel;
    private Animator mainAnim;
    private GameObject partPanel;
    private GameObject rotatePanel;

    private bool isRotating = false;

    private List<GameObject> allPanels = new List<GameObject>();
    [HideInInspector]
    public GameObject activePanel;

    // Use this for initialization
    void Start()
    {

        mainPanel = transform.GetChild(0).gameObject;
        mainAnim = mainPanel.GetComponent<Animator>();
        partPanel = transform.GetChild(1).gameObject;
        activePanel = mainPanel;
        //colorByPanel.SetActive(false);



    }

    public IEnumerator Scroll(GameObject button)
    {
        yield return new WaitForSeconds(.617f);

        GameObject listContent = button.transform.parent.gameObject;

        float scrollAmt = 4f / listContent.transform.GetChild(0).GetChild(0).GetChildCount();
        

        if (button.name == "PartUp")
            listContent.GetComponent<ScrollRect>().verticalNormalizedPosition += scrollAmt;
        //listContent.GetComponent<ScrollRect>().velocity = new Vector2(0, scrollSpeed);
        else
            listContent.GetComponent<ScrollRect>().verticalNormalizedPosition -= scrollAmt;
            
            //listContent.GetComponent<ScrollRect>().velocity = new Vector2(0, scrollSpeed);

        //yield return new WaitForSeconds(2f);

        //listContent.GetComponent<ScrollRect>().velocity = Vector2.zero;


    }


    public IEnumerator SwitchToCanvas(string buttonName)
    {
        yield return new WaitForSeconds(.617f);

        activePanel.GetComponent<Animator>().SetTrigger("ScreenOff");
        activePanel.GetComponent<Animator>().ResetTrigger("ScreenOn");

        yield return new WaitForSeconds(.75f);

        switch (buttonName)
        {
            case "Parts":

                partPanel.GetComponent<Animator>().ResetTrigger("ScreenOff");
                partPanel.GetComponent<Animator>().SetTrigger("ScreenOn");
                activePanel = partPanel;
                break;

            case "Return":
                mainAnim.SetTrigger("ScreenOn");
                mainAnim.ResetTrigger("ScreenOff");
                activePanel = mainPanel;
                break;
        }
    }


    public IEnumerator ChangePartStatus(GameObject assocGO)
    {
        yield return new WaitForSeconds(.617f);

        assocGO.SetActive(!assocGO.activeSelf);

    }


    //public IEnumerator Return()
    //{

    //    yield return new WaitForSeconds(.617f);


    //    activePanel.GetComponent<Animator>().SetTrigger("ScreenOff");
    //    activePanel.GetComponent<Animator>().ResetTrigger("ScreenOn");
    //    yield return new WaitForSeconds(.75f);

    //    //activePanel.SetActive(false);
    //    //mainPanel.SetActive(true);
    //    mainAnim.SetTrigger("ScreenOn");
    //    mainAnim.ResetTrigger("ScreenOff");

    //    activePanel = mainPanel;
    //}


}
