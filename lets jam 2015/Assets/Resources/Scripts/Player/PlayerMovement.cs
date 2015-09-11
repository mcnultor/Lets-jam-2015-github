﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	//Variables
	public float maxSpeed = 10f;
	bool facingRight = true;

	void Start()
	{
	}

	void FixedUpdate()
	{
		//Basic horizontal movement
		float move = Input.GetAxis ("Horizontal");
		GetComponent<Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		if (move > 0 && !facingRight)
			FlipPlayer ();
		else if (move < 0 && facingRight)
			FlipPlayer ();
	}

	void FlipPlayer()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
