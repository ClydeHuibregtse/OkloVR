using UnityEngine;
using System.Collections;

public class CompProps : MonoBehaviour
{
    public float Mass = 0;
    public float Volume = 0;
    public float avgHeat;
    public string materialName = "Anything I want it to be";
    public float anyProp = 12345.678f;
    public int consGroup;


	public void Start()
	{
        avgHeat = Random.value;
	}
}
