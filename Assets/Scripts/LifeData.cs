using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeData : MonoBehaviour {

	public int lifeDigitOne;
	public int lifeDigitZero;

	void Awake () {
		DontDestroyOnLoad (gameObject);
		lifeDigitOne = 0;
		lifeDigitZero = 3;

		if (FindObjectsOfType(GetType()).Length > 1) {
			Destroy (gameObject);
		}
	}
}
