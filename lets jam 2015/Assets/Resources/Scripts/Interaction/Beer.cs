﻿using UnityEngine;
using System.Collections;

public class Beer : MonoBehaviour 
{
	private int IncreaseAmount = 2;

	public void Interact(GameObject player)
	{
		PlayerStats.Drunk += IncreaseAmount;
	}
}