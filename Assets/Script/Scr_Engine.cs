using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Engine : MonoBehaviour {

	public string vDestination;
	public bool[] vKeysAry = new bool[] {true,true,true,true,true};
	// Use this for initialization
	void Start () {

		DontDestroyOnLoad (this.transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
