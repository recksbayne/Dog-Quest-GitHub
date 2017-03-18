using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_DigSpot : MonoBehaviour {
	public GameObject vItem;
	public bool vIsAnItem = true;
	public int vDigSpotNumb;
	// Use this for initialization
	void Start () {

		GameObject tGO = GameObject.FindGameObjectWithTag ("Player");
		bool tBool = tGO.GetComponent<Scr_Player> ().aDigOpened [vDigSpotNumb];
		if (!tBool && vIsAnItem)
			Destroy (this.gameObject);
	}
	
	// Update is called once per frame
	void FoundYou(){
		GameObject tGO;
		if (vIsAnItem) {
			GameObject tObj;
			tObj = Instantiate (vItem);
			tObj.transform.position = new Vector3 (transform.position.x, transform.position.y + 1f, transform.position.z);
			tGO = GameObject.FindGameObjectWithTag ("Player");
			tGO.GetComponent<Scr_Player> ().aDigOpened [vDigSpotNumb] = false;
			Destroy (this.gameObject);
		} else {
			tGO = GameObject.FindGameObjectWithTag ("Canvas");
			tGO.SendMessage("GotoNextRoom",10);
			Destroy (this.gameObject);
		}
	}
	void Update () {
		
	}
}
