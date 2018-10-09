using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleController : MonoBehaviour {

    public float rotateSpeed;

    [Range(0,1)]
    public float screechLikelihood;

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(0, -Time.deltaTime * rotateSpeed, 0);


        if (Random.value < screechLikelihood)
        {
            GetComponent<AudioSource>().Play(0);
        }
	}
}
