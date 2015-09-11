using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public GameObject Resolution;
    public GameObject Quality;

    private int currentResolution = 0;
    private int music = 0;
    private int fx = 0;

    private void Start()
    {
        if (File.Exists("Settings.sav"))
        {
            int qualityLevel = 0;
            TextReader read = new StreamReader("Settings.sav");
            if (!int.TryParse(read.ReadLine(), out PlayerStats.FXVolume))
                Debug.Log("Couldn't Load FX Volume");
            if (!int.TryParse(read.ReadLine(), out PlayerStats.MusicVolume))
                Debug.Log("Couldn't Load Music Volume");

            if (!int.TryParse(read.ReadLine(), out qualityLevel))
                Debug.Log("Couldn't Load Quality Settings");
            else
                QualitySettings.SetQualityLevel(qualityLevel);

            if (!int.TryParse(read.ReadLine(), out currentResolution))
                Debug.Log("Couldn't Load Resolution Settings");

            read.Close();
            Screen.SetResolution(Screen.resolutions[currentResolution].width, Screen.resolutions[currentResolution].height, true);
        }
        else
            Screen.SetResolution(Screen.resolutions[Screen.resolutions.Length - 1].width, Screen.resolutions[Screen.resolutions.Length - 1].height, true);

        Quality.GetComponent<Text>().text = QualitySettings.GetQualityLevel().ToString();
    }

    public void Play()
    {
        Application.LoadLevel(1);
    }

    public void ChangeResolution(int x)
    {
        if (currentResolution + x < Screen.resolutions.Length && currentResolution > 0)
        {
            Resolution.GetComponent<Text>().text = Screen.resolutions[currentResolution].width + "X" + Screen.resolutions[currentResolution].height;
            currentResolution += x;
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
        fx = Mathf.RoundToInt(x * 100);
    }

    public void MusicVolume(float x)
    {
        music = Mathf.RoundToInt(x * 100);
    }

    public void Apply()
    {
        Screen.SetResolution(Screen.resolutions[currentResolution].width, Screen.resolutions[currentResolution].height, true);
        PlayerStats.FXVolume = fx;
        PlayerStats.MusicVolume = music;

        TextWriter write = new StreamWriter("Settings.sav");
        write.WriteLine(fx);
        write.WriteLine(music);
        write.WriteLine(QualitySettings.GetQualityLevel());
        write.WriteLine(currentResolution);
        write.Close();
    }

    public void Quit()
    {
        Application.Quit();
    }
}