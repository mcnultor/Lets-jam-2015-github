using UnityEngine;
using System.Collections;

public class Beer : MonoBehaviour 
{
	public int IncreaseAmount = 2;

	public void Interact(GameObject player)
	{
		PlayerStats.Drunk += IncreaseAmount;
	}
}