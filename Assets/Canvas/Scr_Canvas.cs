using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Canvas : MonoBehaviour {
	public GameObject vHole;
	public GameObject vBlackN;
	public GameObject vBlackE;
	public GameObject vBlackS;
	public GameObject vBlackW;
	public bool vTransition;
	public float vScale;
	public float vDistance;
	public string vDestination;
	// Use this for initialization
	void Start () {
		vTransition = false;
		vScale = 120f;
	}
	
	// Update is called once per frame
	void Update () {
		if (vTransition) {
			vScale -= 1f;
			vHole.transform.localScale = new Vector3 (vScale, vScale, 1f);
		} else {
			if (vScale < 120f)
			vScale += 1f;
			vHole.transform.localScale = new Vector3 (vScale, vScale, 1f);

		}
			
	}
}
