using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Src_OrbScript : MonoBehaviour {

	void OnTriggerEnter (Collider other){
		if (other.gameObject.tag == "Cat") {
			other.gameObject.SendMessage ("EnteringOrb");
		}
	}
}
