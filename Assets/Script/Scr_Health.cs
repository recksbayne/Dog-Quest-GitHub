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
	void Awake(){

		vModel = transform.FindChild ("Obj_Model").gameObject;
	}
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
				//vModel.GetComponent<MeshRenderer> ().enabled = false;
				//vModel.GetComponentInChildren<MeshRenderer> ().enabled = false;
				foreach (Transform childA in vModel.transform) {
					childA.GetComponent<MeshRenderer> ().enabled = false;
					foreach (Transform childB in childA.transform) {
						childB.GetComponent<MeshRenderer> ().enabled = false;
						foreach (Transform childC in childB.transform) {
							childC.GetComponent<MeshRenderer> ().enabled = false;
						}
					}
				}
				//GameObject[] Those = vModel.GetComponentInChildren
				vOn = false;
			} else {
				//vModel.GetComponent<MeshRenderer> ().enabled = true;
				//vModel.GetComponentInChildren<MeshRenderer> ().enabled = true;
				foreach (Transform childA in vModel.transform) {
					childA.GetComponent<MeshRenderer> ().enabled = true;
					foreach (Transform childB in childA.transform) {
						childB.GetComponent<MeshRenderer> ().enabled = true;
						foreach (Transform childC in childB.transform) {
							childC.GetComponent<MeshRenderer> ().enabled = true;
						}
					}
				}
				vOn = true;
			}
			if (vCount >= 12) {
				vFlicker = false;
				if (gameObject.tag == "Cat") {
					if (vHealth <= 0) {
						gameObject.SendMessage ("isDead");
						Destroy (gameObject);
					}
					gameObject.SendMessage ("StopGetHit");
				}
			}
		}
	}
	public void GetDamaged(){
		if (!vFlicker) {
			vOn = true;
			vFlicker = true;
			vHealth -= 1;
			vCount = 0;
			vTimer = 0f;
		}
		}
}

