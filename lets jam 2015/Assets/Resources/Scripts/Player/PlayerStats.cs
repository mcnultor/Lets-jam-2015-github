using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    public enum WeaponTypes
    {
        Spell = 50,
        Melee = 30
    }

    private static int drunk = 0;
    private static int health = 100;

    public static int Drunk
    {
        get
        {
            return drunk;
        }
        set
        {
            drunk = Mathf.Clamp(value, 0, 100);
        }
    }
    public static int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = Mathf.Clamp(value, 0, 100);
        }
    }

    public static int CurrentLevel = 0;
    public static int FXVolume = 0;
    public static int MusicVolume = 0;

    public static float CalculateDamage(WeaponTypes type)
    {
        if (Random.Range(0, 100) < Drunk)
            return 0;

        switch(type)
        {
            case WeaponTypes.Melee:
                return 30 + (Drunk / 2.0f);
            case WeaponTypes.Spell:
                return 50 - (Drunk / 4.0f);
            default:
                return 0;
        }
    }
}