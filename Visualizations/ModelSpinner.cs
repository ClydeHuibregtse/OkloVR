using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSpinner : MonoBehaviour {
    /// <summary>
    /// Speed of model rotation
    /// </summary>
    public float modelRotateSpeed = 60f;

    private Vector3 rotateCenter;

    // Use this for initialization
    void Start () {
        rotateCenter = transform.parent.transform.position;
    }
	
	// FixedUpdate is necessary
	void FixedUpdate () {
        transform.Rotate(transform.parent.transform.forward, Time.deltaTime * modelRotateSpeed, Space.World);

	}
}
