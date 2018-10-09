using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniControls : MonoBehaviour {


    // Update is called once per frame
    void Update () {

        MimicATT();
       
	}
    /// <summary>
    /// Mimics the large All The Things object in the mini All The Things -- Simpler than trying to simply make adjustments to the mini in the same frame
    /// </summary>
   public void MimicATT()
    {
        GameObject ATT = GameObject.Find("AllTheThings");

        for (int i = 0; i < ATT.transform.childCount; ++i)
        {
            // Grab both the miniature component and the large component for comparison
            GameObject miniChild = transform.GetChild(i).gameObject;
            GameObject normChild = ATT.transform.GetChild(i).gameObject;

            // Match activation
            if (miniChild.activeSelf != normChild.activeSelf)
            {
                miniChild.SetActive(normChild.activeSelf);
            }

            // Math material
            if (miniChild.GetComponent<MeshRenderer>().material != normChild.GetComponent<MeshRenderer>().material)
            {
                miniChild.GetComponent<MeshRenderer>().material = normChild.GetComponent<MeshRenderer>().material;
            }
        }
        
    }
}
