using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMenu : MonoBehaviour {

    [Header("Panel Container")]
    public GameObject menuPanel;

    [Header("Player Object")]
    public GameObject player;

    [Header("Action Menu animator")]
    public Animator anim;

    
    /// <summary>
    /// Bool: is true if the action menu is visible
    /// </summary>
    private bool isShowing = false;


    void Update()
    {
        openClose();   
    }

    void openClose()
    {
        if (transform.localScale.y == 0)
        {
            menuPanel.SetActive(false);
        }


        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch)) // Y button
        {

            isShowing = !isShowing;

            if (isShowing)
            {
                // OPEN 

                menuPanel.SetActive(true);

                // Subject to adjustments -- Place menu in front of the user
                transform.position = player.transform.position;
                transform.position += new Vector3(0, 0, 4.5f);

                // Stop the player from being able to move
                player.GetComponent<OVRPlayerController>().Acceleration = 0;

                // Open anim
                anim.ResetTrigger("CloseMenu");
                anim.SetTrigger("OpenMenu");
            }
            else
            {
                // CLOSE

                // Close anim
                anim.ResetTrigger("OpenMenu");
                anim.SetTrigger("CloseMenu");

                // Allow the player to move again
                player.GetComponent<OVRPlayerController>().Acceleration = .5f;
            }

        }

    }

}
