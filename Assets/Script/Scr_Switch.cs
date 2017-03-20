using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Switch : MonoBehaviour {
	public bool vOn;
	public string ObjectsToAffect;
	public Material vMatOn;
	public Material vMatOff;
	// Use this for initialization

	void GetHit(){
		if (vOn)
			vOn = false;
		else
			vOn = true;
		SetImage ();
		GameObject[] Those = GameObject.FindGameObjectsWithTag ("Spikes");
		foreach (GameObject That in Those)
			That.SendMessage ("GetSwitched");
	}

	void SetImage(){
		if (vOn) {
			this.GetComponent<Renderer> ().material = vMatOn;

		} else {
			this.GetComponent<Renderer>().material = vMatOff;

		}

	}
}
