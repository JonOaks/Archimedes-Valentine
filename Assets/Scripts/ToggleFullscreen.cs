using UnityEngine;
using System.Collections;

public class ToggleFullscreen : MonoBehaviour {
	void Update () {
		if (Input.GetKeyDown(KeyCode.M))
			Screen.fullScreen = !Screen.fullScreen;
	}
}
