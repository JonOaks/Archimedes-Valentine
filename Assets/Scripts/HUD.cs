using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour {

	public Sprite[] heartSprites;
	public Image heartUI;

	private PlayerController player;

	private int previousHealth;

	Animator anim;
	public Sprite[] lifeSprites;

	private int digitOne = 0;
	private int digitZero = 0;
	private bool lifeChanged;

	public GameObject lifeDataDigitOne;
	public GameObject lifeDataDigitZero;

	private GameObject lifeDataDigitOnePersistent;
	private GameObject lifeDataDigitZeroPersistent;

	public GameObject gameOverUI;
	public bool gameOverInProgress;

	void Start () {
		player = GameObject.Find("Player").GetComponent<PlayerController> ();
		anim = GameObject.Find("Player").GetComponent<Animator> ();
		lifeChanged = false;

		lifeDataDigitOnePersistent = GameObject.Find ("LifeData");
		lifeDataDigitZeroPersistent = GameObject.Find ("LifeData");

		gameOverInProgress = false;
	}

	void Update () {
		if((previousHealth - player.curHealth) > 1)
			player.curHealth = previousHealth - 1;
		heartUI.sprite = heartSprites[player.curHealth];
		previousHealth = player.curHealth;


		// Life Data
		lifeDataDigitOne.GetComponent<Image> ().sprite = lifeSprites[lifeDataDigitOnePersistent.GetComponent<LifeData> ().lifeDigitOne];
		lifeDataDigitZero.GetComponent<Image> ().sprite = lifeSprites[lifeDataDigitZeroPersistent.GetComponent<LifeData> ().lifeDigitZero];

		for (int k = 0; k < 10; k++) {
			if (lifeDataDigitOne.GetComponent<Image> ().sprite == lifeSprites [k]) {
				digitOne = k;
				break;
			}
		}
		for (int l = 0; l < 10; l++) {
			if (lifeDataDigitZero.GetComponent<Image> ().sprite == lifeSprites [l]) {
				digitZero = l;
				break;
			}
		}

		if (anim.GetBool ("Dead") && !lifeChanged) {
			lifeChanged = true;
			for (int i = 0; i < 2; i++) {
				if (digitOne == 0) {
					if (digitZero != 1) {
						lifeDataDigitZero.GetComponent<Image> ().sprite = lifeSprites [digitZero - 1];
						lifeDataDigitZeroPersistent.GetComponent<LifeData> ().lifeDigitZero = digitZero - 1;
					} else {
						if (!gameOverInProgress) {
							GameObject.Find ("HUD").SetActive (false);
						}

						gameOverInProgress = true;
						gameOverUI.SetActive (true);
						StartCoroutine ("Wait");
					}
				} else {
					if (digitZero == 0) {
						lifeDataDigitOne.GetComponent<Image> ().sprite = lifeSprites [digitOne - 1];
						lifeDataDigitOnePersistent.GetComponent<LifeData> ().lifeDigitOne = digitOne - 1;
					} else {
						lifeDataDigitZero.GetComponent<Image> ().sprite = lifeSprites [digitZero - 1];
						lifeDataDigitOnePersistent.GetComponent<LifeData> ().lifeDigitOne = digitOne - 1;
					}
				}
			}
		}
	}

	IEnumerator Wait () {
		yield return new WaitForSeconds (1.5f);
		lifeDataDigitOnePersistent.GetComponent<LifeData> ().lifeDigitOne = 0;
		lifeDataDigitZeroPersistent.GetComponent<LifeData> ().lifeDigitZero = 3;
		// restarting spawn point
		lifeDataDigitOnePersistent.GetComponent<LifeData> ().spawnPoint = new Vector3 (-1.9f, -114.5f, 0f);
		lifeDataDigitOnePersistent.GetComponent<LifeData> ().limitLeft = 5f;
		lifeDataDigitOnePersistent.GetComponent<LifeData> ().limitRight = -114.5f;
		lifeDataDigitOnePersistent.GetComponent<LifeData> ().limitBot = 7.4f;
		lifeDataDigitOnePersistent.GetComponent<LifeData> ().limitTop = 7.4f; 
						
		SceneManager.LoadScene ("MainMenu");
	}
}
