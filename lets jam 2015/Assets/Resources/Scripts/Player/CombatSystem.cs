using UnityEngine;
using System.Collections;

public class CombatSystem : MonoBehaviour
{
    public GameObject Text;
    public GameObject[] Buttons;

    [System.NonSerialized]
    public GameObject Enemy;

    private float textA = 0;

    public void UseSpell()
    {
        int amount = Mathf.RoundToInt(PlayerStats.CalculateDamage(PlayerStats.WeaponTypes.Spell));
        Enemy.GetComponent<Enemy>().DoDamage(amount);
        Debug.Log("PLAYER (Spell)" + amount);
        DoneTurn();
    }

    public void UseMelee()
    {
        int amount = Mathf.RoundToInt(PlayerStats.CalculateDamage(PlayerStats.WeaponTypes.Melee));
        Enemy.GetComponent<Enemy>().DoDamage(amount);
        Debug.Log("PLAYER (Melee)" + amount);
        DoneTurn();
    }

    private void DoneTurn()
    {
        for (int i = 0; i < GetComponent<CombatSystem>().Buttons.Length; i++)
            Buttons[i].SetActive(false);

        if (Enemy)
            Enemy.GetComponent<Enemy>().DoTurn();
        else
            GetComponent<PlayerMovement>().enabled = true;
    }

    public void DoDamage(int damage)
    {
        textA = 1;
        PlayerStats.Health -= damage;
        Text.GetComponent<TextMesh>().text = "-" + damage;
        if (PlayerStats.Health <= 0)
        {
            Application.LoadLevel(2);
        }
    }

    private void Update()
    {
        if (textA >= 0)
        {
            textA -= Time.deltaTime;
            Text.GetComponent<TextMesh>().color = new Color(1.0f, 0.0f, 0.0f, textA);
        }
    }
}