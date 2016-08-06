using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {
	public float scrollSpeed = -2.0f;
	public float start = 10.0f;
	public float end = -10.0f;
	public bool isAlive = true;
	public GameObject emptyPrefab;
	public List<GameObject> prefabs;

	private List<GameObject> alive = new List<GameObject>();

	public PlayerControls player;


	void Start() {
		if (prefabs.Count <= 0) {
			Debug.Log ("Prefabs havent been set!!");
		}

	}

	void FixedUpdate() {
		if( !isAlive ) {
			return;
		}

		// Update alive block positions..
		float amount = Time.fixedDeltaTime * scrollSpeed;
		foreach(var block in alive)
		{
			var info = block.GetComponent<BlockInfo>();
			info.moveBlock (amount);
		}
	}

	void startup() 
	{
		var prefab = emptyPrefab;
		Vector3 lastEnd = new Vector3(
			transform.position.x + end , 
			transform.position.y, 
			transform.position.z);

		while(true) {
			var go = addBlock(prefab, lastEnd);
			var info = go.GetComponent<BlockInfo>();

			go.transform.position = new Vector3(
				lastEnd.x + info.getHalf() , 
				lastEnd.y ,
				lastEnd.z);

			lastEnd = new Vector3 (
				go.transform.position.x + info.getHalf(),
				go.transform.position.y,
				go.transform.position.z);

			if (lastEnd.x > transform.position.x + start) {
				break;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if( !isAlive ) {
			return;
		}

		float vStart = transform.position.x + start;
		while(true)
		{
			GameObject last = null;
			BlockInfo lastInfo = null;

			if( alive.Count <= 0 ) {
				startup();
			}
				
			last = alive[alive.Count - 1];
			lastInfo = last.GetComponent<BlockInfo>();

			if(lastInfo.isPast(vStart)) {
				var prefab = getPrefab();
				var go = addBlock(prefab, transform.position);

				var blockInfo = go.GetComponent<BlockInfo>();
				go.transform.position = new Vector3(
						lastInfo.getBack() + blockInfo.getHalf() , 
						transform.position.y , 
						transform.position.z);
			}
			else {
				break;
			}
		}

		float vEnd = transform.position.x + end;
		for (var i = alive.Count - 1; i >= 0; --i) {
			var block = alive[i];
			var info = block.GetComponent<BlockInfo>();

			if( info == null || info.isPast(vEnd) ) {
				Destroy(block);
				alive.RemoveAt(i);
			}
		}
	}

	GameObject getPrefab() {
		return prefabs [Random.Range(0, prefabs.Count)];
	}

	GameObject addBlock(GameObject prefab , Vector3 position) {
		var obj = Instantiate(
			prefab, 
			position, 
			Quaternion.identity);

		var go = obj as GameObject;
		if (go == null) {
			return null;
		}

		alive.Add (go);

		Debug.Log (go.transform.name);
		Debug.Log (go.transform.FindChild ("RaptorVersion").gameObject.transform.name);
		Debug.Log (go.transform.FindChild ("HumanChild").gameObject.transform.name);
		Debug.Log (go.transform.FindChild ("HumanChild").FindChild ("RaptorChild").gameObject.transform.name);

		if (player.GetRaptorityState () == true) {
			go.GetComponent<SpriteRenderer> ().enabled = false;

			go.transform.FindChild ("RaptorVersion").gameObject.GetComponent<SpriteRenderer> ().enabled = true;
			go.transform.FindChild ("HumanChild").gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			go.transform.FindChild ("HumanChild").FindChild ("RaptorChild").gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		} else if (player.GetRaptorityState () == false) {
			go.GetComponent<SpriteRenderer> ().enabled = true;

			go.transform.FindChild ("RaptorVersion").gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			go.transform.FindChild ("HumanChild").gameObject.GetComponent<SpriteRenderer> ().enabled = true;
			go.transform.FindChild ("HumanChild").FindChild ("RaptorChild").gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		}

		//SetVisibleWorld (go, player.GetRaptorityState ());

		return go;
	}

	/*
	void SetVisibleWorld (GameObject go, bool isRaptor) {
		if (go.transform.name == "RaptorVersion") {
			return;
		}

		if (isRaptor) {
			go.GetComponent<SpriteRenderer> ().enabled = false;

			go.transform.Find ("RaptorVersion").gameObject.GetComponent<SpriteRenderer> ().enabled = true;
			go.transform.Find ("HumanChild").gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			go.transform.Find ("RaptorChild").gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		} else {

		}
	}
	*/
}
