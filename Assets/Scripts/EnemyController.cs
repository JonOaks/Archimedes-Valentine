using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	
	public float curHealth = 24;
	public bool dead = false;
	private bool played = false;

	Animator anim;

	public GameObject explosionSoundObject;

	void Start () {
		anim = gameObject.GetComponent<Animator> ();
	}

	void Update() {
		if(curHealth <= 0) {
			dead = true;
			gameObject.GetComponent<Collider2D> ().enabled = false;
			anim.SetBool ("Dead", dead);
			Destroy(gameObject,0.4f);
			if(!played){
				played = true;
				Instantiate(explosionSoundObject);
			}
		}
	}

	public void Damage(int damage) {
		curHealth -= damage;
	}
}
