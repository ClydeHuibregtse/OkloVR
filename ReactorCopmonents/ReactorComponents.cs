using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

public class ReactorComponents : MonoBehaviour
{
    public List<GameObject> reactorComponentsList = new List<GameObject>();

    // Dictionary to sort components by grouping
    public Dictionary<string, List<GameObject>> groupNames = new Dictionary<string, List<GameObject>>();
    // List of all seen materials - will use for color key
    public List<string> matList = new List<string>();


    // Use this for initialization
    public void Start()
    {
        // Store the reactorComponents in a list of GameObjects BEFORE activating/deactivating - 
        // this way we retain the information without being unable to locate the objects again

        foreach (var newComp in GameObject.FindGameObjectsWithTag("ReactorComponent"))
        {
            setMat(newComp);
            sortComp(newComp);
            reactorComponentsList.Add(newComp);
        }


    }

	void sortComp( GameObject newComp)
    {
        //Add a mesh collider for raycasting
        //newComp.AddComponent(typeof(MeshCollider));

        string compName = newComp.name;
        string[] compNameSplit = compName.Split('_');

        string groupName = compNameSplit[0];
        if (groupNames.ContainsKey(groupName)){
            groupNames[groupName].Add(newComp);
        }
        else{
            groupNames[groupName] = new List<GameObject>();
            groupNames[groupName].Add(newComp);
        }

    }



    public void setMat( GameObject newComp)
    {
        //Debug.Log(newComp.name);
        string compName = newComp.name;
        string[] matNames = compName.Split('_');

        if (!matList.Contains(matNames[2])){
            matList.Add(matNames[2]);
        }

        // Maybe just generate new color every time it sees a new material type instead of whole material
        newComp.GetComponent<MeshRenderer>().material = Resources.Load("Materials/" + matNames[2]) as Material;

    }

}   
