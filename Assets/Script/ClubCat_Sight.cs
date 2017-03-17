using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubCat_Sight : MonoBehaviour {
	public BasicEnemyScript ProCat;
	void OnTriggerEnter(Collider something){
		if (something.gameObject.tag == "Orb") {
			Debug.Log ("ENTER");
			ProCat.OrbDir (something.gameObject.transform.position);
		}
	}
}
