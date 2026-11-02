using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class SettingsManager : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public TMP_Dropdown resolutionDropdown;

    private List<Resolution> resolutions = new List<Resolution>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BuildResolutionDropdown();
        volumeSlider.onValueChanged.AddListener(SetVolume);
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        LoadSetting();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadSetting()
    {
        float volume = PlayerPrefs.GetFloat("Volume");
        SetVolume(volume);
        int resolutionIndex = PlayerPrefs.GetInt("resolution");
        SetResolution(resolutionIndex);
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat("Volume", value);
        volumeSlider.value = value;
        PlayerPrefs.SetFloat("Volume", value);
    }

    void BuildResolutionDropdown()
    {
        resolutions.Clear();
        List<string> options = new List<string>();
        foreach (Resolution res in Screen.resolutions)
        {
            if (resolutions.Exists(r => r.width == res.width && r.height == res.height)) continue;
            resolutions.Add(res);
            options.Add(res.width + " x " + res.height);
        }
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
    }

    public void SetResolution(int index)
    {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        resolutionDropdown.value = index;
        Screen.fullScreen = true;
        PlayerPrefs.SetInt("resolution", index);
    }
}
