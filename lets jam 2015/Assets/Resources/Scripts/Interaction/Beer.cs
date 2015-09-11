using UnityEngine;
using System.Collections;

public class Beer : MonoBehaviour 
{
	public int IncreaseAmount = 20;

	public void Interact(GameObject player)
	{
		PlayerStats.Drunk += IncreaseAmount;
	}
}