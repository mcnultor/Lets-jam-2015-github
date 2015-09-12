using UnityEngine;
using System.Collections;

public class DwarvenAle : MonoBehaviour {

	private int IncreaseAmount = 3;
	
	public void Interact(GameObject player)
	{
		PlayerStats.Drunk += IncreaseAmount;
	}
}
