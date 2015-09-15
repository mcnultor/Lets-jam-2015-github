using UnityEngine;
using System.Collections;

public class TextNextButton : MonoBehaviour
{
    [System.NonSerialized]
    public GameObject textMaker;

    public void ButtonPressed()
    {
        textMaker.GetComponent<ShowText>().NextText();
    }
}