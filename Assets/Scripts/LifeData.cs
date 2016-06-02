using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeData : MonoBehaviour {

	public int lifeDigitOne;
	public int lifeDigitZero;
	public Vector3 spawnPoint;
	public Vector3 cameraPosition;
	public float limitLeft;
	public float limitRight;
	public float limitTop;
	public float limitBot;

	void Awake () {
		DontDestroyOnLoad (gameObject);
		lifeDigitOne = 0;
		lifeDigitZero = 3;
		spawnPoint = new Vector3 (-0.73f, 5.4f, 0f);
		limitLeft = 6f;
		limitRight = 149.8f;
		limitTop = 7.4f;
		limitBot = 7.4f;

		if (FindObjectsOfType(GetType()).Length > 1) {
			Destroy (gameObject);
		}
	}
}
