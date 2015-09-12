using UnityEngine;
using System.Collections;

public class Stout : MonoBehaviour {

	private int IncreaseAmount = 4;
	
	public void Interact(GameObject player)
	{
		PlayerStats.Drunk += IncreaseAmount;
	}
}
