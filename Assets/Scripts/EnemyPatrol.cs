using UnityEngine;
using System.Collections;

public class EnemyPatrol : MonoBehaviour {

	Animator anim;

	public float moveSpeed;
	public bool moveRight;

	public Transform wallCheck;
	public float wallCheckRadius;
	public LayerMask whatIsWall;
	private bool hittingWall;

	private bool notAtEdge;
	public Transform edgeCheck;

	private bool dead;

	void Start () {
		anim = gameObject.GetComponent<Animator> ();
	}
	
	void Update () {

		hittingWall = Physics2D.OverlapCircle (wallCheck.position, wallCheckRadius, whatIsWall);
		notAtEdge = Physics2D.OverlapCircle (edgeCheck.position, wallCheckRadius, whatIsWall);

		if (hittingWall || !notAtEdge) {
			moveRight = !moveRight;
		}
			
		anim.SetFloat ("Speed", Mathf.Abs(moveSpeed));
		dead = anim.GetBool ("Dead");
		if (dead) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, GetComponent<Rigidbody2D> ().velocity.y);
		} else {
			if (moveRight) {
				transform.localScale = new Vector3 (-1f, 1f, 1f);
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
			} else {
				transform.localScale = new Vector3 (1f, 1f, 1f);
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
			}
		}
	}
}
