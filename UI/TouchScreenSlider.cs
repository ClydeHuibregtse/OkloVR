using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchScreenSlider : MonoBehaviour {
    // Put on the slider handle

    Transform oldParent;

    GameObject hand;

    private void Start()
    {
        oldParent = transform.parent;
    }

    private void OnTriggerEnter(Collider other)
    {

        
        hand = other.gameObject;
        //if (other.tag != "Hand") return;

    }

    private void OnTriggerExit(Collider other)
    {
        hand = null;
    }


    private void Update()
    {
        //Debug.Log(hand);
        if (hand != null)
        {
           // Debug.Log((hand.transform.position.x - transform.position.x) * 100 / transform.parent.parent.GetComponent<RectTransform>().rect.width);
           // is trigger is unchecked so only the whole hand can be used as the cursor :( fix this trash code clyde.
            transform.parent.parent.gameObject.GetComponent<Slider>().value += (hand.transform.position.x - transform.position.x) /Time.deltaTime/ transform.parent.parent.GetComponent<RectTransform>().rect.width;
        }
    }
}
