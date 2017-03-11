using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Health : MonoBehaviour {

	// Base Stats
	public int vHealth;
	public bool vCanDie;


	// Animation
	public GameObject vModel;
	public bool vFlicker;
	private bool vOn;
	private float vTimer;
	private int vCount;
	
	// Update is called once per frame
	void Update () {
		if (vFlicker)
			Flicker ();
	}
	void Flicker(){
		if (vTimer >= 0f) {
			vTimer -= .01f;
		} else {vCount += 1;
			vTimer = .1f;
			if (vOn) {
				vModel.GetComponent<MeshRenderer> ().enabled = false;
				vOn = false;
			} else {
				vModel.GetComponent<MeshRenderer> ().enabled = true;
				vOn = true;
			}
			if (vCount >= 12) {
				vFlicker = false;
			}
		}
	}
	void GetDamaged(){
		if (!vFlicker) {
			vOn = true;
			vFlicker = true;
			vCount = 0;
			vTimer = 0f;
		}
		}
}

