using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Collectable : MonoBehaviour {
	public GameObject vItem;
	public bool vIsLoot = true;
	public int vBoneNumber;
	// Use this for initialization
	void Start () {
		if (!vIsLoot) {
			GameObject tGO = GameObject.FindGameObjectWithTag ("Player");
			bool tBool = tGO.GetComponent<Scr_Player> ().aBonesHere [vBoneNumber];
			if (!tBool)
				Destroy (this.gameObject);
		}
	}
	void Update(){
		if (transform.position.y > 1f)
			transform.position = new Vector3 (transform.position.x, transform.position.y - .05f, transform.position.z);
		else
			transform.position = new Vector3 (transform.position.x, 1f, transform.position.z);
	}
	void GotYou(){
			GameObject tObj;
			tObj = Instantiate (vItem);
			tObj.transform.position = new Vector3 (transform.position.x, transform.position.y + 1f, transform.position.z);

		if (!vIsLoot) {
			GameObject tGO = GameObject.FindGameObjectWithTag ("Player");
			tGO.GetComponent<Scr_Player> ().aBonesHere [vBoneNumber] = false;
		}
			Destroy (this.gameObject);
	}
}
