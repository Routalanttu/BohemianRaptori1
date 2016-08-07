using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour {

	private int score;


	Rigidbody2D rb;
	Vector2 jumpDirection;

	Animator humanAnim;
	Animator raptorAnim;

	//SpriteRenderer humanRenderer;
	//SpriteRenderer raptorRenderer;

	private float jumpCooldown;
	private float punchCooldown;
	private float shiftCooldown;
	private float gothruCooldown;


	private bool isRaptor;

	private bool isAlive;


	public Text scoreText;
	public Text resetText;

	//private GameObject[] humanGos;
	//private GameObject[] raptorGos;


	//private SpriteRenderer[] humanSprites;
	//private SpriteRenderer[] raptorSprites;

	private float jumpPower = 120f;

	AudioSource audio;
	public AudioClip jumpSound;
	public AudioClip deathSound;
	public AudioClip goThroughSound;
	public AudioClip shiftSound;
	public AudioClip resetSound;

	// Use this for initialization
	void Start () {
		
		isAlive = true;
		isRaptor = false;
		rb = GetComponent<Rigidbody2D> ();


		scoreText.text = "Score: " + score + "      Z = Switch realities   X = Jump   C = Punch   R = Reset";
		resetText.enabled = false;

		/*
		humanGos = GameObject.FindGameObjectsWithTag ("HumanWorld");
		raptorGos = GameObject.FindGameObjectsWithTag ("RaptorWorld");
		*/

		/*
		humanSprites = new SpriteRenderer[humanGos.Length];
		raptorSprites = new SpriteRenderer[raptorGos.Length];

		for (int i = 0; i < humanGos.Length; i++) {
			humanSprites[i] = humanGos [i].GetComponent<SpriteRenderer> ();
		}

		for (int i = 0; i < raptorGos.Length; i++) {
			raptorSprites[i] = raptorGos [i].GetComponent<SpriteRenderer> ();
		}
		*/

		humanAnim = GetComponent<Animator> ();
		raptorAnim = transform.FindChild ("RaptorVersion").GetComponent<Animator> ();

		/*
		humanRenderer = GetComponent<SpriteRenderer> ();
		raptorRenderer = transform.FindChild ("RaptorVersion").GetComponent<SpriteRenderer> ();
		raptorRenderer.enabled = false;
		*/

		SetHumanObjectVisibility (true);
		SetRaptorObjectVisibility (false);

		jumpDirection = new Vector2 (0f,1f);

		audio = GetComponent<AudioSource> ();
		audio.PlayOneShot (resetSound);

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isAlive) {
			if (Input.GetButton ("Jump") && (jumpCooldown <= 0f && punchCooldown <= 0f) && (transform.position.y < -3.7f)) {
				audio.PlayOneShot (jumpSound);
				rb.AddForce (jumpDirection * jumpPower);
				humanAnim.SetBool ("isJumping", true);
				raptorAnim.SetBool ("isJumping", true);
				jumpCooldown = 0.75f;
			}

			if (Input.GetButton ("Fire1") && jumpCooldown <= 0f && punchCooldown <= 0f) {
				audio.PlayDelayed (0.2f);
				humanAnim.SetBool ("isPunching", true);
				raptorAnim.SetBool ("isPunching", true);
				punchCooldown = 0.75f;
			}

			if (Input.GetButton ("Fire2") && shiftCooldown <= 0f) {
				if (!isRaptor) {
					//raptorRenderer.enabled = true;
					//humanRenderer.enabled = false;
					SetRaptorObjectVisibility (true);
					SetHumanObjectVisibility (false);
					isRaptor = true;
					jumpPower = 240f;
					audio.PlayOneShot (shiftSound);
				} else if (isRaptor) {
					//raptorRenderer.enabled = false;
					//humanRenderer.enabled = true;
					SetRaptorObjectVisibility (false);
					SetHumanObjectVisibility (true);
					isRaptor = false;
					jumpPower = 120f;
					audio.PlayOneShot (shiftSound);
				}
				shiftCooldown = 0.25f;
			}

		}

		if (Input.GetKey (KeyCode.R)) {
			Application.LoadLevel ("Main");
		}

		//Debug.Log (transform.position.y);
	}

	void Update () {
		JumpParameterCheck ();
		PunchParameterCheck ();
		ShiftParameterCheck ();
		GothruParameterCheck ();
	}

	void JumpParameterCheck () {
		if (jumpCooldown > 0f) {
			jumpCooldown -= Time.deltaTime;
		}

		if (jumpCooldown <= 0f) {
			humanAnim.SetBool ("isJumping", false);
			raptorAnim.SetBool ("isJumping", false);
		}
	}

	void PunchParameterCheck () {
		if (punchCooldown > 0f) {
			punchCooldown -= Time.deltaTime;
		}

		if (punchCooldown <= 0f) {
			humanAnim.SetBool ("isPunching", false);
			raptorAnim.SetBool ("isPunching", false);
		}
	}

	void ShiftParameterCheck () {
		if (shiftCooldown > 0f) {
			shiftCooldown -= Time.deltaTime;
		}
	}

	void GothruParameterCheck () {
		if (gothruCooldown > 0f) {
			gothruCooldown -= Time.deltaTime;
		}
		if (gothruCooldown <= 0f) {
			humanAnim.SetBool ("isPassing", false);
			raptorAnim.SetBool ("isPassing", false);
		}
	}

	void SetHumanObjectVisibility (bool newState) {
		GameObject[] humanGos = GameObject.FindGameObjectsWithTag ("HumanWorld");

		for (int i = 0; i < humanGos.Length; i++) {
			humanGos [i].GetComponent<SpriteRenderer> ().enabled = newState;
		}
	}

	void SetRaptorObjectVisibility (bool newState) {
		GameObject[] raptorGos = GameObject.FindGameObjectsWithTag ("RaptorWorld");

		for (int i = 0; i < raptorGos.Length; i++) {
			raptorGos [i].GetComponent<SpriteRenderer> ().enabled = newState;
		}
	}

	public float GetPunchCooldown () {
		return punchCooldown;
	}

	void Die () {
		//Pakko laittaa ihmistä enemmän oikealle sivulle koska death-animaatio oli tehty leveämmäksi.
		humanAnim.SetBool ("isDead", true);
		raptorAnim.SetBool ("isDead", true);
		audio.PlayOneShot (deathSound);

		Debug.Log ("Death");

		isAlive = false;
		resetText.enabled = true;
	}


	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag("Jumpable")) {
			Die();
		}
		if (other.gameObject.CompareTag("JumpScorer")) {
			Animator duckerAnim = other.gameObject.transform.parent.parent.GetComponent<Animator> ();

			duckerAnim.SetBool ("isDead", true);

			score++;
		}
		if (other.gameObject.CompareTag("Punchable")) {
			if ((punchCooldown >= 0.01f) && (!isRaptor)) {

				Animator grannyAnim = other.gameObject.transform.parent.GetComponent<Animator> ();

				grannyAnim.SetBool ("isDead", true);

				score++;
			} else {
				Die ();
			}
		}
		if (other.gameObject.CompareTag ("HumanPassable")) {
			if (isRaptor) {
				Die ();
			} else {
				Animator doorAnim = other.gameObject.transform.FindChild ("Door").GetComponent<Animator> ();

				// Näytä avautuva ovi.
				humanAnim.SetBool("isPassing",true);
				gothruCooldown = 0.5f;
				audio.PlayOneShot (goThroughSound);
				score++;

				doorAnim.SetBool ("isOpened", true);
			}
		}
		if (other.gameObject.CompareTag ("RaptorPassable")) {
			if ((!isRaptor)) {
				Die ();
			} else {
				// Näytä suhahtava puska.
				raptorAnim.SetBool("isPassing",true);
				audio.PlayOneShot (goThroughSound);
				score++;

				Animator guardAnim = other.gameObject.transform.parent.GetComponent<Animator> ();

				guardAnim.SetBool ("isDead", true);

			}
		}

		scoreText.text = "Score: " + score + "      Z = Switch realities   X = Jump   C = Punch   R = Reset";
		Debug.Log (score);
	}


	public bool GetRaptorityState () {
		return isRaptor;
	}

	public bool GetAliveness() {
		return isAlive;
	}

	
}
