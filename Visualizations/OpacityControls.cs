using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpacityControls : MonoBehaviour {

    public float transparencySpeed = 1;

    private GameObject ground;
    private GameObject grass;
    private GameObject wood;

    private bool stairsUp = false;
    private bool changeOpa = true;

    // Use this for initialization
    void OnEnable () {
        wood = GameObject.Find("WoodPlatforming");
        wood.SetActive(false);
        ground = GameObject.Find("ground");
        grass = GameObject.Find("grass");

	}
	
	// Update is called once per frame
	void Update () {

        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch))
        {
            SwitchToStairs();

        }


        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            changeOpa = !changeOpa;

        }




        if (changeOpa)
        {
            StandardShaderUtils.ChangeRenderMode(grass.GetComponent<Renderer>().material, StandardShaderUtils.BlendMode.Transparent);
            StandardShaderUtils.ChangeRenderMode(ground.GetComponent<Renderer>().material, StandardShaderUtils.BlendMode.Transparent);
            ChangeOpacity();
        }
        else
        {
            StandardShaderUtils.ChangeRenderMode(grass.GetComponent<Renderer>().material, StandardShaderUtils.BlendMode.Opaque);
            StandardShaderUtils.ChangeRenderMode(ground.GetComponent<Renderer>().material, StandardShaderUtils.BlendMode.Opaque);
        }
        
    }



    private void ChangeOpacity()
    {

        Color grassTmp = grass.GetComponent<Renderer>().material.color;
        Color groundTmp = ground.GetComponent<Renderer>().material.color;
        grassTmp.a += OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch)[1] * Time.deltaTime * transparencySpeed;
        groundTmp.a += OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch)[1] * Time.deltaTime * transparencySpeed;

        
        // Clamp the alpha between 0 and 1
        if (grassTmp.a < 0) grassTmp.a = 0;
        if (grassTmp.a > 1) grassTmp.a = 1;



        // Clamp the alpha between 0 and 1
        if (groundTmp.a < 0) groundTmp.a = 0;
        if (groundTmp.a > 1) groundTmp.a = 1;

        grass.GetComponent<Renderer>().material.color = grassTmp;
        ground.GetComponent<Renderer>().material.color = groundTmp;
    }


    private void SwitchToStairs()
    {

        if (stairsUp)
        {
            grass.SetActive(true);
            ground.SetActive(true);
            wood.SetActive(false);
        }
        else
        {
            grass.SetActive(false);
            ground.SetActive(false);
            wood.SetActive(true);
        }
        stairsUp = !stairsUp;

    }
}
