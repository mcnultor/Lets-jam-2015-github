using UnityEngine;

public class BackgroundChoser : MonoBehaviour
{
    public Sprite[] backgrounds;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = backgrounds[Random.Range(0, backgrounds.Length)];
    }
}