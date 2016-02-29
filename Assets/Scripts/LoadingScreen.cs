using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {

	public GameObject soundObject;
	public float soundVolume;

	void Start () {
		Time.timeScale = 1;
		soundObject = GameObject.Find("Loading_Screen_Sound");
	}

	void Update () {
		soundVolume = soundObject.GetComponent<AudioSource> ().volume;
		soundObject.GetComponent<AudioSource> ().volume = soundVolume - 0.0015f;
		if (soundObject.GetComponent<AudioSource> ().volume == 0) {
			SceneManager.LoadScene ("Stage1");
		}
	}
}
