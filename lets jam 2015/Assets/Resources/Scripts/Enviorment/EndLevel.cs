using UnityEngine;
using System.IO;
using System.Collections;

public class EndLevel : MonoBehaviour
{
    private void Interact(GameObject player)
    {
        Application.LoadLevel(1);
    }

    public void LoadGame()
    {
        PlayerStats.Health = 100;
        Application.LoadLevel(1);
    }
}