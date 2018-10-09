using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploderControls : MonoBehaviour {

    /// <summary>
    /// Describes the position of the hand anchor holding the mini object in the previous frame.
    /// </summary>
    private float lastPos = 0;

    /// <summary>
    /// Describes the change in position of the hand anchor holding the mini object in the global X direction.
    /// </summary>
    private float dX = 0;

    /// <summary>
    /// Describes whether or not the mini object has been split into its groups. The mini object should not be split more than once.
    /// </summary>
    private bool notSplit = true;

    private bool restartCounter = true;

    /// <summary>
    /// Number of seconds of neglegence until the mini object is destroyed. 
    /// This will likely only reach completion if the user has 'thrown' the mini object and cannot effectively chase it down.
    /// </summary>
    private float neglectedCoolDown = 7f;

    /// <summary>
    /// Time at which the user begins neglecting the mini object.
    /// </summary>
    private float neglectedTimeStamp;

    /// <summary>
    /// Key: string defining the group name. Value: List of GameObjects containing all components in a particular group.
    /// </summary>
    private Dictionary<string, List<GameObject>> sortedComps;


    // Use this for initialization
    void Start () {
        sortedComps = SortComps();
    }

    /// <summary>
    /// Sorts the components into their respesctive groups by the groupnames assigned to them at naming.
    /// <returns>  Returns a dictionary in the form of sortedComps.</returns>
    /// </summary>
    Dictionary<string, List<GameObject>> SortComps()
    {

        Dictionary<string, List<GameObject>> out_dict = new Dictionary<string, List<GameObject>>();

        for (int i=0; i < transform.childCount; ++i)
        {
            GameObject child = transform.GetChild(i).gameObject;
            
            string group = child.name.Split('_')[0];

            if (out_dict.ContainsKey(group))
            {
                out_dict[group].Add(child);
            }
            else
            {
                out_dict[group] = new List<GameObject>();
                out_dict[group].Add(child);
            }

            
        }

        return out_dict;
    }


    // Update is called once per frame
    void Update () {

        Explode();
    }

    /// <summary>
    /// Controls cooldowns for neglected objects and calls the split methods if user wants an exploded model.
    /// </summary>
    void Explode()
    {

        if (restartCounter)
        {
            // Begin counting to the next cooldown.
            neglectedTimeStamp = Time.time + neglectedCoolDown;
        }

        // User has grabbed the object and is no longer neglecting it
        if (GetComponent<OVRGrabbable>().grabbedBy != null)
        {
            dX = GetComponent<OVRGrabbable>().grabbedBy.transform.position.x - lastPos;
            lastPos = GetComponent<OVRGrabbable>().grabbedBy.transform.position.x;
            restartCounter = true;
        }
        // User is now neglecting the obejct
        else
        {
            dX = 0;
            restartCounter = false;
        }

        if (Time.time >= neglectedTimeStamp)
        {
            // Object has been neglected for too long - destroy it so the user can create a new one.
            Destroy(gameObject);
        }

        // User has shaken the object quickly enough in the X direction
        if (Mathf.Abs(dX * 100) > 2.5 && Mathf.Abs(dX * 100) < 5f && notSplit)
        {
            notSplit = false;
            SplitComps(sortedComps);
        }

        if (!notSplit && GetComponent<OVRGrabbable>().grabbedBy == null)
        {
            Destroy(gameObject);
        }

    }


    /// <summary>
    /// Separates the components along a line by a specified distance such that each group is visible in its own space.
    /// </summary>
    /// <param name="sorted"> Dictionary containing sorted components by key: groupname, value: list of objects</param>
    void SplitComps( Dictionary<string, List<GameObject>> sorted )
    {
        float splitdiff = .5f;
        int counter = 0;
        if (sorted.Count % 2 == 0)
            counter = -sorted.Count / 2;
        else
            counter = -(sorted.Count - 1) / 2;
        foreach (KeyValuePair<string, List<GameObject>> entry in sorted)
        {
            foreach (GameObject comp in entry.Value)
            {
                comp.transform.position += new Vector3(splitdiff * counter, 0, 0);
            }
            counter += 1;



        }



    }

}
