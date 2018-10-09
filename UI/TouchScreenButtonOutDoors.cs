using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class TouchScreenButtonOutDoors : MonoBehaviour
{

    public TouchScreenControllerOutDoor tsController;

    private Animator anim;

    public GameObject assocGameObj;


    private void OnTriggerEnter(Collider other)
    {

        if (other.name != "HandCollider") return;
        VRButtonFuncs.VibrateTouch(other);

        string displayName = gameObject.name;

        anim.SetTrigger("TouchClick");

        //Debug.Log(gameObject.tag);

        switch (gameObject.tag)
        {
            case "PartButton":
                StartCoroutine(tsController.ChangePartStatus(assocGameObj));
                break;

            case "ScrollBarButton":
                StartCoroutine(tsController.Scroll(gameObject));
                break;

            default:
                StartCoroutine(tsController.SwitchToCanvas(displayName));
                break;

        }

    }



    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        tsController = GameObject.Find("FloatingScreen").GetComponent<TouchScreenControllerOutDoor>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

