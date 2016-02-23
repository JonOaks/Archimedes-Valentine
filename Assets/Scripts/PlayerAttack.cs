using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
	public bool attacking = false;

	private float attackTimer = 0;
	private float attackCd = 0.25f;

	public Collider2D attackTrigger;

	Animator anim;

	void Awake () {
		anim = gameObject.GetComponent<Animator> ();
		attackTrigger.enabled = false;
	}

	void Update () {
		if (Input.GetKeyDown("f") && !attacking) {
			attacking = true;
			attackTimer = attackCd;

			attackTrigger.enabled = true;
		}

		if (attacking) {
			if (attackTimer > 0) {
				attackTimer -= Time.deltaTime;
			} else {
				attacking = false;
				attackTrigger.enabled = false;
			}
		}

		anim.SetBool("Attack", attacking);
	}
}
