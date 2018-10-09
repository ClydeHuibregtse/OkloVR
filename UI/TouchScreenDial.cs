using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchScreenDial : MonoBehaviour {

    public SmallSceneInputManager input;

    public float dispValue = 0;

    private float curAngle = 0;

    private Transform hand;
	// Use this for initialization
	void Start () {

        
        input = GameObject.Find("PhysicsStarter").GetComponent<SmallSceneInputManager>();
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ENTER");
        hand = other.transform;
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("EXIT");
        hand = null;
    }
    // Update is called once per frame
    void Update () {

        if ((input.rPHTrig == 1 && input.rPITrig == 1 && input.rNearTouch) || (input.lPHTrig == 1 && input.lPITrig == 1 && input.lNearTouch))
        {
            if (hand != null && hand.tag == "Hand")
            {
                
                //Debug.Log(hand.transform.localRotation.z);
                dispValue += (hand.localRotation.z - curAngle)/Time.deltaTime;
                curAngle = hand.localRotation.z;

            }

            GetComponentInChildren<Text>().text = dispValue.ToString();
        }


        GameObject.Find("TESTER").GetComponent<Image>().color = new Color(Mathf.Abs(dispValue) / 100, 0, 0);

	}

    
}
