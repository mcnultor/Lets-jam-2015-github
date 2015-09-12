using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShowText : MonoBehaviour
{
    public DrunkText[] Text;

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
        for (int i = 0; i < Text.Length; i++)
        {
            if (PlayerStats.Drunk <= 25)
                queue.Add(Text[i].drunk25);
            else if (PlayerStats.Drunk <= 50)
                queue.Add(Text[i].drunk50);
            else if (PlayerStats.Drunk <= 75)
                queue.Add(Text[i].drunk75);
            else if (PlayerStats.Drunk <= 100)
                queue.Add(Text[i].drunk100);
        }
        text.transform.FindChild("Text").gameObject.GetComponent<Text>().text = queue[0];
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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
        if (queue.Count > 0)
        {
            if (queue.Count == 1)
            {
                queue.Clear();
                GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
            }
            else
            {
                queue.RemoveAt(0);
                text.transform.FindChild("Text").gameObject.GetComponent<Text>().text = queue[0];
            }
        }
    }
}

[System.Serializable]
public class DrunkText
{
    public string drunk25 = "";
    public string drunk50 = "";
    public string drunk75 = "";
    public string drunk100 = "";
}