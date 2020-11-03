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
        Slider.value = PlayerPrefs.GetFloat("bgSound", 0);
        mixer.SetFloat("bgSound", Mathf.Log10(Slider.value) * 20);
        toggle.GetComponent<ToggleUI>().isOn = IntToBool(PlayerPrefs.GetInt("SFX", 0));
        sfxMixer.SetFloat("SFX", BoolToInt(toggle.GetComponent<ToggleUI>().isOn));
    }

    private float BoolToInt(bool isOn)
    {
        if (isOn)
            return 0f;
        else
            return -80f;
    }

    private bool IntToBool(int v)
    {
        if (v != 0)
            return true;
        else
            return false;
    }

    public void ChangeVolume(float sliderValue)
    {
        mixer.SetFloat("bgSound", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("bgSound", sliderValue);

        Debug.Log("slider value: " + PlayerPrefs.GetFloat("bgSound", 0));
    }

    public void SfxVolume()
    {
        int saveSfx;
        toggleOn = toggle.GetComponent<ToggleUI>().isOn;
        if (toggleOn)
        {
            muteSfx = -80;
            saveSfx = 0;
        }
        else
        {
            muteSfx = 0;
            saveSfx = 1;
        }

        sfxMixer.SetFloat("SFX", muteSfx);
        PlayerPrefs.SetInt("SFX", saveSfx);
        Debug.Log("sfx: "+muteSfx);
    }
}
