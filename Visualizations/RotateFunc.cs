using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFunc : MonoBehaviour {

    public float modelRotateSpeed = 60;

    public bool rotateable = false;

    private Vector3 offset;

    void Awake()
    {
        offset = GetComponent<MeshRenderer>().bounds.center + Vector3.right;
    }

	// Update is called once per frame
	void Update () {
		
        if (rotateable)
        { 
            transform.RotateAround(offset, Vector3.up, Time.deltaTime * modelRotateSpeed);
        }

	}
}
