using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetCoinOnContact : MonoBehaviour {

	public GameObject coinSoundObject;
	private GameObject instantiatedObject;

	public Sprite[] scoreSprites;

	public Image scoreUIZero;
	public Image scoreUIOne;
	public Image scoreUITwo;
	public Image scoreUIThree;

	private int digitThree = 0;
	private int digitTwo = 0;
	private int digitOne = 0;
	private int digitZero = 0;

	void OnTriggerEnter2D (Collider2D col) {
		if (col.name == "Player") {
			instantiatedObject = Instantiate (coinSoundObject);


			for (int i = 0; i < 10; i++) {
				if (scoreUIThree.sprite == scoreSprites [i]) {
					digitThree = i;
					break;
				}
			}
			for (int j = 0; j < 10; j++) {
				if (scoreUITwo.sprite == scoreSprites [j]) {
					digitTwo = j;
					break;
				}
			}
			for (int k = 0; k < 10; k++) {
				if (scoreUIOne.sprite == scoreSprites [k]) {
					digitOne = k;
					break;
				}
			}
			for (int l = 0; l < 10; l++) {
				if (scoreUIZero.sprite == scoreSprites [l]) {
					digitZero = l;
					break;
				}
			}

			if (digitZero + 1 == 10) {
				scoreUIZero.sprite = scoreSprites [0];
				if (digitOne + 1 == 10) {
					scoreUIOne.sprite = scoreSprites [0];
					if (digitTwo + 1 == 10) {
						scoreUITwo.sprite = scoreSprites [0];
						scoreUIThree.sprite = scoreSprites [digitThree + 1];
					} else {
						scoreUITwo.sprite = scoreSprites [digitTwo + 1];
					}
				} else {
					scoreUIOne.sprite = scoreSprites [digitOne + 1];
				}
			} else {
				scoreUIZero.sprite = scoreSprites [digitZero + 1];
			}

			Destroy (gameObject);
			Destroy (instantiatedObject,coinSoundObject.GetComponent<AudioSource> ().clip.length);
		}
	}
}
