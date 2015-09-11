using UnityEngine;
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
		//If PlayerStats.drunk equals zero, then it will be infinite.
		//Hence why we add 1, to prevent an error
		GetComponent<Rigidbody2D>().velocity = new Vector2 ((move * maxSpeed)/(PlayerStats.Drunk +1), GetComponent<Rigidbody2D>().velocity.y);

		//Flip the player image if either facing right or left
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
