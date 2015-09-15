using UnityEngine;
using System.Collections;

public class EnemeySpawner : MonoBehaviour
{
    public GameObject NextTextButton;

    public GameObject[] target;

    public GameObject Level10Boss;
    public GameObject Level20Boss;
    public GameObject Level30Boss;
    public GameObject Level40Boss;

    private void Start()
    {
        GameObject spwn = null;
        if (PlayerStats.CurrentLevel == 10)
            spwn = (GameObject)Instantiate(Level10Boss, transform.position, Quaternion.identity);
        else if (PlayerStats.CurrentLevel == 20)
            spwn = (GameObject)Instantiate(Level20Boss, transform.position, Quaternion.identity);
        else if (PlayerStats.CurrentLevel == 30)
            spwn = (GameObject)Instantiate(Level30Boss, transform.position, Quaternion.identity);
        else if (PlayerStats.CurrentLevel == 40)
            spwn = (GameObject)Instantiate(Level40Boss, transform.position, Quaternion.identity);
        else if (Random.Range(0, 2) == 0)
            Instantiate(target[Random.Range(0, target.Length)], transform.position, Quaternion.identity);
        
        if (spwn != null)
            NextTextButton.GetComponent<TextNextButton>().textMaker = spwn;
    }
}