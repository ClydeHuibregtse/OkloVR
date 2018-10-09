using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.EventSystems;

public class VideoController : MonoBehaviour {

    public EventSystem m_EventSystem;

    private GameObject lastSelected;

    private void Start()
    {
        foreach (GameObject image in GameObject.FindGameObjectsWithTag("MainMenu"))
        {
            image.GetComponent<VideoPlayer>().enabled = false;
        } 
    }

    private void Update()
    {
        //Debug.Log(m_EventSystem.currentSelectedGameObject);

        //if (m_EventSystem.currentSelectedGameObject == null && m_EventSystem.currentSelectedGameObject.tag != "MainMenu") return;


        if (lastSelected == null && m_EventSystem.currentSelectedGameObject != null)
        {
            lastSelected = m_EventSystem.currentSelectedGameObject;
        }
        if (lastSelected != m_EventSystem.currentSelectedGameObject && m_EventSystem.currentSelectedGameObject != null)
        {

            if (lastSelected.tag == "MainMenu" && m_EventSystem.currentSelectedGameObject.tag == "MainMenu")
            {
                lastSelected.GetComponent<VideoPlayer>().enabled = false;
                m_EventSystem.currentSelectedGameObject.GetComponent<VideoPlayer>().enabled = true;
                lastSelected = m_EventSystem.currentSelectedGameObject;

            }

        }
    }




    public void VideoStarter(Transform other)
    {
        if (other.tag != "MainMenu") return;

        Debug.Log(other.name);
        other.gameObject.GetComponent<VideoPlayer>().enabled = true;
    
    }
    public void VideoEnder(Transform other)
    {

        if (other.tag != "MainMenu") return;

        Debug.Log("EXIT");
        other.gameObject.GetComponent<VideoPlayer>().enabled = false;

    }
}
