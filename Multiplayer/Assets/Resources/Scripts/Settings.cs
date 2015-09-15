using UnityEngine;
using System.Collections;

public enum WeaponType
{
    Melee,
    Spell
}

public class Settings : MonoBehaviour
{
    public class Display
    {
        private static int resolution = 0;

        public static int Resolution
        {
            get
            {
                return resolution;
            }
            set
            {
                resolution = Mathf.Clamp(value, 0, Screen.resolutions.Length);
            }
        }
    }

    public class Volume
    {
        private static float music = 0.3f;
        private static float fx = 1.0f;

        public static float Music
        {
            get
            {
                return music;
            }
            set
            {
                music = Mathf.Clamp(value, 0.0f, 1.0f);
            }
        }
        public static float FX
        {
            get
            {
                return fx;
            }
            set
            {
                fx = Mathf.Clamp(value, 0.0f, 1.0f);
            }
        }
    }

    public class Player
    {
        private static int health = 100;
        private static int drunk = 0;

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

        public static float CalculateDamage(WeaponType type)
        {
            if (Random.Range(0, 50) < Drunk)
                return -1;
            
            if (type == WeaponType.Melee)
                return 30.0f + ((float)Drunk / 2.0f);
            else if (type == WeaponType.Spell)
                return 50.0f - ((float)Drunk / 4.0f);
            else
                return -2;
        }
    }
}