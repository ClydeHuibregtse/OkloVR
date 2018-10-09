using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class controls the laser mode functionality in the Control Room scene. 
/// It has methods for raycasting and moving individual reactor components.
/// UNDER DEVELOPMENT
/// </summary>
public class LaserModeControls : MonoBehaviour {

    // Hand anchors
    public GameObject lHand;
    public GameObject rHand;


    // Audio
    private AudioSource aSourceGrab;
    public AudioClip aClipGrab;

    // Sensitivities
    public float sensitivity; // translational
    public float expSens; // expansion

    // storage pointers for raycasted targets
    private GameObject rgrabbedObj = null;
    private GameObject lgrabbedObj = null;

    private LineRenderer lRend;
    private LineRenderer rRend;

    private RaycastHit rLastHit;
    private RaycastHit lLastHit;


    // storage pointers for hand movements
    private Vector3 rDeltaMov;
    private Vector3 lDeltaMov;
    private Vector3 rLastMov;
    private Vector3 lLastMov;

    private Vector3 delta;





    public AudioSource SetAudioSource(AudioClip clip, bool loop, bool playAwake, float vol)
    {
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.clip = clip;
        newSource.loop = loop;
        newSource.playOnAwake = playAwake;
        newSource.volume = vol;

        return newSource;

    }

    private void Awake()
    {
        aSourceGrab = SetAudioSource(aClipGrab, false, false, .2f);

    }

    /// <summary>
    /// Raycasts and returns the collided object that should be connected 
    /// to whichHand - adjusts the pointers for the appropriate grabbed object.
    /// </summary>
    /// <param name="whichHand"></param>
    /// <returns></returns>
    private GameObject RayCastSearch(GameObject whichHand)
    {
        RaycastHit hit;
        // initiate the layermask
        int layerMask = 1 << 9;
        layerMask = ~layerMask; // we want every layer except for the 8th to be masked


        /////////// NEEDS UPDATE - Switch method for updating position to: grab-exit function-update-drop instead of grab/update ///////////
        if (whichHand == rHand)
        {
            
            Vector3[] rPoints = { whichHand.transform.position, (whichHand.transform.forward * 500 + whichHand.transform.position) };
 

            rRend.positionCount = rPoints.Length;
            rRend.SetPositions(rPoints);

            if (Physics.Raycast(whichHand.transform.position, whichHand.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {

                if (hit.transform.tag != "ReactorComponent") return null;

                if (rLastHit.transform != null)
                {
                    rLastHit.transform.gameObject.GetComponent<Renderer>().material.color = Color.white;
                }

                hit.transform.gameObject.GetComponent<Renderer>().material = (Material)Instantiate(Resources.Load("Materials/LaserMode"));

                if (rLastMov != null)
                    rDeltaMov = rPoints[1] - rLastMov;

                if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) == 1)
                {

                    if (rgrabbedObj == null)
                    {
                        aSourceGrab.Play(0);
                        VRButtonFuncs.VibrateTouch(rHand.GetComponent<Collider>());
                        rgrabbedObj = hit.transform.gameObject;
                    }
                }
                else
                {
                    rgrabbedObj = null;
                }

                rLastMov = rPoints[1];


                rLastHit = hit;

                return hit.collider.gameObject;
            }
            else return null;

        }
        else
        {
            Vector3[] lPoints = { whichHand.transform.position, (whichHand.transform.forward * 500 + whichHand.transform.position) };


            lRend.positionCount = lPoints.Length;
            lRend.SetPositions(lPoints);

            if (Physics.Raycast(whichHand.transform.position, whichHand.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {

                if (hit.transform.tag != "ReactorComponent") return null;

                if (lLastHit.transform != null)
                {
                    lLastHit.transform.gameObject.GetComponent<Renderer>().material.color = Color.white;
                }
                
                hit.transform.gameObject.GetComponent<Renderer>().material = (Material)Instantiate(Resources.Load("Materials/LaserMode"));


                if (lLastMov != null)
                    lDeltaMov = lPoints[1] - lLastMov;

                if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch) == 1)
                {
                    if (lgrabbedObj == null)
                    {
                        aSourceGrab.Play(0);
                        VRButtonFuncs.VibrateTouch(lHand.GetComponent<Collider>());
                        lgrabbedObj = hit.transform.gameObject;

                    }

                }
                else
                {
                    lgrabbedObj = null;
                }

                lLastMov = lPoints[1];

                lLastHit = hit;

                return hit.collider.gameObject;
            }
            else return null;

        }

    }



	// Use this for initialization
	void Start () {

        lRend = lHand.GetComponent<LineRenderer>();
        rRend = rHand.GetComponent<LineRenderer>();

    }


    void LateUpdate() {

        // Adjust the correct pointers
        RayCastSearch(rHand);
        RayCastSearch(lHand);


        ////// Needs update - expansion may not be worth adding but could be cool //////
        delta  = rDeltaMov + lDeltaMov;

        if (rgrabbedObj == lgrabbedObj && rgrabbedObj != null)
        {
            Debug.Log("Expand");
            rgrabbedObj.transform.localScale += delta.magnitude * Time.deltaTime * expSens * Vector3.one;
            

        }
        else
        {
            if (rgrabbedObj != null)
            {
                rgrabbedObj.transform.position += rDeltaMov * sensitivity * Time.deltaTime;

            }
            if (lgrabbedObj != null)
            {
                lgrabbedObj.transform.position += lDeltaMov * sensitivity * Time.deltaTime;
            }
        }



    }
}
