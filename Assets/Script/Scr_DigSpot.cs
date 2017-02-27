using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DigSpot : MonoBehaviour {
	public string vItemName;
	public GameObject vItem;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FoundYou(){
		GameObject tObj;
		tObj = Instantiate (vItem);
		tObj.transform.position = new Vector3 (transform.position.x,transform.position.y+1f,transform.position.z);
		Destroy(this.gameObject);
	}
	void Update () {
		
	}
}
