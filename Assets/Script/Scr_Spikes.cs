using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Spikes : MonoBehaviour {
	public bool vSwitchable;
	public bool vOn = true;
	private float vY;
	public bool vMove;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (vMove) {
			if (vOn) {
				if (vY < 0.5f)
					vY += .01f;
				else {
					vY = 0.5f;
					vMove = false;
				}
			} else {
				if (vY > 0f)
					vY -= .01f;
				else {
					vY = 0f;
					vMove = false;
				}
			}

			transform.position = new Vector3 (transform.position.x, vY, transform.position.z);
		}
	}
	void GetSwitched(){
		Debug.Log ("Get Switched");
		if (vSwitchable) {
			vMove = true;
			if (vOn)
				vOn = false;
			else
				vOn = true;
		}
	}
}
