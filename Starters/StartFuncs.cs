using UnityEngine;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine.UI;

public class StartFuncs : MonoBehaviour
{
    public void LoadModel( string filename )
    {
        // Turn off the input field:
        GameObject inputField = GameObject.Find("InputFieldHolder");
        inputField.SetActive(false);

        bool good_load = false;


      

        //ObjReader.use.ConvertFile("/Users/chuibregtse/Documents/OkloVR/oklovr/Oklo_VR_1.1/Assets/" + filename, true);
        //Debug.Log(Application.dataPath + "Assets / " + filename);
        ObjReader.use.ConvertFile(Application.dataPath + "/" + filename, true);

        GameObject AllTheThings = GameObject.Find("AllTheThings");

        foreach (GameObject component in GameObject.FindObjectsOfType(typeof(GameObject)))
        {
            if (component.name.Contains("_"))
            {
                component.transform.SetParent(AllTheThings.transform);
                good_load = true;
            }

        }

        foreach (Transform child in AllTheThings.transform)
        {

            child.tag = "ReactorComponent";
            child.gameObject.AddComponent<MeshCollider>();

        }

        AllTheThings.AddComponent<ReactorCompProps>();
        AllTheThings.AddComponent<ReactorComponents>();
        if (!good_load)
        {
            Debug.Log(inputField.GetComponentsInChildren<Text>()[2].text);
            inputField.GetComponentsInChildren<Text>()[2].text += "\nBad Entry";
            inputField.SetActive(true);
        }

    }


	// Use this for initialization
	void Start()
	{



        //Mesh holderMesh = new Mesh();
        //OBJLoader newMesh = new OBJLoader();
        //GameObject newModel = OBJLoader.LoadOBJFile("/Users/chuibregtse/Documents/OkloVR/oklovr/Oklo_VR_1.1/Assets/testfile.obj");
        ////ObjLoad.SetGeometryData("/Users/chuibregtse/Documents/OkloVR/oklovr/Oklo_VR_1.1/Assets/testfile.obj");




	}

	// Update is called once per frame
	void Update()
	{
			
	}
}
