using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFuncs : MonoBehaviour {

    [Header("Audio Source Component")]
    public AudioSource aSource;
    
    [Header("Click Sound")]
    public AudioClip aClick;

    [Header("Hover Sound")]
    public AudioClip aHover;


    public void Onhover()
    {
        aSource.PlayOneShot(aHover);
    }

    public void Onclick()
    {
        aSource.PlayOneShot(aClick);
    }



}
