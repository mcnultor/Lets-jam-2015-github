using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Music").Length > 1)
            Destroy(gameObject);
    }

    private void Update()
    {
        GetComponent<AudioSource>().volume = (float)PlayerStats.MusicVolume / 100.0f;
    }
}