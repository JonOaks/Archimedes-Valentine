using UnityEngine;
using System.Collections;

public class GetCoinOnContact : MonoBehaviour {

	public GameObject coinSoundObject;
	private GameObject instantiatedObject;

	void OnTriggerEnter2D (Collider2D col) {
		if (col.name == "Player") {
			instantiatedObject = Instantiate (coinSoundObject);
			Destroy (gameObject);
			Destroy (instantiatedObject,coinSoundObject.GetComponent<AudioSource> ().clip.length);
		}
	}
}
