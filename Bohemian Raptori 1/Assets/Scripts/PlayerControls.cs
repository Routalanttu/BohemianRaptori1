using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	Rigidbody2D rb;
	Vector2 jumpDirection;

	Animator humanAnim;
	Animator raptorAnim;

	//SpriteRenderer humanRenderer;
	//SpriteRenderer raptorRenderer;

	private float jumpCooldown;
	private float punchCooldown;
	private float shiftCooldown;


	private bool isRaptor;


	//private GameObject[] humanGos;
	//private GameObject[] raptorGos;


	//private SpriteRenderer[] humanSprites;
	//private SpriteRenderer[] raptorSprites;

	private float jumpPower = 120f;

	AudioSource audio;
	public AudioClip jumpSound;
	public AudioClip deathSound;
	public AudioClip goThroughSound;

	// Use this for initialization
	void Start () {
		isRaptor = false;
		rb = GetComponent<Rigidbody2D> ();

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

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetButton ("Jump") && (jumpCooldown <= 0f && punchCooldown <= 0f) && (transform.position.y < -3.7f)) {
			audio.PlayOneShot(jumpSound);
			rb.AddForce(jumpDirection*jumpPower);
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
				SetRaptorObjectVisibility(true);
				SetHumanObjectVisibility (false);
				isRaptor = true;
				jumpPower = 240f;
			} else if (isRaptor) {
				//raptorRenderer.enabled = false;
				//humanRenderer.enabled = true;
				SetRaptorObjectVisibility(false);
				SetHumanObjectVisibility(true);
				isRaptor = false;
				jumpPower = 120f;
			}
			shiftCooldown = 0.25f;
		}



		//Debug.Log (transform.position.y);
	}

	void Update () {
		JumpParameterCheck ();
		PunchParameterCheck ();
		ShiftParameterCheck ();
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
		audio.PlayOneShot (deathSound);

		Debug.Log ("Death");
	}


	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag("Jumpable")) {
			Die();
		}
		if (other.gameObject.CompareTag("Punchable")) {
			if ((punchCooldown >= 0.01f) && (!isRaptor)) {
				// Mummon turpaansaanti
			} else {
				Die ();
			}
		}
		if (other.gameObject.CompareTag ("HumanPassable")) {
			if (isRaptor) {
				Die ();
			} else {
				// Näytä avautuva ovi.
				// play gothrusound
			}
		}
		if (other.gameObject.CompareTag ("RaptorPassable")) {
			if ((!isRaptor)) {
				Die ();
			} else {
				// Näytä suhahtava puska.
				// play gothrusound
			}
		}
	}


	public bool GetRaptorityState () {
		return isRaptor;
	}

	
}
