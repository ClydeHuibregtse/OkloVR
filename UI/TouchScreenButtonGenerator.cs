using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchScreenButtonGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CreateButtons();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void CreateButtons()
    {
        foreach (GameObject comp in GameObject.FindGameObjectsWithTag("ReactorComponent"))
        {
            GameObject newButton = (GameObject)Instantiate(Resources.Load("TouchScreenButtonPrefab"));

            newButton.transform.SetParent(transform,false);
            newButton.transform.localEulerAngles = Vector3.zero;
            newButton.tag = "PartButton";
            newButton.GetComponent<TouchScreenButtonOutDoors>().assocGameObj = comp;

            //newButton.GetComponent<BoxCollider>().size = new Vector3(newButton.GetComponent<RectTransform>().rect.width, newButton.GetComponent<BoxCollider>().size.y, newButton.GetComponent<BoxCollider>().size.z);

            newButton.name = comp.name + "_TSButton";

            newButton.GetComponentInChildren<Text>().text = comp.name;
            newButton.GetComponentInChildren<Text>().fontSize = 30;

        }

    }

}
