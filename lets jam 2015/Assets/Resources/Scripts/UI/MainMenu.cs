using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public GameObject Resolution;
    public GameObject Quality;
    public GameObject Music;
    public GameObject FX;

    private int currentResolution = 0;

    private void Start()
    {
        Music.GetComponent<Slider>().value = (float)PlayerStats.MusicVolume / 100.0f;
        FX.GetComponent<Slider>().value = (float)PlayerStats.FXVolume / 100.0f;
        currentResolution = Screen.resolutions.Length - 1;
        Resolution.GetComponent<Text>().text = Screen.resolutions[currentResolution].width + "X" + Screen.resolutions[currentResolution].height;
        Screen.SetResolution(Screen.resolutions[currentResolution].width, Screen.resolutions[currentResolution].height, true);
    }

    public void Play()
    {
        Application.LoadLevel("Intro");
    }

    public void ChangeResolution(int x)
    {
        if (currentResolution + x < Screen.resolutions.Length && currentResolution > 0)
        {
            currentResolution += x;
            Resolution.GetComponent<Text>().text = Screen.resolutions[currentResolution].width + "X" + Screen.resolutions[currentResolution].height;
        }
    }

    public void ChangeQuality(int x)
    {
        Quality.GetComponent<Text>().text = QualitySettings.GetQualityLevel().ToString();
        if (x > 0)
            QualitySettings.IncreaseLevel();
        else if (x < 0)
            QualitySettings.DecreaseLevel();
    }

    public void FXVolume(float x)
    {
        PlayerStats.FXVolume = Mathf.RoundToInt(x * 100);
    }

    public void MusicVolume(float x)
    {
        PlayerStats.MusicVolume = Mathf.RoundToInt(x * 100);
    }

    public void Apply()
    {
        Screen.SetResolution(Screen.resolutions[currentResolution].width, Screen.resolutions[currentResolution].height, true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}