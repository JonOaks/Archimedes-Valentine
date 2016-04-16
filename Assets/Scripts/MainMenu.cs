using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public UnityEngine.UI.Button startBTN;

	public GameObject menuSelectSoundObject;

	public GameObject backgroundSound;
	private float soundTime = 0.0f;

	void Start () {
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Z)) {
			startBTN.animator.Play ("Pressed");
			startBTN.animator.Stop ();
			backgroundSound.GetComponent<AudioSource> ().Stop ();
			soundTime = menuSelectSoundObject.GetComponent<AudioSource> ().clip.length;
			Instantiate (menuSelectSoundObject);
			StartCoroutine ("Wait");
		} else {
			startBTN.animator.Play ("Highlighted");
		}
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(soundTime-1);

		SceneManager.LoadScene ("Stage1");
	}
}
