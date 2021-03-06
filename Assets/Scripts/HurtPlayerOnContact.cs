﻿using UnityEngine;
using System.Collections;

public class HurtPlayerOnContact : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D col) {
		if (col.name == "Player") {
			var player = col.GetComponent<PlayerController> ();
			if(player.curHealth == 1) {
				player.curHealth -= 1;
			} else if (player.curHealth == 0) {
				Physics2D.IgnoreLayerCollision(11, 13, true);
				Physics2D.IgnoreLayerCollision(13, 11, true);
			} else {
				player.knockbackCount = player.knockbackLength;
				player.curHealth -= 1;
				StartCoroutine (Wait(col, 0.1f));

				if (col.transform.position.x < transform.position.x) {
					player.knockFromRight = true;
				} else {
					player.knockFromRight = false;
				}
			}
		}
	}

	IEnumerator Wait(Collider2D sr, float time) {

		Physics2D.IgnoreLayerCollision(11, 13, true);

		sr.GetComponent<SpriteRenderer> ().enabled = false;
		yield return new WaitForSeconds (time);
		sr.GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (time);
		sr.GetComponent<SpriteRenderer> ().enabled = false;
		yield return new WaitForSeconds (time);
		sr.GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (time);
		sr.GetComponent<SpriteRenderer> ().enabled = false;
		yield return new WaitForSeconds (time);
		sr.GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (time);
		sr.GetComponent<SpriteRenderer> ().enabled = false;
		yield return new WaitForSeconds (time);
		sr.GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (time);
		sr.GetComponent<SpriteRenderer> ().enabled = false;
		yield return new WaitForSeconds (time);
		sr.GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (time);
		sr.GetComponent<SpriteRenderer> ().enabled = false;
		yield return new WaitForSeconds (time);
		sr.GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (time);
		sr.GetComponent<SpriteRenderer> ().enabled = false;
		yield return new WaitForSeconds (time);
		sr.GetComponent<SpriteRenderer> ().enabled = true;

		Physics2D.IgnoreLayerCollision(11, 13, false);
	}
}
