using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float maxSpeed = 5f;
	public bool facingRight = true;

	Animator anim;

	public bool grounded = false;
	public float jumpForce = 290f;
	public bool doubleJump = false;


	public bool edge;
	public Transform sightStart, sightEnd;

	void Start () {
		anim = gameObject.GetComponent<Animator> ();
	}

	void FixedUpdate () {
		if (grounded)
			doubleJump = false;
		anim.SetBool ("Ground", grounded);

		anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

		float move = Input.GetAxis ("Horizontal");

		anim.SetFloat ("Speed", Mathf.Abs(move));

		GetComponent<Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();

		Debug.DrawLine (sightStart.position, sightEnd.position, Color.green);

		// Checking if the player is close to the right edge
		if (Physics2D.Linecast (sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer ("Right Edge")) 
			&& Mathf.Abs(move) == 0.0 && grounded && facingRight) {
			edge = true;
		// Checking if the player is close to the left edge
		} else if (Physics2D.Linecast (sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer ("Left Edge")) 
			&& Mathf.Abs(move) == 0.0 && grounded && !facingRight) {
			edge = true;
		} else {
			edge = false;
		}

		anim.SetBool ("Edge", edge);
	}

	void Update () {
		if ((grounded || !doubleJump) && Input.GetKeyDown (KeyCode.Space)) {
			anim.SetBool ("Ground", false);
			GetComponent<Rigidbody2D> ().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
			GetComponent<Rigidbody2D>().AddForce (new Vector2(0, jumpForce));

			if (!doubleJump && !grounded) {
				anim.SetBool ("Ground", false);
				GetComponent<Rigidbody2D> ().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
				GetComponent<Rigidbody2D>().AddForce (new Vector2(0, 140f));
				doubleJump = true;
			}
		}
	}

	void Flip () {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
