using UnityEngine;
using UnityEngine.UI;

public class LevelCounter : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Text>().text = "Level: " + PlayerStats.CurrentLevel;
    }
}