using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    public enum WeaponTypes
    {
        Spell = 50,
        Melee = 30
    }

    public static float Drunk = 0;
    public static float Health = 100;

    public static float CalculateDamage(WeaponTypes type)
    {
        if (Random.Range(0, 100) < Drunk)
            return 0;

        switch(type)
        {
            case WeaponTypes.Melee:
                return ((int)WeaponTypes.Melee) * (Drunk / 38);
            case WeaponTypes.Spell:
                return ((int)WeaponTypes.Spell) / (Drunk / 50);
            default:
                return 0;
        }
    }
}