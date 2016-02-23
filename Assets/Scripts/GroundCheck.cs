using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {

	private PlayerController player;

	void Start () {
		player = gameObject.GetComponentInParent<PlayerController> ();
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.GetComponent<Collider2D>().gameObject.layer != LayerMask.NameToLayer ("Enemy")) {
			player.grounded = true;
		}
	}

	void OnTriggerStay2D (Collider2D col) {
		if (col.GetComponent<Collider2D>().gameObject.layer != LayerMask.NameToLayer ("Enemy")) {
			player.grounded = true;
		}
	}

	void OnTriggerExit2D (Collider2D col) {
		player.grounded = false;
	}

}
