using UnityEngine;
using System.Collections;

public class ObjectMover : MonoBehaviour {

	//Rigidbody2D rb;
	private float movementForce = 2f;

	Vector3 startPosition;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		//rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		//rb.AddForce (new Vector2 (-1f, 0f) * movementForce);
		transform.position = new Vector3(transform.position.x - Time.deltaTime*movementForce,startPosition.y,startPosition.z);
	}
}
