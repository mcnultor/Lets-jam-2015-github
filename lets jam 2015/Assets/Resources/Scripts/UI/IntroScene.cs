using UnityEngine;
using System.Collections;

public class IntroScene : MonoBehaviour
{
    public void Continue()
    {
        Application.LoadLevel("NetworkConnector");
    }
}