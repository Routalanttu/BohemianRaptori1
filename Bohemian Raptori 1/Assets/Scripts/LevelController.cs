using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*

public class LevelController : MonoBehaviour {
	public Camera camera;
	public float scrollSpeed = -1.0f;
	public float start = 100.0f;
	public float end = -100.0f;
	public bool isAlive = true;
	public List<GameObject> prefabs;

	private List<GameObject> alive = new List<GameObject>();
	private int count = 0;

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
		float amount = Time.fixedTime * scrollSpeed;
		foreach(var block in alive)
		{
			var info = block.GetComponent<BlockInfo>();
			info.moveBlock (amount);
		}
	}

	// Update is called once per frame
	void Update () {
		if( !isAlive ) {
			return;
		}

		float vStart = transform.position.x + start;
		bool done = false;
		do {
			if( alive.Count <= 0 ) {
				var prefab = getPrefab();
				var info = prefab.GetComponent<BlockInfo>();

				addBlock(prefab, transform.position);
			}
			else {
				var last = alive[alive.Count - 1];
				var lastInfo = prefab.GetComponent<BlockInfo>();

				var prefab = getPrefab();
				var block = addBlock(prefab, transform.position);

				var blockInfo = block.GetComponent<BlockInfo>();
				go.transform.position = new Vector3(
						lastInfo.getBack() + blockInfo.getHalf() , 
						transform.position.y , 
						transform.position.z);
			}

			var last = alive[alive.Count - 1];
			var info = prefab.GetComponent<BlockInfo>();

			if(!info.isPast(vStart)) {
				done = true;
			}
		} 
		while(!done);

		float vEnd = transform.position.x + end;
		for (var i = alive.Count; i >= 0; --i) {
			var block = alive[i];
			var info = block.GetComponent<BlockInfo>();

			if( info == null || info.isPast(vEnd) ) {
				Destroy(block);
			}
			alive.RemoveAt(i);
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
		return go;
	}
}


*/