using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationHelper : MonoBehaviour {

    public AudioClip aClipRotate;
    public AudioSource aSourceRotate;

    public bool isHorizontal = true;

    public AudioSource SetAudioSource(AudioClip clip, bool loop, bool playAwake, float vol)
    {
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.clip = clip;
        newSource.loop = loop;
        newSource.playOnAwake = playAwake;
        newSource.volume = vol;

        return newSource;

    }

    public void Start()
    {

        aSourceRotate = SetAudioSource(aClipRotate, false, false, .5f);
       

    }

}
