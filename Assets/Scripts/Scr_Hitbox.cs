using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Hitbox : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter (Collider Other){
		Debug.Log ("Hit");
	}
}
