using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {

	public GameObject soundObject;

	void Start () {
		Time.timeScale = 1;
		soundObject = GameObject.Find("Loading_Screen_Sound");
	}

	void FixedUpdate () {
		if (soundObject.GetComponent<AudioSource> ().volume == 0) {
			SceneManager.LoadScene ("Stage1");
		}
		soundObject.GetComponent<AudioSource> ().volume -= 0.001f;
	}
}
