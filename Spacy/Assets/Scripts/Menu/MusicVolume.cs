using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioMixer sfxMixer;
    public Slider Slider;
    public GameObject toggle;

    private float muteSfx;
    private bool toggleOn;

    //private AudioSource audioVolume;

    private void Awake()
    {

    }

    private void Start()
    {
        Slider.value = PlayerPrefs.GetFloat("bgSound", 0.75f);
    }
    public void changeVolume(float sliderValue)
    {
        mixer.SetFloat("bgSound", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("bgSound", sliderValue);
    }

    public void SfxVolume()
    {
        toggleOn = toggle.GetComponent<ToggleUI>().isOn;
        if (toggleOn)
        {
            muteSfx = -80;
        }
        else
        {
            muteSfx = 0;
        }

        sfxMixer.SetFloat("SFX", muteSfx);
    }
}
