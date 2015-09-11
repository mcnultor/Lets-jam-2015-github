using UnityEngine;
using System.Collections;

public class PlayerTrigger : MonoBehaviour
{
    public GameObject InteractObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            InteractObject.SendMessage("Interact", other.gameObject);
        }
    }
}