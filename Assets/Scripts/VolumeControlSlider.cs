using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeControlSlider : MonoBehaviour
{
    public enum VolumeParameter
    {
        MasterVolume,
        MusicVolume,
        SFXVolume,
    }
    [SerializeField] AudioMixer mixer;
    [SerializeField] VolumeParameter volumeParameter;
    [SerializeField] float multiplier = 30f;

    public void OnSliderValueChanged(float value)
    {
        mixer.SetFloat(volumeParameter.ToString(), Mathf.Log10(value) * multiplier + 10);
    }    
}
