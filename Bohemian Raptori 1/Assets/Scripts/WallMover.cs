using UnityEngine;
using System.Collections;

public class WallMover : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (0f, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < -3.98f) {
			transform.position = new Vector3 (0f, transform.position.y, transform.position.z);
		}
	}
}
