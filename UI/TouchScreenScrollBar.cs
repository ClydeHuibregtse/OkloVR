using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchScreenScrollbar : MonoBehaviour
{
    // Put on the slider handle

    Transform oldParent;

    GameObject hand;

    private void Start()
    {
        oldParent = transform.parent;
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("HI");
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

            Debug.Log(hand);
            transform.parent.parent.gameObject.GetComponent<Scrollbar>().value += (hand.transform.position.y - transform.position.y) / Time.deltaTime / transform.parent.parent.GetComponent<RectTransform>().rect.height;
        }
    }
}
