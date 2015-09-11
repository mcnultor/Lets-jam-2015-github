using UnityEngine;
using System.Collections;

public class DrunkBar : MonoBehaviour
{
    private void Update()
    {
        ((RectTransform)transform).position = new Vector3(20, PlayerStats.Drunk + 10, 0);
        ((RectTransform)transform).sizeDelta = new Vector2(15, PlayerStats.Drunk * 2);
    }
}