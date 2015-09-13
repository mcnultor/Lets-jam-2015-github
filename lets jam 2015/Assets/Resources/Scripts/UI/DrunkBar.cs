using UnityEngine;
using System.Collections;

public class DrunkBar : MonoBehaviour
{
    public bool Mask = false;

    private void Update()
    {
        if (!Mask)
            ((RectTransform)transform).localPosition = new Vector3(4.4f, 3 + 55 - (((float)PlayerStats.Drunk / 100.0f) * 55.0f), 0);
        else
            ((RectTransform)transform).localPosition = new Vector3(-4.8f, -55 + (((float)PlayerStats.Drunk / 100.0f) * 55.0f) - 4.6f, 0);
    }
}