using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Hitbox : MonoBehaviour {
	public GameObject vPlayer;
	public bool vIsWeapon = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter (Collider Other){
		switch (Other.tag) {
		case "TreasureBox":
			Other.SendMessage ("GetHit");
			break;
		case "Door":
			Other.SendMessage ("GetHit");
			break;
		case "Switch":
			//if (vIsWeapon)
				Other.SendMessage ("GetHit");
			break;
		case "Player":
			Other.SendMessage("GetDamaged");
			break;
		}
	}
}
