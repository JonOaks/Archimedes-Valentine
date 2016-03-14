﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	
	public Transform sightStart, sightEnd;

	Animator anim;

	public float jumpForce = 290f;
	public float maxSpeed = 5f;

	public bool facingRight = true;
	public bool grounded = false;
	public bool doubleJump = false;
	public bool edge;

	private PlayerAttack playerAttack;

	// Stats
	public int curHealth;
	public int maxHealth;

	// Knockback
	public float knockback;
	public float knockbackLength;
	public float knockbackCount;
	public bool knockFromRight;

	void Start () {
		anim = gameObject.GetComponent<Animator> ();
		playerAttack = gameObject.GetComponent<PlayerAttack> ();

		maxHealth = 5;
		curHealth = maxHealth;
	}

	void FixedUpdate () {
		if (grounded) {
			doubleJump = false;
		}

		anim.SetBool ("Ground", grounded);

		anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

		float move = Input.GetAxis ("Horizontal");

		if((grounded && playerAttack.attacking) || anim.GetBool ("Dead"))
		{
			move = 0;
		}

		if(anim.GetBool ("Dead")) {
			move = 0;
			doubleJump = true;
			grounded = false;
			playerAttack.attackTrigger.enabled = false;
		}

		anim.SetFloat ("Speed", Mathf.Abs(move));

		if (knockbackCount <= 0) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D> ().velocity.y);
		} else {
			if (knockFromRight) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (-knockback, knockback);
			}
			if (!knockFromRight) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (knockback, knockback);
			}
			knockbackCount -= Time.deltaTime;
		}

		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();

		// EDGE LOGIC
		Debug.DrawLine (sightStart.position, sightEnd.position, Color.green); // Debugging line reference

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

		// In case the couroutine from the enemy on trigger function stops
		if (!GetComponent<SpriteRenderer> ().enabled) {
			GetComponent<SpriteRenderer> ().enabled = true;
		}
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

		if (curHealth > maxHealth) {
			curHealth = maxHealth;
		}

		if (curHealth <= 0) {
			Die ();

		}
	}

	void Flip () {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void Die () {
		Physics2D.IgnoreLayerCollision(11, 13, true);
		anim.SetBool ("Dead", true);
	}
}