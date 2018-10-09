using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Linq   ;

public class ReactorCompProps : MonoBehaviour
{
    private GameObject currentObj;


    string GetEscapeSequence(char c)
    {
        return "\\u" + ((int)c).ToString("X4");
    }

    public void ReadTextFile(string filePath)
    {
        //Dictionary<string, Dictionary<string,string>> outdict = new Dictionary<string, Dictionary<string, string>>();


        StreamReader reader = new StreamReader(filePath);

        foreach (string line in reader.ReadToEnd().Split('\n'))
        {

            if (!line.StartsWith(" "))
            {
                // Component Name

                //string splitline = line.TrimEnd(' ').TrimStart(' ');

                //foreach (char c in splitline)
                //{
                //    Debug.Log(c + GetEscapeSequence(c));
                //}

                string splitline = new string(line.Where(c => !char.IsControl(c)).ToArray()).Split(' ')[0];

                // Debug.Log(splitline + "--" + "assembly_clad_ss316");
                //Debug.Log(splitline.ToString().Normalize(NormalizationForm.FormKD) == "assembly_clad_ss316");
                currentObj = GameObject.Find(splitline);

                // Debug.Log(splitline == "assembly_can_ss316");
                //currentObj = GameObject.Find("assembly_can_ss316");

                currentObj.AddComponent<CompProps>();

                // Debug.Log(currentObj.name);
            }
            else
            {
                CompProps compProps = currentObj.GetComponent<CompProps>();

                if (line.Contains("Mass:"))
                {
                    compProps.Mass = float.Parse(line.Split(':')[1].Split('+')[0]);
                }
                if (line.Contains("Volume:"))
                {
                    compProps.Volume = float.Parse(line.Split(':')[1].Split('+')[0]);
                }
                if (line.Contains("ConsGroup:"))
                {
                    compProps.consGroup = int.Parse(line.Split(':')[1]);
                }
            }
        }
        reader.Close();
    }

    // Use this for initialization
    void Start()
    {
        ReadTextFile(Application.dataPath + "/OberonProperties.txt");
    }
}
