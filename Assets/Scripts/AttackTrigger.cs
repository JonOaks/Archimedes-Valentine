using UnityEngine;
using System.Collections;

public class AttackTrigger : MonoBehaviour {
	public int damage = 24;

	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag == "Enemy") {
			col.GetComponent<EnemyController> ().Damage(damage);
		} 
	}
}
