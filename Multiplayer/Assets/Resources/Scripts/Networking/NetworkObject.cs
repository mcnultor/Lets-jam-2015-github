using UnityEngine;
using System.Collections;

public class NetworkObject : MonoBehaviour
{
    [System.NonSerialized]
    public int NetworkID;
    [System.NonSerialized]
    public string Owner;
    [System.NonSerialized]
    public Component[] components;

    public void UnloadComponents(object[] components)
    {
        if (this.components.Length != this.components.Length)
        { 
            Debug.Log(NetworkID + " Componenets length does not match");
            this.components = new Component[components.Length];
            for (int i = 0; i < components.Length; i++)
                gameObject.AddComponent<Component>();
        }

        for (int i = 0; i < components.Length; i++)
        {
            this.components[i] = (Component)components[i];
        }
    }

    public void RemoveObject()
    {
        Destroy(gameObject);
    }
}