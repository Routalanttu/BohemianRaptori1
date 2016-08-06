using UnityEngine;
using System.Collections;

public class EchoLocation : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		Debug.Log (transform.position);
		Debug.Log (transform.localPosition);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
