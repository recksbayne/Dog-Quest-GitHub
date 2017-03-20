﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Collectable : MonoBehaviour {
	public GameObject vItem;
	public bool vIsLoot = true;
	public int vBoneNumber;
	public AudioSource BonePick;
	// Use this for initialization
	void Start () {
		if (!vIsLoot) {
			GameObject tGO = GameObject.FindGameObjectWithTag ("Player");
			bool tBool = tGO.GetComponent<Scr_Player> ().aBonesHere [vBoneNumber];
			if (!tBool)
				Destroy (this.gameObject);
		}
	}
	void GotYou(){
			BonePick.Play ();
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
