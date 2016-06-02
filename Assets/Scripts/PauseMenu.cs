using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
	
	public GameObject pauseUI;

	private bool paused = false;
	private bool defaultPauseMenu = true;
	private bool menuMovePlayed = false;
	private bool menuSelectPlayed = false;
	private bool menuEnterPlayed = true;

	public UnityEngine.UI.Button continueBTN;
	public UnityEngine.UI.Button endBTN;

	public GameObject menuMoveSoundObject;
	public GameObject menuSelectSoundObject;
	public GameObject menuEnterSoundObject;
	private GameObject instantiatedObject;

	private float soundTime = 0.0f;

	private HUD hud;

	void Start () {
		pauseUI.SetActive (false);
		defaultPauseMenu = true;
		menuMovePlayed = false;
		menuSelectPlayed = false;
		menuEnterPlayed = true;

		hud = GameObject.Find("Main Camera").GetComponent<HUD> ();
	}

	void Update () {
		if (Input.GetButtonDown ("Pause") && !GameObject.Find("Player").GetComponent <PlayerController> ().endLevelInProgress) {
			paused = !paused;
		}

		if (paused && !hud.gameOverInProgress) {
			GameObject.Find("Player").GetComponent <PlayerController> ().levelMusic.Pause();
			pauseUI.SetActive (true);
			Time.timeScale = 0;
			if (menuEnterPlayed) {
				menuEnterPlayed = false;
				soundTime = menuEnterSoundObject.GetComponent<AudioSource> ().clip.length;
				instantiatedObject = Instantiate(menuEnterSoundObject);
				Destroy (instantiatedObject, soundTime);
			}
			if (defaultPauseMenu) {
				continueBTN.animator.Play ("Highlighted");
				endBTN.animator.Play ("Normal");
				if (menuMovePlayed) {
					menuMovePlayed = false;
					soundTime = menuMoveSoundObject.GetComponent<AudioSource> ().clip.length;
					instantiatedObject = Instantiate(menuMoveSoundObject);
					Destroy (instantiatedObject, soundTime);
				}
				if (Input.GetKeyDown (KeyCode.Z)) {
					if (!menuSelectPlayed) {
						menuSelectPlayed = true;
						soundTime = menuSelectSoundObject.GetComponent<AudioSource> ().clip.length;
						instantiatedObject = Instantiate(menuSelectSoundObject);
						Destroy (instantiatedObject, soundTime);
					}
					paused = false;
					GameObject.Find("Player").GetComponent <PlayerController> ().levelMusic.Play();
				}
			} else {
				continueBTN.animator.Play ("Normal");
				endBTN.animator.Play ("Highlighted");
				if(!menuMovePlayed){
					menuMovePlayed = true;
					soundTime = menuMoveSoundObject.GetComponent<AudioSource> ().clip.length;
					instantiatedObject = Instantiate(menuMoveSoundObject);
					Destroy (instantiatedObject, soundTime);
				}
				if (Input.GetKeyDown (KeyCode.Z)) {
					if (!menuSelectPlayed) {
						menuSelectPlayed = true;
						SceneManager.LoadScene ("LoadingScreen");
					}
					paused = false;
					GameObject.Find("Player").GetComponent <PlayerController> ().levelMusic.Play();
				}
			}
			if (Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.UpArrow)) {
				defaultPauseMenu = !defaultPauseMenu;
			}
		} else {
			if (!menuEnterPlayed && !menuSelectPlayed) {
				soundTime = menuEnterSoundObject.GetComponent<AudioSource> ().clip.length;
				instantiatedObject = Instantiate(menuEnterSoundObject);
				Destroy (instantiatedObject, soundTime);
				GameObject.Find("Player").GetComponent <PlayerController> ().levelMusic.Play();
			}
			defaultPauseMenu = true;
			menuEnterPlayed = true;
			menuSelectPlayed = false;
			menuMovePlayed = false;
			pauseUI.SetActive (false);
			Time.timeScale = 1;
		}
	}
}