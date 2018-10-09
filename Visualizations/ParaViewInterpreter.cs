using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class ParaViewInterpreter: MonoBehaviour {

    private Mesh mesh;

	// Use this for initialization
	void Start () {

        mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector2[] uvs = new Vector2[vertices.Length];

        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
        }
        mesh.uv = uvs;


        Color[] colors = new Color[mesh.vertices.Length];


        Dictionary<Vector3, Color> colorDict = ReadVRMLFile("Assets/MainProject/Resources/testVRML.vrml");

        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            //  Debug.Log(i);

            // Debug.Log(mesh.vertices[i].ToString("G4"));
            //colors[i] = Color.Lerp(Color.green, Color.red, -mesh.vertices[i].x * 10f);
            Debug.Log(mesh.vertices[i].ToString("G8"));
            Debug.Log(colorDict.Keys.ToArray()[0].ToString("G8"));

            Vector3 RoundedVert = new Vector3(-(float)Math.Round(mesh.vertices[i].x, 6), (float)Math.Round(mesh.vertices[i].y, 6),(float)Math.Round(mesh.vertices[i].z, 6));


            colors[i] = colorDict[RoundedVert];
        }

        mesh.colors = colors;

       // Debug.Log(mesh.colors.Length);

       



	}
	

    private Dictionary<Vector3, Color> ReadVRMLFile(string filename)
    {
        bool doingColors = false;
        bool doingVerts = false;

        List<Vector3> vertices = new List<Vector3>();

        Dictionary<Vector3, Color> outDict = new Dictionary<Vector3, Color>();




        int ci = 0;


        StreamReader reader = new StreamReader(filename);

        foreach (string line in reader.ReadToEnd().Split('\n'))
        {
            string trimmedLine = line.TrimStart(' ').Replace(',', ' ') ;

            if (trimmedLine.Contains("color ["))
            {
                doingColors = true;
                continue;
            }
            if (trimmedLine.Contains("point ["))
            {
                
                doingVerts = true;
                continue;
            }



            if (doingVerts)
            {
                
                if (trimmedLine.Contains("]"))
                {
                    doingVerts = false;
                    continue;
                }


                string[] splitLine = trimmedLine.Split(' ');
                Vector3 vert = new Vector3((float)Math.Round(float.Parse(splitLine[0]), 6), (float)Math.Round(float.Parse(splitLine[1]), 6), (float)Math.Round(float.Parse(splitLine[2]), 6));

                vertices.Add(vert);

            }


            if (doingColors)
            {


                if (trimmedLine.Contains("]"))
                {
                    doingColors = false;
                    continue;
                }

                string[] splitLine = trimmedLine.Split(' ');
                Color outColor = new Color(float.Parse(splitLine[0]), float.Parse(splitLine[1]), float.Parse(splitLine[2]));


                outDict[vertices[ci]] = outColor;

                ci++;
            }



        }
        return outDict;
    }

	// Update is called once per frame
	void Update () {


	}
}
