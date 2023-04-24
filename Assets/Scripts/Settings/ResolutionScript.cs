using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionScript : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private int currentResolutionIndex = 0;

    void Start()
    {
        resolutions = UnityEngine.Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        for(int i=0; i < resolutions.Length; i++)
        {
            string resolitionOption = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(resolitionOption);
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
    }


    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }

    /*public void SaveSettings(int resolutionIndex)
    {
        PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
    }

    public void LoadSettings(int currentResolutionIndex)
    {
        if (PlayerPrefs.HasKey("ResolutionPreference"))
            resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference");
        else
            resolutionDropdown.value = currentResolutionIndex;
    }*/


}
