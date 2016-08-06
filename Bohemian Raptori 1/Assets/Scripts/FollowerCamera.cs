using UnityEngine;
using System.Collections;

public class FollowerCamera : MonoBehaviour {
	public Transform target;
	public bool followX = true;
	public bool followY = false;
	public bool followZ = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(
			followX ? target.position.x : transform.position.x, // just follow the x position
			followY ? target.position.y : transform.position.y, 
			followZ ? target.position.z : transform.position.z
		);
	}
}
