using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonitorControls : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Text monitorText = GetComponentInChildren<Text>();

        StartCoroutine(TextSwapper(monitorText));


	}


    IEnumerator TextSwapper(Text monText)
    {
        monText.text = "Oklo Inc. >>>";
        yield return new WaitForSeconds(.5f);
        monText.text = "Oklo Inc. >>> |";
        yield return new WaitForSeconds(.5f);
        StartCoroutine(TextSwapper(monText));

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
    }
}
