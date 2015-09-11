using UnityEngine;
using System.Collections;

public class TextNextButton : MonoBehaviour
{
    public void ButtonPressed()
    {
        GameObject.FindWithTag("Text Maker").GetComponent<ShowText>().NextText();
    }
}