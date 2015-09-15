using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CombatSystem : MonoBehaviour
{
    public GameObject Text;
    public GameObject[] Buttons;

    public AudioClip[] punch;
    public AudioClip[] spell;
    public AudioClip[] grunt;

    [System.NonSerialized]
    public GameObject Enemy;

    private float textA = 0;

    private void Start()
    {
        ((RectTransform)GameObject.FindWithTag("PlayerHealth").transform).localPosition = new Vector3(PlayerStats.Health + 10, 0, 0);
        ((RectTransform)GameObject.FindWithTag("PlayerHealth").transform).sizeDelta = new Vector2(PlayerStats.Health * 2, 15);
        GameObject.FindWithTag("PlayerHealthText").GetComponent<Text>().text = PlayerStats.Health + "/100";
    }

    public void PlayAttackSound(PlayerStats.WeaponTypes type)
    {
        GameObject g = new GameObject();
        g.AddComponent<AudioSource>();
        if (type == PlayerStats.WeaponTypes.Melee)
        {
            g.GetComponent<AudioSource>().clip = punch[Random.Range(0, punch.Length)];
        }
        else
        {
            g.GetComponent<AudioSource>().clip = spell[Random.Range(0, spell.Length)];
        }
        
        g.GetComponent<AudioSource>().volume = PlayerStats.FXVolume;
        g.GetComponent<AudioSource>().Play();
        StartCoroutine(DestroyAudio(g));
    }

    public void PlayGrunt()
    {
        Debug.Log("Played Grunt");
        GameObject g = new GameObject();
        g.AddComponent<AudioSource>();
        g.GetComponent<AudioSource>().volume = PlayerStats.FXVolume;
        g.GetComponent<AudioSource>().clip = grunt[Random.Range(0, grunt.Length)];
        g.GetComponent<AudioSource>().Play();
        StartCoroutine(DestroyAudio(g));
    }

    public IEnumerator DestroyAudio(GameObject g)
    {
        yield return new WaitForSeconds(g.GetComponent<AudioSource>().clip.length);
        Destroy(g);
    }

    public void UseSpell()
    {
        GetComponent<Animator>().SetTrigger("Spell");
        int amount = Mathf.RoundToInt(PlayerStats.CalculateDamage(PlayerStats.WeaponTypes.Spell));
        Enemy.GetComponent<Enemy>().DoDamage(amount);
        Debug.Log("PLAYER (Spell)" + amount);
        DoneTurn();
    }

    public void UseMelee()
    {
        GetComponent<Animator>().SetTrigger("Attack");
        int amount = Mathf.RoundToInt(PlayerStats.CalculateDamage(PlayerStats.WeaponTypes.Melee));
        Enemy.GetComponent<Enemy>().DoDamage(amount);
        Debug.Log("PLAYER (Melee)" + amount);
        DoneTurn();
    }

    private void DoneTurn()
    {
        PlayGrunt();
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
        ((RectTransform)GameObject.FindWithTag("PlayerHealth").transform).localPosition = new Vector3(PlayerStats.Health + 10, 0, 0);
        ((RectTransform)GameObject.FindWithTag("PlayerHealth").transform).sizeDelta = new Vector2(PlayerStats.Health * 2, 15);
        Text.GetComponent<TextMesh>().text = "-" + damage;
        GameObject.FindWithTag("PlayerHealthText").GetComponent<Text>().text = PlayerStats.Health + "/100";
        if (PlayerStats.Health <= 0)
        {
            PlayerStats.Health = 100;
            if (PlayerStats.CurrentLevel != 0)
            { 
                while (PlayerStats.CurrentLevel % 10 != 0)
                    PlayerStats.CurrentLevel--;

                PlayerStats.CurrentLevel++;
            }

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