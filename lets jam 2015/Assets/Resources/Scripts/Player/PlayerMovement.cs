using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	//Variables
	public float maxSpeed = 10f;
	private bool facingRight = true;

    private void FixedUpdate()
    {
        //Basic horizontal movement
        float move = Input.GetAxis("Horizontal");
        //If PlayerStats.drunk equals zero, then it will be infinite.
        //Hence why we add 1, to prevent an error
        float xMov = (move * maxSpeed) / ((PlayerStats.Drunk + 25.0f) / 25.0f);
        xMov = Mathf.Clamp(xMov, -10, 10);
        GetComponent<Rigidbody2D>().velocity = new Vector2(xMov, GetComponent<Rigidbody2D>().velocity.y);

        if (GetComponent<Rigidbody2D>().velocity.x != 0)
            GetComponent<Animator>().SetInteger("Movment", 1);
        else
            GetComponent<Animator>().SetInteger("Movment", 0);

        //Flip the player image if either facing right or left
        if (move > 0 && !facingRight)
        {
            FlipPlayer();
        }
        else if (move < 0 && facingRight)
        {
            FlipPlayer();
        }
    }

	private void FlipPlayer()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
