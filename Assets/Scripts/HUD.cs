using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	public Sprite[] heartSprites;

	public Image heartUI;

	private PlayerController player;

	private int previousHealth;

	void Start () {
		player = GameObject.Find("Player").GetComponent<PlayerController> ();
	}

	void Update () {
		if((previousHealth - player.curHealth) > 1)
			player.curHealth = previousHealth - 1;
		heartUI.sprite = heartSprites[player.curHealth];
		previousHealth = player.curHealth;
	}
}
