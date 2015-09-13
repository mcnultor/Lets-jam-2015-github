using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public GameObject Text;
    public int Health = 100;
    public int MaxDamage = 20;
    public int MinDamage = 10;

    private float textA = 0;
    private GameObject player;
    private GameObject EnemeyHealth;

    private void Start()
    {
        EnemeyHealth = GameObject.FindWithTag("EnemyHealth");
        EnemeyHealth.SetActive(false);
    }

    private void Interact(GameObject player)
    {
        EnemeyHealth.SetActive(true);
        this.player = player;
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<CombatSystem>().Enemy = gameObject;
        for (int i = 0; i < player.GetComponent<CombatSystem>().Buttons.Length; i++)
            player.GetComponent<CombatSystem>().Buttons[i].SetActive(true);
    }

    public void DoDamage(int damage)
    {
        textA = 1.0f;
        Text.GetComponent<TextMesh>().text = "-"  + damage;
        Health -= damage;
        ((RectTransform)EnemeyHealth.transform).localPosition = new Vector3(-Health, 0, 0);
        ((RectTransform)EnemeyHealth.transform).sizeDelta = new Vector2(Health * 2, 15);
        if (Health <= 0)
        {
            player.GetComponent<CombatSystem>().Enemy = null;
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        if (textA >= 0)
        { 
            textA -= Time.deltaTime;
            Text.GetComponent<TextMesh>().color = new Color(1.0f, 0.0f, 0.0f, textA);
        }
    }

    public void DoTurn()
    {
        int d = Random.Range(MinDamage, MaxDamage);
        player.GetComponent<CombatSystem>().DoDamage(d);
        Debug.Log("ENEMY: " + d);
        for (int i = 0; i < player.GetComponent<CombatSystem>().Buttons.Length; i++)
            player.GetComponent<CombatSystem>().Buttons[i].SetActive(true);
    }
}