using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;


public class Settings : MonoBehaviour
{
    //resolutionControl
    Resolution[] resolutions;
    public Dropdown resolutionDropdown;


    public void GetResolution()
    {
        //resolutions
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions(); //clear out the default options that are on the dropdown

        List<string> options = new List<string>(); //list of strings : future options

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++) //add some options to the list
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height) //cannot compare 2 resolutions directly
            {
                currentResolutionIndex = i; //used to know the current resolution of the screen
            }
        }

        resolutionDropdown.AddOptions(options); //add the resolutions list to the dropdown
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue(); //display the current resolution
    }

    //options menu functions
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("LastQuality", qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}