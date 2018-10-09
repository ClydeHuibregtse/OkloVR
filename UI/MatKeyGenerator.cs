using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatKeyGenerator : MonoBehaviour {

    [Header("Materials Content")]
    public GameObject ParentMatContent;


    // Use this for initialization
    void Start () {
        
        GameObject AllTheThings = GameObject.Find("AllTheThings");
       

        ReactorComponents reactorComponents = AllTheThings.GetComponent<ReactorComponents>();

        foreach (string matName in reactorComponents.matList){
            createMatKeyObject(matName, ParentMatContent);

        }


    }
    
    void createMatKeyObject( string matName, GameObject parent)
    {
        // Generate the new 
        Material newMat = (Material)Instantiate(Resources.Load("Materials/" + matName));
        GameObject newKeyEle = (GameObject)Instantiate(Resources.Load("MaterialPrefab"));

        // Organize hierarchy
        Transform imageChild = newKeyEle.transform.GetChild(1);

        newKeyEle.name = matName + "_MaterialPrefab";
        newKeyEle.transform.SetParent(parent.transform,false);

        // Set the proper color and textB
        newKeyEle.GetComponentInChildren<Text>().text = matName;
        imageChild.GetComponentInChildren<Image>().color = newMat.color; 


    }

}
