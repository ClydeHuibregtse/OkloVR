using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizationTools : MonoBehaviour {

    [Header("Rate of Opacity Change")]
    public float transparencySpeed = 25;

    [Header("Rate of Scale Change")]
    public float scaleSpeed = 1f;

    private GameObject pivotHolder;

    private Vector3 newScale;


    private OVRGrabbable OVRGrab;

    [HideInInspector]
    public Color oldColor;

    // Use this for initialization
    void Start () {

        //pivotHolder = new GameObject(gameObject.name + "_Pivot");
        //pivotHolder.transform.parent = transform.parent;

        //pivotHolder.transform.position = GetComponent<Renderer>().bounds.center;

        //transform.parent = pivotHolder.transform

        //oldParent = transform.parent.gameObject;

        OVRGrab = GetComponent<OVRGrabbable>();
        oldColor = GetComponent<Renderer>().material.color;
    }
	
	// Update is called once per frame
	void Update () {
        
        if (OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.LTouch))
        {
            ChangeTransparency();
        }
        if (OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            ChangeScale();
        }
        if (OVRInput.GetUp(OVRInput.Button.Two, OVRInput.Controller.LTouch))
        {
            // Reset the blend mode and return to original color
            StandardShaderUtils.ChangeRenderMode(GetComponent<Renderer>().material, StandardShaderUtils.BlendMode.Opaque);
            GetComponent<Renderer>().material.color = oldColor;
        }

    }


    void ChangeTransparency()
    {
        if (OVRGrab.grabbedBy)
        {
            //Change blend mode
            StandardShaderUtils.ChangeRenderMode(GetComponent<Renderer>().material, StandardShaderUtils.BlendMode.Transparent);

            Color tmp = GetComponent<Renderer>().material.color;
            tmp.a += OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch)[1] * Time.deltaTime * transparencySpeed;

            // Clamp the alpha between 0 and 1
            if (tmp.a < 0) tmp.a = 0;
            if (tmp.a > 1) tmp.a = 1;

            GetComponent<Renderer>().material.color = tmp;
        }
    }

    //public static void ScaleAround(Transform target, Transform pivot, Vector3 scale)
    //{
    //    Transform pivotParent = pivot.parent;
    //    Vector3 pivotPos = pivot.position;
    //    pivot.parent = target;
    //    target.localScale = scale;
    //    target.position += pivotPos - pivot.position;
    //    pivot.parent = pivotParent;
    //}

    void ChangeScale()
    {
        if (OVRGrab.grabbedBy)
        {
            //transform.SetParent(OVRGrab.grabbedBy.transform);

            float input = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch)[1];

            //Vector3 A = pivotHolder.transform.position;
            //Vector3 B = OVRGrab.grabbedBy.transform.position;

            //Vector3 FP = (A - B) * input * Time.deltaTime * scaleSpeed + B;

            //pivotHolder.transform.position = OVRGrab.grabbedBy.transform.position;
            transform.localScale += new Vector3(input, input, input) * Time.deltaTime * scaleSpeed;


            //ScaleAround(transform, OVRGrab.grabbedBy.transform, newScale);

        }

        else
        {
           // transform.SetParent(oldParent.transform);
        }


    }

}
