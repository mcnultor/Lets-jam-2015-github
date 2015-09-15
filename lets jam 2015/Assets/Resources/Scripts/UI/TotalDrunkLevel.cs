using UnityEngine;
using UnityEngine.UI;

public class TotalDrunkLevel : MonoBehaviour
{
    public static int TotalDrunk = 0;

    private void Start()
    {
        GetComponent<Text>().text = TotalDrunk.ToString();
    }
}