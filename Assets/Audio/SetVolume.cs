using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetLevel(float sliderValue)
    {
        //log 10 because the value is in decibels
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }
}
