using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class ColorByHolder : MonoBehaviour {


    private GameObject colorByContent;

	// Use this for initialization
	void Start () {
        GameObject.Find("ColorBy_Mass_btn").GetComponentInChildren<Button>().onClick.AddListener(delegate
        {
            colorBy("Mass");

        });
        GameObject.Find("ColorBy_Volume_btn").GetComponentInChildren<Button>().onClick.AddListener(delegate
        {
            colorBy("Volume");

        });
        GameObject.Find("ColorBy_Material_btn").GetComponentInChildren<Button>().onClick.AddListener(delegate
        {
            colorBy("Material");

        });
        GameObject.Find("ColorBy_AvgHeat_btn").GetComponentInChildren<Button>().onClick.AddListener(delegate
        {
            colorBy("AvgHeat");

        });

	}


    public void colorBy(string indicator)
    {
        GameObject[] reactorComps = GameObject.FindGameObjectsWithTag("ReactorComponent");

        //Maximums - used for scaling colors
        float maxVol = reactorComps.Max(c => c.GetComponent<CompProps>().Volume);
        float maxMass = reactorComps.Max(c => c.GetComponent<CompProps>().Mass);
        float maxAvgHeat = reactorComps.Max(c => c.GetComponent<CompProps>().avgHeat);

        if (indicator == "Mass")
        {

            foreach (GameObject comp in reactorComps)
            {
                comp.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.black, Color.blue, comp.GetComponent<CompProps>().Mass/ maxMass);
                //comp.GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, comp.GetComponent<CompProps>().Mass / maxMass, 0.5f);
            }
        }
        if (indicator == "Volume")
        {
                 
            foreach (GameObject comp in reactorComps)
            {
                //comp.GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, comp.GetComponent<CompProps>().Volume / maxVol, 0.5f);
                comp.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.black, Color.blue, comp.GetComponent<CompProps>().Volume / maxVol);
            }
        }
        if (indicator == "Material")
        {
            
            foreach (GameObject comp in reactorComps)
            {
                GameObject.Find("AllTheThings").GetComponent<ReactorComponents>().setMat(comp);
            }
        }
        if (indicator == "AvgHeat")
        {

            foreach (GameObject comp in reactorComps)
            {
                comp.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.black, Color.red, comp.GetComponent<CompProps>().avgHeat/ maxAvgHeat);
                //comp.GetComponent<MeshRenderer>().material.color = new Color(comp.GetComponent<CompProps>().avgHeat / maxAvgHeat,0f,0f, 0.5f);
            }
        }
    }
}
