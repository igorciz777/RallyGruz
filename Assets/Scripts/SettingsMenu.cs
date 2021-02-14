using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Dropdown resDropdown;
    public Dropdown qualityDropdown;
    public Slider volSlider;
    private float currentVolume;
    private Resolution[] resolutions;

    private void Start()
    {
        resDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " +
                     resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width
                  && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }

        resDropdown.AddOptions(options);
        resDropdown.RefreshShownValue();
        LoadSettings(currentResolutionIndex);
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
        audioMixer.GetFloat("MasterVolume", out currentVolume);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void SetResolution(int resIndex)
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetQuality(int qualityIndex)
    {
        if (qualityIndex != 6) QualitySettings.SetQualityLevel(qualityIndex);
        qualityDropdown.value = qualityIndex;
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game");
    }
    public void SaveSettings()
    {
        PlayerPrefs.SetInt("QualitySettingPreference",
                qualityDropdown.value);
        PlayerPrefs.SetInt("ResolutionPreference",
                resDropdown.value);
        PlayerPrefs.SetInt("FullscreenPreference",
                System.Convert.ToInt32(Screen.fullScreen));
        PlayerPrefs.SetFloat("VolumePreference",
                currentVolume);
    }
    public void LoadSettings(int currentResolutionIndex)
    {
        if (PlayerPrefs.HasKey("QualitySettingPreference"))
            qualityDropdown.value =
                         PlayerPrefs.GetInt("QualitySettingPreference");
        else
            qualityDropdown.value = 3;
        if (PlayerPrefs.HasKey("ResolutionPreference"))
            resDropdown.value =
                         PlayerPrefs.GetInt("ResolutionPreference");
        else
            resDropdown.value = currentResolutionIndex;
        if (PlayerPrefs.HasKey("FullscreenPreference"))
            Screen.fullScreen =
            System.Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
        else
            Screen.fullScreen = true;
        if (PlayerPrefs.HasKey("VolumePreference"))
            volSlider.value =
                        PlayerPrefs.GetFloat("VolumePreference");
        else
            volSlider.value =
                        PlayerPrefs.GetFloat("VolumePreference");
    }
}
