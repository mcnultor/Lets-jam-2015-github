using UnityEngine;
using System.Collections;

public class Connect : MonoBehaviour
{
    private void Start()
    {
        Network.Connect("213.107.103.140", 25565);
    }

    private void OnConnectedToServer()
    {
        Destroy(gameObject);
        Application.LoadLevelAdditive("Default");
    }

    private void OnServerInitialized()
    {
        Destroy(gameObject);
        Application.LoadLevelAdditive("default");
    }

    private void OnFailedToConnect()
    {
        Network.InitializeSecurity();
        Network.InitializeServer(32, 25565, false);
    }
}