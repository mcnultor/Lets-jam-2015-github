using UnityEngine;
using System.Collections;

public class EnemeySpawner : MonoBehaviour
{
    public GameObject[] target;

    public GameObject Level10Boss;
    public GameObject Level20Boss;
    public GameObject Level30Boss;
    public GameObject Level40Boss;
    public GameObject Level50Boss;

    private void Start()
    {
        if (PlayerStats.CurrentLevel == 10)
            Instantiate(Level10Boss, transform.position, Quaternion.identity);
        else if (PlayerStats.CurrentLevel == 20)
            Instantiate(Level20Boss, transform.position, Quaternion.identity);
        else if (PlayerStats.CurrentLevel == 30)
            Instantiate(Level30Boss, transform.position, Quaternion.identity);
        else if (PlayerStats.CurrentLevel == 40)
            Instantiate(Level40Boss, transform.position, Quaternion.identity);
        else if (PlayerStats.CurrentLevel == 50)
            Instantiate(Level50Boss, transform.position, Quaternion.identity);
        else if (Random.Range(0, 2) == 0)
            Instantiate(target[Random.Range(0, target.Length)], transform.position, Quaternion.identity);
    }
}