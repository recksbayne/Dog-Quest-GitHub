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
	public AudioSource getHitSound;
	
	// Update is called once per frame
	void Awake(){

		vModel = transform.FindChild ("Obj_Model").gameObject;
	}
	void Update () {
		if (vFlicker)
			Flicker ();
	}
	public void ResetLife(){
		vHealth = 10;
		foreach (Transform childA in vModel.transform) {
			childA.GetComponent<MeshRenderer> ().enabled = true;
			foreach (Transform childB in childA.transform) {
				childB.GetComponent<MeshRenderer> ().enabled = true;
				foreach (Transform childC in childB.transform) {
					childC.GetComponent<MeshRenderer> ().enabled = true;
				}
			}
		}
	}
	void Flicker(){
		if (vTimer >= 0f) {
			vTimer -= .01f;
		} else {vCount += 1;
			vTimer = .1f;
			if (vOn) {
				foreach (Transform childA in vModel.transform) {
					childA.GetComponent<MeshRenderer> ().enabled = false;
					foreach (Transform childB in childA.transform) {
						childB.GetComponent<MeshRenderer> ().enabled = false;
						foreach (Transform childC in childB.transform) {
							childC.GetComponent<MeshRenderer> ().enabled = false;
						}
					}
				}
				vOn = false;
			} else {
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
				if (gameObject.tag == "Player") {
					if (vHealth <= 0) {
						GameObject tGO = GameObject.FindGameObjectWithTag ("Canvas");
						tGO.SendMessage("GotoNextRoom",-1);
						foreach (Transform childA in vModel.transform) {
							childA.GetComponent<MeshRenderer> ().enabled = false;
							foreach (Transform childB in childA.transform) {
								childB.GetComponent<MeshRenderer> ().enabled = false;
								foreach (Transform childC in childB.transform) {
									childC.GetComponent<MeshRenderer> ().enabled = false;
								}
							}
						}
					}
					//gameObject.SendMessage ("StopGetHit");
				}
			}
		}
	}
	public void GetDamaged(){
		if (!vFlicker && vHealth > 0) {
			vOn = true;
			vFlicker = true;
			vHealth -= 1;
			vCount = 0;
			vTimer = 0f;
			getHitSound.Play ();
				
		}
		} 
}

