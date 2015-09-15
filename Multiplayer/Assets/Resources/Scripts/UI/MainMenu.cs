using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject ResolutionText, MusicSlider, FXSlider;

    public void Start()
    {
        Settings.Display.Resolution = Screen.resolutions.Length - 1;
        ResolutionText.GetComponent<Text>().text = Screen.resolutions[Settings.Display.Resolution].width + "X" + Screen.resolutions[Settings.Display.Resolution].height;
        Screen.SetResolution(Screen.resolutions[Settings.Display.Resolution].width, Screen.resolutions[Settings.Display.Resolution].height, true);
        MusicSlider.GetComponent<Slider>().value = Settings.Volume.Music;
        FXSlider.GetComponent<Slider>().value = Settings.Volume.FX;
    }

    public void FXVolumeChanged(float volume)
    {
        Settings.Volume.FX = FXSlider.GetComponent<Slider>().value;
    }

    public void MusicVolumeChanged(float volume)
    {
        Settings.Volume.Music = MusicSlider.GetComponent<Slider>().value;
    }

    public void ChangeResolution(int change)
    {
        if (Settings.Display.Resolution + change < Screen.resolutions.Length && Settings.Display.Resolution + change > 0)
        { 
            Settings.Display.Resolution += change;
            ResolutionText.GetComponent<Text>().text = Screen.resolutions[Settings.Display.Resolution].width + "X" + Screen.resolutions[Settings.Display.Resolution].height;
        }
    }

    public void Connect()
    {
        NetworkManager.Connect("127.0.0.1", 25565);
    }

    public void ApplyResolution()
    {
        Screen.SetResolution(Screen.resolutions[Settings.Display.Resolution].width, Screen.resolutions[Settings.Display.Resolution].height, true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}