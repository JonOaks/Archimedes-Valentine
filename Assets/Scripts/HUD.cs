using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	public Sprite[] heartSprites;

	public Image heartUI;

	private PlayerController player;

	void Start () {
		player = GameObject.Find("Player").GetComponent<PlayerController> ();
	}

	void Update () {
		heartUI.sprite = heartSprites[player.curHealth];
	}
}
