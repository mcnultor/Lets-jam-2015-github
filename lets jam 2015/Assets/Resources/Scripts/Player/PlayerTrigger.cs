using UnityEngine;
using System.Collections;

public class PlayerTrigger : MonoBehaviour
{
    public GameObject InteractObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (InteractObject != null)
                InteractObject.SendMessage("Interact", other.gameObject);
            else
                Destroy(gameObject);

            other.gameObject.GetComponent<Animator>().SetInteger("Movment", 0);
        }
    }
}