using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShowText : MonoBehaviour
{
    public string Text = "";

    private static List<string> queue = new List<string>();
    private GameObject text;
    private float a = 0;

    private void Start()
    {
        queue.Clear();
        text = GameObject.FindWithTag("Canvas").transform.FindChild("Panel").gameObject;
    }

    public void Interact(GameObject player)
    {
        queue.Add(Text);
        text.transform.FindChild("Text").gameObject.GetComponent<Text>().text = queue[0];
    }

    private void LateUpdate()
    {
        if (queue.Count > 0)
        {
            text.SetActive(true);
            if (a < 1)
                a += Time.deltaTime;
        }
        else
        {
            if (a <= 0)
                text.SetActive(false);
            else
                a -= Time.deltaTime;
        }

        a = Mathf.Clamp(a, 0.0f, 1.0f);

        text.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, a);
        text.transform.FindChild("Text").GetComponent<Text>().color = new Color(0.5f, 0.5f, 0.5f, a);
        text.transform.FindChild("Button").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, a);
    }

    public void NextText()
    {
        if (queue.Count == 1)
            queue.Clear();
        else
        {
            queue.RemoveAt(0);
            text.transform.FindChild("Text").gameObject.GetComponent<Text>().text = queue[0];
        }
    }
}