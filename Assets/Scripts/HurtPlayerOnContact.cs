using UnityEngine;
using System.Collections;

public class HurtPlayerOnContact : MonoBehaviour {

	public int damageToGive;
	
	void OnTriggerEnter2D (Collider2D col) {
		if (col.name == "Player") {
			var player = col.GetComponent<PlayerController> ();
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

	IEnumerator Wait(Collider2D sr, float time) {

		Physics2D.IgnoreCollision(sr, GetComponent<Collider2D> (), true);

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

		Physics2D.IgnoreCollision(sr, GetComponent<Collider2D> (), false);
	}
}
