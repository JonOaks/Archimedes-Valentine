using UnityEngine;
using System.Collections;

public class SoundEffect : MonoBehaviour {

	void Start () {
		GetComponent<AudioSource>().Play();
		StartCoroutine("SoundEnd");
	}

	IEnumerator SoundEnd () {
		yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
		Destroy(gameObject);
	}
}
