using UnityEngine;
using System.Collections;

public class CombatSystem : MonoBehaviour
{
    public GameObject[] Buttons;
    public GameObject Enemy;

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
        PlayerStats.Health -= damage;
        if (PlayerStats.Health <= 0)
        {
            Application.LoadLevel(2);
        }
    }
}