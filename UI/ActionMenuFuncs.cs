using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionMenuFuncs : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnButtonClick()
    {

        Debug.Log("EHH");

    }


    public void OnHoverEnter(Transform t)
    {
        Debug.Log("E");
        Debug.Log("Entered: " + t.gameObject.name);

    }

    public void OnHoverExit(Transform t)
    {
        Debug.Log("Ex");
        Debug.Log("Left: " + t.gameObject.name);

    }

    public void OnSelected(Transform t)
    {
        Debug.Log("S");
        Debug.Log("Selected: " + t.gameObject.name);
    }
}
