using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	
	public Transform sightStart, sightEnd;

	Animator anim;

	public float jumpForce;
	public float maxSpeed;
	public float move;

	public bool facingRight;
	public bool grounded;
	public bool doubleJump;
	public bool edge;
	private bool jumped = false;

	private PlayerAttack playerAttack;

	// Stats
	public int curHealth;
	public int maxHealth;

	// Knockback
	public float knockback;
	public float knockbackLength;
	public float knockbackCount;
	public bool knockFromRight;

	// Death Sound
	public GameObject deathSoundObject;
	private GameObject instantiatedSoundObject;
	private bool deathSoundPlayed = false;

	// Transition Between Areas
	public GameObject cameraObject;
	private float cameraSize;
	private CameraController cameraScript;
	public bool cameraMoving = false;
	public GameObject transitionObject;

	private float previousLimitLeft;
	private float previousLimitRight;
	private float previousLimitTop;
	private float previousLimitBot;

	public GameObject gameOverUI;
	public GameObject gameOverImage;
	public GameObject loadingImage;
	public GameObject loadingImage2;
	public GameObject loadingImage3;
	public bool endLevelInProgress;
	public GameObject HUD;
	private GameObject persistentObject;

	void Start () {
		persistentObject = GameObject.Find ("LifeData");
		gameObject.transform.position = persistentObject.GetComponent<LifeData> ().spawnPoint;
		anim = gameObject.GetComponent<Animator> ();
		playerAttack = gameObject.GetComponent<PlayerAttack> ();

		jumpForce = 430f;
		maxSpeed = 9f;

		facingRight = true;
		grounded = false;
		doubleJump = true;
		jumped = false;

		maxHealth = 5;
		curHealth = maxHealth;

		cameraScript = cameraObject.GetComponent<CameraController> ();
		cameraSize = cameraObject.GetComponent<Camera> ().aspect * (cameraObject.GetComponent<Camera>().orthographicSize * 2f);
		cameraScript.ActivateLimits (persistentObject.GetComponent <LifeData> ().limitLeft, persistentObject.GetComponent <LifeData> ().limitRight,
			persistentObject.GetComponent <LifeData> ().limitBot, persistentObject.GetComponent <LifeData> ().limitTop);

		endLevelInProgress = false;
	}

	void FixedUpdate () {

		anim.SetBool ("Ground", grounded);

		anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

		move = Input.GetAxis ("Horizontal");

		if(grounded && playerAttack.attacking)
		{
			move = 0;
		}

		if(anim.GetBool ("Dead")) {
			move = 0;
			doubleJump = true;
			jumped = true;
			grounded = false;
			playerAttack.attackTrigger.enabled = false;

			if (instantiatedSoundObject == null)
				SceneManager.LoadScene ("Stage1");
			cameraScript.enabled = false;
		}
			
		if (cameraMoving) {
			move = 0;
			loadingImage.SetActive (true);
		}

		anim.SetFloat ("Speed", Mathf.Abs(move));

		if (knockbackCount <= 0) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D> ().velocity.y);
		} else {
			if (knockFromRight) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (-knockback, knockback);
			}
			if (!knockFromRight) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (knockback, knockback);
			}
			knockbackCount -= Time.deltaTime;
		}

		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();

		// EDGE LOGIC
		Debug.DrawLine (sightStart.position, sightEnd.position, Color.green); // Debugging line reference

		// Checking if the player is close to the right edge
		if (Physics2D.Linecast (sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer ("Right Edge")) 
			&& Mathf.Abs(move) == 0.0 && grounded && facingRight) {
			edge = true;
		// Checking if the player is close to the left edge
		} else if (Physics2D.Linecast (sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer ("Left Edge")) 
			&& Mathf.Abs(move) == 0.0 && grounded && !facingRight) {
			edge = true;
		} else {
			edge = false;
		}

		anim.SetBool ("Edge", edge);

		// In case the couroutine from the enemy on trigger function stops
		if (!GetComponent<SpriteRenderer> ().enabled) {
			GetComponent<SpriteRenderer> ().enabled = true;
		}

		if (grounded) {
			jumped = false;
		}
	}

	void Update () {
		if (grounded) {
			jumped = false;
		}

		if ((grounded || !jumped) && Input.GetKeyDown (KeyCode.Space)) {
			grounded = false;
			jumped = true;
			anim.SetBool ("Ground", false);
			GetComponent<Rigidbody2D> ().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
			GetComponent<Rigidbody2D>().AddForce (new Vector2(0, jumpForce));

			/*if (!doubleJump && !grounded) {
				anim.SetBool ("Ground", false);
				GetComponent<Rigidbody2D> ().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
				GetComponent<Rigidbody2D>().AddForce (new Vector2(0, 140f));
				doubleJump = true;
			}*/
		}

		if (curHealth > maxHealth) {
			curHealth = maxHealth;
		}

		if (curHealth <= 0) {
			Die ();

		}

		if (!cameraMoving) {
			gameOverUI.SetActive (false);
			gameOverImage.SetActive (true);
			loadingImage.SetActive (false);
			endLevelInProgress = false;
			HUD.SetActive (true);
		}
	}

	void Flip () {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void Die () {
		Physics2D.IgnoreLayerCollision(11, 13, true);
		anim.SetBool ("Dead", true);
		if (!deathSoundPlayed) {
			deathSoundPlayed = true;
			instantiatedSoundObject = Instantiate (deathSoundObject);
			Destroy (instantiatedSoundObject, deathSoundObject.GetComponent<AudioSource> ().clip.length + 1);
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Fall") {
			Die ();
		}

		if (col.name == "EndLevel1") {
			endLevelInProgress = true;
			HUD.SetActive (false);
			gameOverUI.SetActive (true);
			gameOverImage.SetActive (false);

			persistentObject.GetComponent <LifeData> ().limitBot = -114.5f;
			persistentObject.GetComponent <LifeData> ().limitTop = -114.5f;
			Vector3 target = new Vector3 (-1.9f, -114.5f, 0f);
			Vector3 cameraTarget = new Vector3 (5f, -114.5f, 0f);
			cameraScript.MoveCamera (cameraTarget, 50f);
			cameraScript.ActivateLimits (5f, 149.8f, persistentObject.GetComponent <LifeData> ().limitBot, persistentObject.GetComponent <LifeData> ().limitTop);

			gameObject.transform.position = target;
			GameObject.Find ("LifeData").GetComponent<LifeData> ().spawnPoint = target;
			curHealth = maxHealth;
		}

		//Transition between areas logic
		/*if (col.tag == "Transition") {
			if (!facingRight) {
				previousLimitLeft = cameraScript.limitLeft;
				previousLimitRight = cameraScript.limitRight;
				//previousLimitTop = cameraScript.limitTop;
				//previousLimitBot = cameraScript.limitBottom;

				Vector3 target = new Vector3 (col.transform.position.x-5, cameraObject.transform.position.y, 0);
				cameraScript.MoveCamera (target, 20f);
				cameraScript.ActivateLimits (col.transform.position.x - 9, col.transform.position.x - 5, (float)2.04, (float)2.04);

				transitionObject.transform.position = new Vector3 (transitionObject.transform.position.x-5+cameraSize/2f, transitionObject.transform.position.y, transitionObject.transform.position.z);
			} else {
				Vector3 target = new Vector3 (previousLimitLeft, cameraObject.transform.position.y, 0);
				cameraScript.MoveCamera (target, 20f);
				cameraScript.ActivateLimits (previousLimitLeft, previousLimitRight, (float)2.04, (float)2.04);

				transitionObject.transform.position = new Vector3 (transitionObject.transform.position.x+5-cameraSize/2f, transitionObject.transform.position.y, transitionObject.transform.position.z);
			}
		}*/
	}

	IEnumerator Wait(float time) {
		yield return new WaitForSeconds (time);
	}
}