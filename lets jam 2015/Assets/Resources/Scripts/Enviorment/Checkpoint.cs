using UnityEngine;
using System.IO;

public class Checkpoints : MonoBehaviour
{
    private void Interact(GameObject player)
    {
        PlayerStats.Drunk -= 20;

        TextWriter write = new StreamWriter("save.dat");
        write.WriteLine(PlayerStats.Drunk);
        write.WriteLine(PlayerStats.Health);
        write.WriteLine(PlayerStats.CurrentLevel);
        write.Close();
    }
}